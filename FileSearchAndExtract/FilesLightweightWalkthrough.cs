using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wpf.Frno.SearchAndExtract.Logging;

namespace Wpf.Frno.SearchAndExtract.FileSearchAndExtract
{
    public class FilesLightweightWalkthrough
    {
        private ConcurrentBag<Tuple<string, string, string, long>>  files = new ConcurrentBag<Tuple<string, string, string, long>>();
        public ConcurrentBag<Tuple<string, string, string, long>> Files
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

        public string OriginalPath { get; set; }

        public async Task WalkDirectoryTree(DirectoryInfo currentDirectoryInfo, string searchPattern, int level)
        {
            if (level == 0)
            {
                return;
            }

            try
            {
                foreach (FileInfo fi in currentDirectoryInfo.GetFiles(searchPattern))
                {
                    Files.Add(new Tuple<string, string, string, long>(
                        fi.Name, 
                        Path.GetExtension(fi.Name),
                        fi.DirectoryName.Replace(OriginalPath, string.Empty),
                        fi.Length));
                }

                Parallel.ForEach(currentDirectoryInfo.GetDirectories(), dir => WalkDirectoryTree(dir, searchPattern, level - 1));
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
        }

        public void ClearFiles()
        {
            var newBag = new ConcurrentBag<Tuple<string, string, string, long>>();
            Interlocked.Exchange(ref files, newBag);
        }
    }
}
