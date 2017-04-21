using System;

namespace Wpf.Frno.SearchAndExtract.Search.Interfaces
{
    public interface ISearchType
    {
        string Name { get; }

        Type FileSearchType { get; }
    }
}
