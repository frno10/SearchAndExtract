using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using Wpf.Frno.SearchAndExtract.FileSearchAndExtract;
using Wpf.Frno.SearchAndExtract.Search;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;
using Wpf.Frno.SearchAndExtract.Utilities;

namespace Wpf.Frno.SearchAndExtract.ViewModels
{
    public class MainViewModel : NotifyPropertyChanged
    {
        private Dispatcher dispatcher;
        private IFileSearch fileSearch;
        private IList<ISearchInfo> searchTypes;
        private string status;
        private int levelOfSubfoldersToBrowse;
        private string errorMessage;
        private string elapsedTime;
        private string originalPath;
        private List<string> fileSearchPattern;
        private List<string> contentSearchPattern;

        public IFileSearch FileSearch
        {
            get
            {
                return fileSearch;
            }
            set
            {
                fileSearch = value;
                fileSearch.FileSearchPattern = fileSearchPattern;
                fileSearch.OriginalPath = originalPath; 
                OnPropertyChanged();
            }
        }
        public IList<ISearchInfo> SearchTypes
        {
            get
            {
                return searchTypes;
            }
            set
            {
                searchTypes = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged();
            }
        }
        public List<string> FileSearchPattern
        {
            get
            {
                return fileSearchPattern;
            }
            set
            {
                if (FileSearch != null)
                {
                    FileSearch.FileSearchPattern = value;
                }
                fileSearchPattern = value;
            }
        }
        public List<string> ContentSearchPattern
        {
            get
            {
                return contentSearchPattern;
            }
            set
            {
                if (FileSearch != null)
                {
                    FileSearch.ContentSearchPattern = value;
                }
                contentSearchPattern = value;
            }
        }
        public int LevelOfSubfoldersToBrowse
        {
            get
            {
                return levelOfSubfoldersToBrowse;
            }
            set
            {
                levelOfSubfoldersToBrowse = value;
            }
        }
        public string OriginalPath
        {
            get { return FileSearch.OriginalPath; }
            set
            {
                if(FileSearch != null &&
                    !string.IsNullOrWhiteSpace(value) &&
                    (Directory.Exists(value) || File.Exists(value)))
                {
                    FileSearch.OriginalPath = value;
                    SetOriginalPathAsStartingSearchDirectory();
                }
                originalPath = value;
            }
        }
        public bool IsSearchRunning { get { return FileSearch.IsSearchRunning; } }
        public int ResultsCount
        {
            get
            {
                return FileSearch != null && FileSearch.Files != null ? FileSearch.Files.Count : 0;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged();
            }
        }
        public string ElapsedTime
        {
            get
            {
                return elapsedTime;
            }
            set
            {
                elapsedTime = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
            InitializeSearchTypes();
            LevelOfSubfoldersToBrowse = 3;
            fileSearchPattern = new List<string>();
        }

        public void Clear()
        {
            FileSearch.ClearFiles();
        }

        public void InvokePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public async Task Search()
        {
            Status = "Working ...";
            Stopwatch stopwatch = Stopwatch.StartNew();
            ErrorMessage = string.Empty;
            Clear();

            if (FileSearch.ItemsToQuery.Count == 0)
            {
                SetOriginalPathAsStartingSearchDirectory();
            }

            await Task.Run(() =>
            {
                try
                {
                    do
                    {
                        if (FileSearch.ItemsToQuery.Count > 0)
                        {
                            FileSearch.ProcessDirectoryQueue();
                        }
                        else
                        {
                            ErrorMessage = "Specify root folder";
                        }
                        OnPropertyChanged("ResultsCount");
                        ElapsedTime = string.Format("{0} ms", stopwatch.ElapsedMilliseconds);
                    }
                    while (IsSearchRunning);
                    FileSearch.OnSearchCompleted();
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                }
            }
            );

            stopwatch.Stop();
            Status = "Done";
        }

        private void InitializeSearchTypes()
        {
            IEnumerable<Type> searchTypes = ReflectionHelper.GetAllTypesInAssembly<ISearchInfo>();
            SearchTypes = ReflectionHelper.InitializeTypes<ISearchInfo>(searchTypes);
        }

        private void SetOriginalPathAsStartingSearchDirectory()
        {
            if (!string.IsNullOrWhiteSpace(OriginalPath))
            {
                FileSearch.ClearFiles();
                FileSystemInfo fileInfo = null;

                if (Directory.Exists(OriginalPath))
                {
                    fileInfo = new DirectoryInfo(OriginalPath);
                }
                else if(File.Exists(OriginalPath))
                {
                    fileInfo = new FileInfo(OriginalPath);
                }
                FileSearch.ItemsToQuery.Enqueue(new Data.FileInfoAndSearchDepth(fileInfo, levelOfSubfoldersToBrowse));
            }
        }
    }
}
