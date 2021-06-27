using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    public class FileWatcherSettingsConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        public FoldersCollection Folders => ((FoldersCollection) (base["Folders"]));

        [ConfigurationProperty("FilesRules")]
        public FileRulesCollection FilesRules => ((FileRulesCollection) (base["FilesRules"]));

        [ConfigurationProperty("DefaultRule")]
        public DefaultRuleElement DefaultRule => ((DefaultRuleElement) (base["DefaultRule"]));

        [ConfigurationProperty("CurrentCulture")]
        public CurrentCultureElement CurrentCulture => ((CurrentCultureElement) (base ["CurrentCulture"]));
    }
}