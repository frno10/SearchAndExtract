using System.Windows;

namespace Wpf.Frno.SearchAndExtract.Utilities
{
    public class ScreenHelper
    {
        public static bool? Open<T>(object dataContext) where T : Window
        {
            Window window = default(T);
            if (window != null)
            {
                window.DataContext = dataContext;
                return window.ShowDialog();
            }
            return false;
        }
    }
}
