using System.Collections.Generic;

namespace Wpf.Frno.SearchAndExtract.IO
{
    public interface ISettings<TItem>
    {
        string Folder { get; set; }
        string FileName { get; set; }
        string FilePath { get; }

        void Save(IList<TItem> settings);
        IList<TItem> Load();
    }
}
