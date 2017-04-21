using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.Logging;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Search
{
    public class RegexSimpleSearch : IFileSearch
    {
        public const string name = "Regex file search";
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

        public RegexSimpleSearch(Dispatcher dispatcher)
        {
            files = new ObservableConcurrentBag2<IFileData>(dispatcher);
            Results = new List<RegexMatchedRecord>();
        }

        public Queue<FileInfoAndSearchDepth> ItemsToQuery { get; set; } = new Queue<FileInfoAndSearchDepth>();

        public bool IsSearchRunning { get { return ItemsToQuery != null && ItemsToQuery.Count > 0; } }

        public bool UseRegex { get; set; }

        public FileAndFolderCombinationEnum SearchIn { get { return FileAndFolderCombinationEnum.Folder; } }

        public IEnumerable<string> FileSearchPattern { get; set; }

        public IEnumerable<string> ContentSearchPattern { get; set; }

        public List<RegexMatchedRecord> Results { get; set; } 

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
                foreach (var filePattern in FileSearchPattern)
                {
                    foreach (FileInfo fi in ((DirectoryInfo) directoryToQuery.FileSystemInfo).GetFiles(filePattern))
                    {
                        string fileText = File.ReadAllText(fi.FullName);
                        foreach (var pattern in ContentSearchPattern)
                        {
                            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                            MatchCollection matches = regex.Matches(fileText);

                            if (matches != null && matches.Count > 0)
                            foreach (Match match in matches)
                            {
                                if (match.Success)
                                {
                                    Results.Add(
                                        new RegexMatchedRecord()
                                        {
                                            Match = match.Groups[0].Value,
                                            File = fi.FullName
                                        });

                                }
                            }
                        }
                        Files.Add(new FileData(
                            fi.Name,
                            Path.GetExtension(fi.Name),
                            fi.DirectoryName.Replace(OriginalPath, string.Empty),
                            fi.Length));
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
                throw ex;
            }
        }

        public void OnSearchCompleted()
        {
            //foreach (var file in Files)
            //{
            //    string fileText = File.ReadAllText(file.GetFullFilePath(OriginalPath));
            //    foreach (var pattern in ContentSearchPattern)
            //    {
            //        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            //        Match matches = regex.Match(fileText);

            //        if (matches.Success)
            //        {
            //            Results.Add(matches.Groups[0].Value);
            //        }
            //    }
            //}
        }

        public void ClearFiles()
        {
            files.Clear();
        }
    }
}
