using Ookii.Dialogs.Wpf;
using Wpf.Frno.SearchAndExtract.Search;

namespace Wpf.Frno.SearchAndExtract.Utilities
{
    public class FileAndFolderBrowsers
    {
        public static string BrowseForFolder()
        {
            VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();

            bool? result = dialog.ShowDialog();

            return dialog.SelectedPath;
        }

        public static string BrowseForSingleFile()
        {
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();

            bool? result = dialog.ShowDialog();

            return dialog.FileName;
        }

        public static string[] BrowseForMultipleFiles()
        {
            VistaOpenFileDialog dialog = new VistaOpenFileDialog();

            bool? result = dialog.ShowDialog();

            return dialog.FileNames;
        }

        internal static string Browse(FileAndFolderCombinationEnum searchIn)
        {
            string result = string.Empty;

            switch(searchIn)
            {
                case FileAndFolderCombinationEnum.File:
                    result = BrowseForSingleFile();
                    break;
                case FileAndFolderCombinationEnum.Folder:
                    result = BrowseForFolder();
                    break;
            }
            return result;
        }
    }
}
