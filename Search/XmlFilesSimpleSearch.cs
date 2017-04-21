using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.Logging;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Search
{
    public class XmlFilesSimpleSearch : IFileSearch
    {
        public const string name = "XML Simple file search";
        public const string description = "";

        public string Name { get { return name; } }

        public string Description { get { return description; } }

        private ObservableConcurrentBag2<IFileData> files;
        public ObservableConcurrentBag2<IFileData> Files
        {
            get
            {
                return files;
            }
            set
            {
                files = value;
            }
        }

        public XmlFilesSimpleSearch(Dispatcher dispatcher)
        {
            files = new ObservableConcurrentBag2<IFileData>(dispatcher);
        }

        public Queue<FileInfoAndSearchDepth> ItemsToQuery { get; set; } = new Queue<FileInfoAndSearchDepth>();

        public FileAndFolderCombinationEnum SearchIn { get { return FileAndFolderCombinationEnum.Folder; } }

        public bool IsSearchRunning { get { return ItemsToQuery != null && ItemsToQuery.Count > 0; } }

        public bool UseRegex { get; set; }

        public IEnumerable<string> FileSearchPattern { get; set; }

        public IEnumerable<string> ContentSearchPattern { get; set; }

        public string OriginalPath { get; set; }

        public void ProcessDirectoryQueue()
        {
            var directoryToQuery = ItemsToQuery.Dequeue();

            if (directoryToQuery.SubfolderSearchDepth == 0)
            {
                return;
            }

            try
            {
                foreach (string filePattern in FileSearchPattern)
                {
                    foreach (FileInfo fi in ((DirectoryInfo)directoryToQuery.FileSystemInfo).GetFiles(filePattern))
                    {
                        Files.Add(new XmlFileData(
                            fi.Name,
                            Path.GetExtension(fi.Name),
                            fi.DirectoryName.Replace(OriginalPath, string.Empty),
                            fi.Length,
                            OriginalPath));
                    }
                }

                //Parallel.ForEach(currentDirectoryInfo.GetDirectories(), dir => WalkDirectoryTree(dir, searchPattern, level - 1));
                ((DirectoryInfo)directoryToQuery.FileSystemInfo).GetDirectories()
                    .ToList()
                    .ForEach(dir => ItemsToQuery.Enqueue(new FileInfoAndSearchDepth(dir, directoryToQuery.SubfolderSearchDepth - 1)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            // LAST CALL
            if(Files != null && Files.Count > 0 && ItemsToQuery.Count == 0)
            {
                var allTags = new List<string>();

                foreach(var file in Files)
                {
                    allTags.Add(((XmlFileData)file).RootElement);
                    allTags.AddRange(((XmlFileData)file).ChildElements);
                }

                foreach(var file in Files)
                {
                    string fileName = file.Name.Split('.')[0];

                    ((XmlFileData)file).FilenameAsRootElementCount = Files.Count(x => ((XmlFileData)x).RootElement == fileName);
                    ((XmlFileData)file).FilenameAsChildElementCount += Files.Sum(x => ((XmlFileData)x).ChildElements.Count(y => y == fileName));
                }
            }
        }

        public void OnSearchCompleted()
        { }

        public void ClearFiles()
        {
            files.Clear();
        }
    }
}
