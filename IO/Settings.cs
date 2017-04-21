using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Wpf.Frno.SearchAndExtract.IO
{
    public class Settings<TItem> : ISettings<TItem>
    {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string FilePath => System.IO.Path.Combine(Folder, FileName);

        private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(XmlStringSettings));

        public void Save(IList<TItem> settings)
        {
            Serialize(settings);
        }

        public IList<TItem> Load()
        {
            return Deserialize();
        }

        private void Serialize(IList<TItem> settings)
        {
            if (string.IsNullOrWhiteSpace(Folder))
            {
                Folder = Environment.CurrentDirectory;
            }

            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    var data = new XmlStringSettings()
                    {
                        Settings = (List<string>)settings
                    };
                    _xmlSerializer.Serialize(writer, data);
                    File.WriteAllText(FilePath, stringWriter.ToString());
                }
            }
        }

        private IList<TItem> Deserialize()
        {
            if (string.IsNullOrWhiteSpace(Folder))
            {
                Folder = Environment.CurrentDirectory;
            }

            if (!string.IsNullOrWhiteSpace(Folder)
                && !string.IsNullOrWhiteSpace(FileName)
                && File.Exists(FilePath))
            {
                string fileContent = File.ReadAllText(FilePath);
                using (var stringWriter = new StringReader(fileContent))
                {
                    {
                        XmlStringSettings data = (XmlStringSettings) _xmlSerializer.Deserialize(stringWriter);
                        return (IList<TItem>) data.Settings;
                    }
                }
            }
            return null;
        }
    }
}
