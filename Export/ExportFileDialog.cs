using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Frno.SearchAndExtract.Export
{
    public class ExportFileDialog : IExportFileDialog
    {
        private ExportFileDialog() { }

        public static ExportFileDialog Instance { get; set; } = new ExportFileDialog();

        public void Open(Func<bool> action, AsyncCallback callback)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                action.BeginInvoke(callback, null);
            }
        }
    }
}
