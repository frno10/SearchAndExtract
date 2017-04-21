using System.IO;

namespace Wpf.Frno.SearchAndExtract.Data
{
    public class FileInfoAndSearchDepth
    {
        public FileInfoAndSearchDepth() { }

        public FileInfoAndSearchDepth(FileSystemInfo fileSystemInfo, int subfolderSearchDepth)
        {
            FileSystemInfo = fileSystemInfo;
            SubfolderSearchDepth = subfolderSearchDepth;
        }

        public FileSystemInfo FileSystemInfo { get; set; }
        public int SubfolderSearchDepth { get; set; }
    }
}
