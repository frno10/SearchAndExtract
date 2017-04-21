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
    public class LayoutsFilesSearch : IFileSearch
    {
        public const string name = "Layouts files search";
        public const string description = "Apart from regular search it checks for a references to other files based within files content baseed on criteria";

        public string Name => name;
        public string Description => description;

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

        public LayoutsFilesSearch(Dispatcher dispatcher)
        {
            files = new ObservableConcurrentBag2<IFileData>(dispatcher);
        }

        public Queue<FileInfoAndSearchDepth> ItemsToQuery { get; set; } = new Queue<FileInfoAndSearchDepth>();

        public FileAndFolderCombinationEnum SearchIn => FileAndFolderCombinationEnum.Folder;

        public bool IsSearchRunning => ItemsToQuery != null && ItemsToQuery.Count > 0;

        public bool UseRegex { get; set; }

        public IEnumerable<string> FileSearchPattern { get; set; }

        public IEnumerable<string> ContentSearchPattern { get; set; }

        public string OriginalPath { get; set; }

        public Dictionary<string, List<string>> Layouts { get; set; }

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
                                new FileDataLayoutLinked(
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
            if (Layouts == null)
            {
                Layouts = new Dictionary<string, List<string>>();
            }
            foreach (FileDataLayoutLinked file in Files)
            {
                if (file.LinksToLayouts != null
                    && file.LinksToLayouts.Any())
                {
                    foreach (var layout in file.LinksToLayouts)
                    {
                        if (!Layouts.ContainsKey(layout))
                        {
                            Layouts.Add(layout, new List<string>() {file.GetFullFilePath()});
                        }
                        else if (!Layouts[layout].Contains(file.GetFullFilePath()))
                        {
                            Layouts[layout].Add(file.GetFullFilePath());
                        }
                    }
                }
                else
                {
                    string noLayout = "-none-";
                    if (!Layouts.ContainsKey(noLayout))
                    {
                        Layouts.Add(noLayout, new List<string>() { file.GetFullFilePath() });
                    }
                    else if (!Layouts[noLayout].Contains(file.GetFullFilePath()))
                    {
                        Layouts[noLayout].Add(file.GetFullFilePath());
                    }
                }
            }
        }

        public void ClearFiles()
        {
            files.Clear();
        }
    }
}
