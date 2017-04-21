using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public interface IFileData
    {
        string Name { get; }
        string Extension { get; }
        string Folder { get; }
        long FileSize { get; }

        string GetFullFilePath(string rootFolder);
    }
}
