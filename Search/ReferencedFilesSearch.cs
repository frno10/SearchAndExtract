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
    public class ReferencedFilesSearch : IFileSearch
    {
        public const string name = "Referenced files search";
        public const string description = "Apart from regular search it checks for a references to other files based within files content baseed on criteria";

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

        public ReferencedFilesSearch(Dispatcher dispatcher)
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
                if (directoryToQuery.FileSystemInfo is DirectoryInfo)
                {
                    foreach (string filePattern in FileSearchPattern)
                    {
                        foreach (FileInfo fi in ((DirectoryInfo) directoryToQuery.FileSystemInfo).GetFiles(filePattern))
                        {
                            Files.Add(
                                new FileDataLinked(
                                    fi.Name,
                                    Path.GetExtension(fi.Name),
                                    fi.DirectoryName.Replace(OriginalPath, string.Empty),
                                    fi.Length,
                                    directoryToQuery.FileSystemInfo.FullName));
                        }
                    }

                    //Parallel.ForEach(currentDirectoryInfo.GetDirectories(), dir => WalkDirectoryTree(dir, searchPattern, level - 1));
                    ((DirectoryInfo)directoryToQuery.FileSystemInfo).GetDirectories()
                        .ToList()
                        .ForEach(dir => ItemsToQuery.Enqueue(new FileInfoAndSearchDepth(dir, directoryToQuery.SubfolderSearchDepth - 1)));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void OnSearchCompleted()
        {
            foreach (FileDataLinked file in Files)
            {
                file.LinkedFromFiles = Files
                    .Where(x => x is FileDataLinked &&
                                ((FileDataLinked)x).LinksToFiles != null &&
                                ((FileDataLinked)x).LinksToFiles.Count > 0 &&
                                ((FileDataLinked)x).LinksToFiles.Contains(file.Name))
                    .Select(x => x.Name).ToList();
            }
        }

        public void ClearFiles()
        {
            files.Clear();
        }
    }
}
