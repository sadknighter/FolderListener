using System;
using System.Configuration;

namespace BCLTaskApp.ConfigSections
{
    [ConfigurationCollection(typeof(FolderElement),
        AddItemName = "Folder")]
    public class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey (ConfigurationElement element)
        {
            return ((element) as FolderElement)?.Name ?? throw new InvalidOperationException();
        }

        public FolderElement this [int idx] => (FolderElement) BaseGet(idx);
    }
}