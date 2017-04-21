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
    public class LinkedFilesSearch : IFileSearch
    {
        public const string name = "Linked files search";
        public const string description = "Search hierarchy created by file references find in initial file content";

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

        public LinkedFilesSearch(Dispatcher dispatcher)
        {

            files = new ObservableConcurrentBag2<IFileData>(dispatcher);
        }

        public Queue<FileInfoAndSearchDepth> ItemsToQuery { get; set; } = new Queue<FileInfoAndSearchDepth>();

        public bool IsSearchRunning { get { return ItemsToQuery != null && ItemsToQuery.Count > 0; } }

        public bool UseRegex { get; set; }

        public FileAndFolderCombinationEnum SearchIn { get { return FileAndFolderCombinationEnum.File; } }

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
            if(directoryToQuery.FileSystemInfo is DirectoryInfo)
            {
                throw new Exception("Please select file, not folder, for this search");
            }

            try
            {
                if (directoryToQuery.FileSystemInfo is FileInfo)
                {
                    FileInfo fi = (FileInfo)directoryToQuery.FileSystemInfo;
                    FileDataLinked file = new FileDataLinked(
                        fi.Name,
                        Path.GetExtension(fi.Name),
                        fi.DirectoryName.Replace(OriginalPath, string.Empty),
                        fi.Length,
                        directoryToQuery.FileSystemInfo is FileInfo ? 
                            ((FileInfo)directoryToQuery.FileSystemInfo).DirectoryName : 
                            directoryToQuery.FileSystemInfo.FullName);
                    Files.Add(file);

                    if (file.LinksToFiles != null &&
                        file.LinksToFiles.Count > 0)
                    {
                        file.LinksToFiles
                            .Distinct()
                            .Where(x => Files.All(y => y.Name != x))
                            .ToList()
                            .ForEach(fileInfo => ItemsToQuery.Enqueue(new FileInfoAndSearchDepth(new FileInfo(Path.Combine(file.Folder, fileInfo)), directoryToQuery.SubfolderSearchDepth - 1)));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void OnSearchCompleted()
        {
            foreach(FileDataLinked file in Files)
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
            ItemsToQuery.Clear();
        }
    }
}
