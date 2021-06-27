using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Threading;

using BCLTaskApp.ConfigSections;
using NLog;

namespace BCLTaskApp
{
    public class RuleFileMoving: DispatcherObject
    {
        public event FileSystemEventHandler Moved;

        public event FileSystemEventHandler RuleFound;

        public event FileSystemEventHandler RuleNotFound;

        private readonly DefaultRuleElement _defaultRule;

        private readonly FileRulesCollection _fileRulesCollection;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private int _indexFile;

        public RuleFileMoving(FileRulesCollection rulesCollection, DefaultRuleElement defaultRule)
        {
            _defaultRule = defaultRule;
            _fileRulesCollection = rulesCollection;
        }

        public void OnCreatedForMoving (object source, FileSystemEventArgs e)
        {
            GetFileMovingRule(e);
        }

        private void GetFileMovingRule(FileSystemEventArgs eventArgs)
        {

            for (var index = 0; index < _fileRulesCollection.Count; index++)
            {
                var pattern = _fileRulesCollection[index].Pattern;
                
                if (CheckRule(pattern, eventArgs))
                {
                    FileMoving(
                        _fileRulesCollection [index].Destination,
                        eventArgs,
                        _fileRulesCollection[index].IsNeededNumber,
                        _fileRulesCollection[index].IsNeededDate);
                    return;
                }
            }

            FileMoving(
                _defaultRule.Destination,
                eventArgs,
                _defaultRule.IsNeededNumber,
                _defaultRule.IsNeededDate);
        }

        private bool CheckRule(string pattern, FileSystemEventArgs eventArgs)
        {

            var fileName = Path.GetFileName(eventArgs.FullPath);
            if (fileName == null)
            {
                var exception = new InvalidOperationException("Can not read/get file name");

                Logger.Error(exception);
                throw exception;
            }
            if (Regex.IsMatch(fileName, pattern))
            {
                OnRuleFound(pattern, eventArgs);
                return true;
            }

            OnRuleNotFound(pattern, eventArgs);

            return false;
        }
        private void FileMoving(string destination, FileSystemEventArgs eventArgs, bool isRequiredNumber, bool isRequiredDate)
        {
            var fileName = Path.GetFileName(eventArgs.FullPath);
            destination = ConstructDestinationPath(destination, fileName, isRequiredNumber, isRequiredDate);
            File.GetAccessControl(eventArgs.FullPath ?? throw new InvalidOperationException());
            Thread.Sleep(1000);

            File.Move(eventArgs.FullPath, destination);

            OnMoved(destination, eventArgs);
            Thread.Sleep(2000);
        }

        private string ConstructDestinationPath(string destination, string fileName, bool isRequiredNumber, bool isRequiredDate)
        {
            if (isRequiredNumber)
            {
                _indexFile++;
                fileName = _indexFile + fileName;
            }

            if (isRequiredDate)
            {
                var date = DateTime.Now.ToString("(dd.MM.yy_HH.mm.ss_tt)",
                    Thread.CurrentThread.CurrentCulture);
                fileName = date + fileName;
            }

            FileSystemPathChecker.CreateDirectoryIfNotExist(destination);

            if (fileName == null)
            {
                var exception = new InvalidOperationException($"Can not get file name in {destination}");
                Logger.Error(exception);
                
                throw exception;
            }
            destination = Path.Combine(
                destination,
                fileName);
            destination = FileSystemPathChecker.RenameFileIfThereAreFileWithSameName(destination);

            return destination;
        }

        public void OnMoved(object sender, FileSystemEventArgs args)
        {
            Moved?.Invoke(sender, args);
        }

        public void OnRuleFound(object sender, FileSystemEventArgs args)
        {
            RuleFound?.Invoke(sender, args);
        }

        public void OnRuleNotFound(object sender, FileSystemEventArgs args)
        {
            RuleNotFound?.Invoke(sender, args);
        }
    }
}
