using System;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Search
{
    public class SearchInfo //: ISearchInfo
    {
        private string name;
        private string description;
        private Type fileSearchType;

        public SearchInfo() { }

        public SearchInfo(string searchName, string description, Type searchFileType)
        {
            name = searchName;
            this.description = description;
            fileSearchType = searchFileType;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public Type FileSearchType
        {
            get
            {
                return fileSearchType;
            }
        }
    }
}
