using System.Collections.Generic;
using Wpf.Frno.SearchAndExtract.Data;

namespace Wpf.Frno.SearchAndExtract.Search.Interfaces
{
    public interface IFileSearch : ISearchInfo
    {
        ObservableConcurrentBag2<IFileData> Files { get; }

        IEnumerable<string> FileSearchPattern { get; set; }

        IEnumerable<string> ContentSearchPattern { get; set; } 

        string OriginalPath { get; set; }

        bool IsSearchRunning { get; }

        bool UseRegex { get; set; }

        Queue<FileInfoAndSearchDepth> ItemsToQuery { get; }

        FileAndFolderCombinationEnum SearchIn { get; }

        void ProcessDirectoryQueue();

        void OnSearchCompleted();

        void ClearFiles();
    }
}
