using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.Logging;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Search
{
    public class FilesBatchWalkthrough //: IFileSearch
    {
        public const string name = "Simple file search";
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

        public FilesBatchWalkthrough(Dispatcher dispatcher)
        {
            files = new ObservableConcurrentBag2<IFileData>(dispatcher);
        }

        public Queue<FileInfoAndSearchDepth> ItemsToQuery { get; set; } = new Queue<FileInfoAndSearchDepth>();

        public FileAndFolderCombinationEnum SearchIn { get { return FileAndFolderCombinationEnum.Folder; } }

        public bool IsSearchRunning { get { return ItemsToQuery != null && ItemsToQuery.Count > 0; } }

        public string SearchPattern { get; set; }

        public string OriginalPath { get; set; }

        public async Task WalkDirectoryTree(DirectoryInfo currentDirectoryInfo, string searchPattern, int level)
        {
            if (level == 0)
            {
                return;
            }

            try
            {
                foreach (FileInfo fi in currentDirectoryInfo.GetFiles(searchPattern))
                {
                    Files.Add(new FileData(
                        fi.Name, 
                        Path.GetExtension(fi.Name),
                        fi.DirectoryName.Replace(OriginalPath, string.Empty),
                        fi.Length));
                }

                //Parallel.ForEach(currentDirectoryInfo.GetDirectories(), dir => WalkDirectoryTree(dir, searchPattern, level - 1));
                currentDirectoryInfo.GetDirectories()
                    .ToList()
                    .ForEach(dir => ItemsToQuery.Enqueue(new FileInfoAndSearchDepth(dir, level -1)));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void ProcessDirectoryQueue()
        {
            var directoryToQuery = ItemsToQuery.Dequeue();

            try
            {
                foreach (FileInfo fi in ((DirectoryInfo)directoryToQuery.FileSystemInfo).GetFiles(SearchPattern))
                {
                    Files.Add(new FileData(
                        fi.Name,
                        Path.GetExtension(fi.Name),
                        fi.DirectoryName.Replace(OriginalPath, string.Empty),
                        fi.Length));
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
        }

        public void OnSearchCompleted()
        { }

        public void ClearFiles()
        {
            files.Clear();
        }
    }
}
