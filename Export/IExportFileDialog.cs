using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Export
{
    public interface IExportFileDialog
    {
        void Open(Func<bool> action, AsyncCallback callback);
    }
}
