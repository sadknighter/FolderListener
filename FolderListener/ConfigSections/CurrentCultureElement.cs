using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    public class CurrentCultureElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Name
        {
            get => ((string) (base ["name"]));
            set => base ["name"] = value;
        }
    }
}
