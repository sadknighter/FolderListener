using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    public class DefaultRuleElement : ConfigurationElement
    {
        [ConfigurationProperty("destination", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Destination
        {
            get => ((string) (base ["destination"]));
            set => base ["destination"] = value;
        }
        [ConfigurationProperty("isNeededNumber", DefaultValue = false, IsKey = false, IsRequired = true)]
        public bool IsNeededNumber
        {
            get => ((bool) (base ["isNeededNumber"]));
            set => base ["isNeededNumber"] = value;
        }

        [ConfigurationProperty("isNeededDate", DefaultValue = false, IsKey = false, IsRequired = true)]
        public bool IsNeededDate
        {
            get => ((bool) (base ["isNeededDate"]));
            set => base ["isNeededDate"] = value;
        }
    }
}