using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Export
{
    public class TxtResultsExporter : IResultsExporter
    {
        public bool Export(string content, string path)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch(Exception e)
            {
                // TODO log
                return false;
            }
            return true;
        }
    }
}
