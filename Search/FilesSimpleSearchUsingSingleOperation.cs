using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.Logging;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Search
{
    public class FilesSimpleSearchUsingSingleOperation : IFileSearch
    {
        public const string name = "Simple file search using single operation searching all subdirectories";
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

        public FilesSimpleSearchUsingSingleOperation(Dispatcher dispatcher)
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

            try
            {
                foreach (var filePattern in FileSearchPattern)
                {
                    foreach (FileInfo fi in ((DirectoryInfo)directoryToQuery.FileSystemInfo).GetFiles(filePattern, SearchOption.AllDirectories))
                    {
                        Files.Add(new FileData(
                            fi.Name,
                            Path.GetExtension(fi.Name),
                            fi.DirectoryName.Replace(OriginalPath, string.Empty),
                            fi.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
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
