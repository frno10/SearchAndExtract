using System.Windows;

namespace Wpf.Frno.SearchAndExtract.Screens
{
    /// <summary>
    /// Interaction logic for SimpleTextViewWindow.xaml
    /// </summary>
    public partial class SimpleTextViewWindow : Window
    {
        public SimpleTextViewWindow()
        {
            InitializeComponent();

            DataContextChanged += SimpleTextViewWindow_DataContextChanged;
        }

        private void SimpleTextViewWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext != null &&
                DataContext is string)
            {
                textViewResults.Text = (string)DataContext;
            }
        }
    }
}
