using System.IO;
using NLog;

namespace BCLTaskApp
{
    public class FileSystemInformer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void OnCreated(object source, FileSystemEventArgs e)
        {
            var changeType = Resources.Resources.CreateOption;

            Logger.Info("Watcher on path {0} invoke : {1}: {2} {3}",
                (source as FileSystemWatcher)?.Path,
                Resources.Resources.File,
                e.FullPath,
                changeType);
        }

        public void OnDeleted(object source, FileSystemEventArgs e)
        {
            var changeType = Resources.Resources.DeleteOption;
            var watcherMessage = Resources.Resources.WatcherMessage;
            var invokeOption = Resources.Resources.InvokeOption;

            Logger.Info("{0} {1} {2}: {3} {4}",
                watcherMessage,
                invokeOption,
                Resources.Resources.FileWatcherService,
                e.FullPath,
                changeType);
        }

        public void OnMoved(object source, FileSystemEventArgs e)
        {
            var changeType = Resources.Resources.MoveOption;
            var watcherMessage = Resources.Resources.WatcherMessage;
            var invokeOption = Resources.Resources.InvokeOption;

            Logger.Info("{0}, {1}, {2}: {3}, {4} {5}",
                watcherMessage,
                invokeOption,
                Resources.Resources.File,
                e.FullPath,
                changeType,
                source);
        }

        public void OnRuleFound(object source, FileSystemEventArgs e)
        {
            var changeType = Resources.Resources.RuleFoundOption;

            Logger.Info("{0}: {1}: {2}", changeType, e.FullPath, source);
        }

        public void OnRuleNotFound(object source, FileSystemEventArgs e)
        {
            var changeType = Resources.Resources.RuleNotFoundOption;

            Logger.Info("{0}: {1}: {2}", changeType, e.FullPath, source);
        }
    }
}
