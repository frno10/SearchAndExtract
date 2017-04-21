using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Frno.SearchAndExtract.Logging;

namespace Wpf.Frno.SearchAndExtract.FileSearchAndExtract
{
    public class FilesWalkthrough
    {
        public ConcurrentBag<FileInfo> Files { get; set; } = new ConcurrentBag<FileInfo>();

        public async Task WalkDirectoryTree(DirectoryInfo currentDirectoryInfo, string searchPattern)
        {
            try
            {
                foreach (var fi in currentDirectoryInfo.GetFiles(searchPattern))
                {
                    Files.Add(fi);
                }

                Parallel.ForEach(currentDirectoryInfo.GetDirectories(), dir => WalkDirectoryTree(dir, searchPattern));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }
    }
}
