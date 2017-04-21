using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using Wpf.Frno.SearchAndExtract.Utilities;
using System.Linq;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public class FileDataLinked : NotifyPropertyChanged, IFileData
    {
        private List<string> linkedFromFiles = new List<string>();

        public FileDataLinked(string name, string extension, string folder, long length, string rootFolder)
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

        [DisplayName("Links to")]
        public List<string> LinksToFiles { get; set; }
        public int LinksToFilesCount
        {
            get { return LinksToFiles != null ? LinksToFiles.Count : 0; }
        }
        public int UniqueLinksToFilesCount
        {
            get { return LinksToFiles != null ? LinksToFiles.Distinct().ToList().Count : 0; }
        }

        [DisplayName("Linked from")]
        public List<string> LinkedFromFiles
        {
            get
            {
                return linkedFromFiles;
            }
            set
            {
                linkedFromFiles = value;
                OnPropertyChanged("LinkedFromFilesCount");
                OnPropertyChanged("UniqueLinkedFromFilesCount");
            }
        }
        public int LinkedFromFilesCount
        {
            get { return LinkedFromFiles != null ? LinkedFromFiles.Count : 0; }
        }
        public int UniqueLinkedFromFilesCount
        {
            get { return LinkedFromFiles != null ? LinkedFromFiles.Distinct().ToList().Count : 0; }
        }

        public string GetFullFilePath(string rootFolder)
        {
            return Path.Combine(
                        rootFolder,
                        Folder.TrimStart('\\'),
                        Name);
        }

        private void GetLinksToFilesCount(string rootFolder)
        {
            var fileFullPath = Path.Combine(
                        rootFolder,
                        Name);

            if (File.Exists(fileFullPath))
            {
                string fileContent = File.ReadAllText(fileFullPath);

                Regex regex = new Regex(@"(\w+\.asp)", RegexOptions.IgnoreCase);

                MatchCollection matches = regex.Matches(fileContent);
                if (matches.Count > 0)
                {
                    LinksToFiles = new List<string>();

                    foreach (Match match in matches)
                    {
                        LinksToFiles.Add(match.Value);
                    }
                }
            }
            OnPropertyChanged("LinksToFilesCount");
        }
    }
}
