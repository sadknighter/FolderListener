using BCLTaskApp.ConfigSections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NLog;

namespace BCLTaskApp
{
    public class FileSystemMonitor:IDisposable
    {
        public FileSystemEventHandler Created;

        private readonly List<FileSystemWatcher> _watchers;
        private readonly FoldersCollection _foldersCollection;
        private readonly RuleFileMoving _ruleFileMoving;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private bool _disposed;


        public FileSystemMonitor()
        {
            _watchers = new List<FileSystemWatcher>();
            var fileWatcherSettings = new FileWatcherSettings();

            _foldersCollection = fileWatcherSettings.GetFoldersCollection();

            _ruleFileMoving = new RuleFileMoving(
                fileWatcherSettings.GetFileRulesCollection(),
                fileWatcherSettings.GetDefaultRule());
        }

        public void Start()
        {
            Logger.Info("{0}{1}{2}", 
                Resources.Resources.StartMessage, 
                Environment.NewLine,
                GetMonitoringDirectories());

            var fileSystemInformer = new FileSystemInformer();

            for (var index = 0; index < _foldersCollection.Count; index++)
            {
                FileSystemPathChecker.CreateDirectoryIfNotExist(_foldersCollection[index].Path);
                var watcher = new FileSystemWatcher(_foldersCollection[index].Path) 
                                  { EnableRaisingEvents = true };

                watcher = SubscribeEvents(watcher, fileSystemInformer);
                _watchers.Add(watcher);
            }

            _ruleFileMoving.Moved += fileSystemInformer.OnMoved;
            _ruleFileMoving.RuleFound += fileSystemInformer.OnRuleFound;
            _ruleFileMoving.RuleNotFound += fileSystemInformer.OnRuleNotFound;
        }

        public void Stop()
        {
            //something to do
        }

        private FileSystemWatcher SubscribeEvents(FileSystemWatcher watcher, FileSystemInformer handler)
        {
            watcher.Created += handler.OnCreated;
            watcher.Created += _ruleFileMoving.OnCreatedForMoving;
            watcher.Deleted += handler.OnDeleted;
            return watcher;
        }

        public string GetMonitoringDirectories()
        {
            var sb = new StringBuilder();
            for (var index = 0; index < _foldersCollection.Count; index++)
            {
                sb.Append(_foldersCollection[index].Path + Environment.NewLine);
            }
            return sb.ToString();
        }

        private void ReleaseManagedResources()
        {
            foreach (var t in _watchers)
            {
                t.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    ReleaseManagedResources();
                }

                _disposed = true;
            }
        }

        ~FileSystemMonitor()
        {
            Dispose(false);
        }
    }
}