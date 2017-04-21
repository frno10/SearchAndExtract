using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Frno.SearchAndExtract.Logging;
using Ookii.Dialogs.Wpf;

namespace Wpf.Frno.SearchAndExtract.ExportTools
{
    public class DataGridToCsv
    {
        private void Export(DataGrid datagrid, string file)
        {
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            datagrid.UnselectAllCells();

            string result = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);

            File.AppendAllText(file, result, Encoding.UTF8);
        }

        public bool TryExport(DataGrid datagrid)
        {
            try
            {
                Ookii.Dialogs.Wpf.VistaSaveFileDialog saveFileDialog = new VistaSaveFileDialog();
                if(saveFileDialog.ShowDialog() == true)
                {
                    Export(datagrid, saveFileDialog.FileName);
                }
            }
            catch(Exception e)
            {
                Logger.Error(e.Message);
            }
            finally { }

            return true;
        }
    }
}
