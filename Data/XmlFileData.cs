using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using Wpf.Frno.SearchAndExtract.FileSearchAndExtract;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public class XmlFileData : FileData
    {
        public XmlFileData(string name, string extension, string folder, long length, string rootFolder)
            : base(name, extension, folder, length)
        {
            ProcessXmlData(rootFolder);
        }

        public string RootElement { get; set; }

        [XmlIgnore]
        public List<string> ChildElements { get; set; }

        public int ChildElementsCount
        {
            get
            {
                return ChildElements.Count;
            }
        }

        public int ChildElementsDistinctCount
        {
            get
            {
                return ChildElements.Distinct().Count();
            }
        }

        public int Lines { get; set; }

        public int FilenameAsRootElementCount { get; set; }
        public int FilenameAsChildElementCount { get; set; }

        void ProcessXmlData(string rootFolder)
        {
            string fullFilePath = this.GetFullFilePath(rootFolder);

            XDocument document = XmlDocumentExtractor.GetXmlDocument(fullFilePath);
            RootElement = document.Root.Name.LocalName;
            ChildElements = document
                .Descendants()
                .Select(el => el.Name.LocalName)
                .ToList();

            Lines = File.ReadLines(fullFilePath).Count();
        }
    }
}
