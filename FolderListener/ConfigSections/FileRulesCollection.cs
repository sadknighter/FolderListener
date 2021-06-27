using System;
using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    [ConfigurationCollection(typeof(FileRulesElement),
        AddItemName = "FileRule")]
    public class FileRulesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement ()
        {
            return new FileRulesElement();
        }

        protected override object GetElementKey (ConfigurationElement element)
        {
            return ((element) as FileRulesElement)?.Name ?? throw new InvalidOperationException();
        }

        public FileRulesElement this [int idx] => (FileRulesElement) BaseGet(idx);
    }
}