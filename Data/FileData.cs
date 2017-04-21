using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public class FileData : IFileData
    {
        public FileData(string name, string extension, string folder, long length)
        {
            Name = name;
            Extension = extension;
            Folder = folder;
            FileSize = length;
        }

        [DisplayName("Ext")]
        public string Extension { get; set; }

        [DisplayName("Size")]
        public long FileSize { get; set; }

        [DisplayName("Folder")]
        public string Folder { get; set; }

        [DisplayName("File")]
        public string Name { get; set; }

        public string GetFullFilePath(string rootFolder)
        {
            return System.IO.Path.Combine(
                        rootFolder,
                        Folder.TrimStart('\\'),
                        Name);
        }
    }
}
