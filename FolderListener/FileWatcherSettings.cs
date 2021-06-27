using System.Configuration;

using BCLTaskApp.ConfigSections;

namespace BCLTaskApp
{
    public class FileWatcherSettings
    {
        public FileWatcherSettingsConfigSection SettingsSection;

        public FileWatcherSettings()
        {
            SettingsSection = (FileWatcherSettingsConfigSection) ConfigurationManager.GetSection("FileWatcherSettings");
        }

        public FoldersCollection GetFoldersCollection()
        {
            return SettingsSection.Folders;
        }

        public FileRulesCollection GetFileRulesCollection()
        {
            return SettingsSection.FilesRules;
        }

        public DefaultRuleElement GetDefaultRule()
        {
            return SettingsSection.DefaultRule;
        }

        public CurrentCultureElement GetCurrentCulture()
        {
            return SettingsSection.CurrentCulture;
        }
    }
}
