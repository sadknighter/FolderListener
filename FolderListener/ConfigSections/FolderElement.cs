using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    public class FolderElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get => ((string) (base ["name"]));
            set => base ["name"] = value;
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Path
        {
            get => ((string) (base ["path"]));
            set => base ["path"] = value;
        }
    }
}