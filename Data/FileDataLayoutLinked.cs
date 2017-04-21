using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Wpf.Frno.SearchAndExtract.Utilities;
using System.Linq;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public class FileDataLayoutLinked : NotifyPropertyChanged, IFileData
    {
        private List<string> linkedFromFiles = new List<string>();

        public FileDataLayoutLinked(string name, string extension, string folder, long length, string rootFolder)
        {
            Name = name;
            Extension = extension;
            Folder = folder;
            FileSize = length;

            GetLinksToFilesCount(rootFolder);
        }

        [DisplayName("Ext")]
        public string Extension { get; set; }

        [DisplayName("Size")]
        public long FileSize { get; set; }

        [DisplayName("Folder")]
        public string Folder { get; set; }

        [DisplayName("File")]
        public string Name { get; set; }

        [DisplayName("Layout")]
        public List<string> LinksToLayouts { get; set; }

        public List<string> Partials { get; set; }

        public int LinksToPartialCount { get { return Partials?.Count ?? 0; } }

        public string GetFullFilePath(string rootFolder = "")
        {
            return "~/" +
                Path.Combine(
                    rootFolder,
                    Folder.TrimStart('\\'),
                    Name)
                    .Replace("\\", "/");
        }

        private void GetLinksToFilesCount(string rootFolder)
        {
            var fileFullPath = Path.Combine(
                        rootFolder,
                        Name);

            if (File.Exists(fileFullPath))
            {
                string fileContent = File.ReadAllText(fileFullPath);

                if (LinksToLayouts == null) LinksToLayouts = new List<string>();
                if (Partials == null) Partials = new List<string>();

                RegexSearchFor(@"Layout[ ]?=[ ]?""(\S*)""", fileContent, LinksToLayouts);
                RegexSearchFor(@"RenderPartial\(""(\S*)""\)", fileContent, Partials);
                RegexSearchFor(@"RenderPartialAsync\(""(\S*)""\)", fileContent, Partials);
            }
            OnPropertyChanged("LinksToPartialCount");
        }

        private void RegexSearchFor(string pattern, string fileContent, List<string> collectionToUpdate)
        {
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(fileContent);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    collectionToUpdate.Add(match.Groups[1].Value);
                }
            }
        }
    }
}
