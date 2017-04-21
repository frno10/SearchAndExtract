using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Frno.SearchAndExtract.Data;

namespace Wpf.Frno.SearchAndExtract.Extensions
{
    public class ListViewExtension
    {
        public static readonly DependencyProperty MatrixSourceProperty =
            DependencyProperty.RegisterAttached("MatrixSource",
            typeof(DataMatrix), typeof(ListViewExtension),
                new FrameworkPropertyMetadata(null,
                    new PropertyChangedCallback(OnMatrixSourceChanged)));

        public static DataMatrix GetMatrixSource(DependencyObject d)
        {
            return (DataMatrix)d.GetValue(MatrixSourceProperty);
        }

        public static void SetMatrixSource(DependencyObject d, DataMatrix value)
        {
            d.SetValue(MatrixSourceProperty, value);
        }

        private static void OnMatrixSourceChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ListView listView = d as ListView;
            DataMatrix dataMatrix = e.NewValue as DataMatrix;

            listView.ItemsSource = dataMatrix;
            GridView gridView = listView.View as GridView;
            int count = 0;
            gridView.Columns.Clear();
            foreach (var col in dataMatrix.Columns)
            {
                gridView.Columns.Add(
                    new GridViewColumn
                    {
                        Header = col.Name,
                        DisplayMemberBinding = new Binding(string.Format("[{0}]", count))
                    });
                count++;
            }
        }
    }
}
