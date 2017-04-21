using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.FileSearchAndExtract
{
    public class XmlDocumentExtractor
    {
        public static List<XamlTag> ExtractTagsFromXmlFiles(IFileSearch fileSearch, string rootFolder)
        {
            List<string> allTags = new List<string>();

            if (fileSearch != null &&
               fileSearch.Files != null)
            {
                foreach (var file in fileSearch.Files)
                {
                    XDocument xmlDocument = GetXmlDocument(file, rootFolder);
                    var tags = xmlDocument
                        .Descendants()
                        .Select(el => el.Name.LocalName)
                        .ToList();
                    allTags.AddRange(tags);
                }
            }
            // remove nested tags
            allTags.RemoveAll(x => x.Contains("."));
            //allTags = allTags.Distinct().OrderBy(x => x).ToList();
            //return allTags;
            return allTags
                .GroupBy(x => x)
                .Select(y => new XamlTag()
                {
                    TagName = y.Key,
                    NumberOfOccurencies = allTags.Count(z => z == y.Key)
                })
                .ToList();
        }

        public static XDocument GetXmlDocument(string filePath)
        {
            return XDocument.Load(filePath);
        }

        public static XDocument GetXmlDocument(IFileData dataFile, string rootFolder)
        {
            return XDocument.Load(dataFile.GetFullFilePath(rootFolder));
        }
    }
}
