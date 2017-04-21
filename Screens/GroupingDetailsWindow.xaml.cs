using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;

namespace Wpf.Frno.SearchAndExtract.Screens
{
    /// <summary>
    /// Interaction logic for GroupingDetailsWindow.xaml
    /// </summary>
    public partial class GroupingDetailsWindow : Window
    {
        public GroupingDetailsWindow()
        {
            InitializeComponent();

            DataContextChanged += GroupingDetailsWindow_DataContextChanged;
        }

        private void GroupingDetailsWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(DataContext != null && 
               DataContext is IFileSearch)
            {
                IFileSearch data = DataContext as IFileSearch;

                PropertyInfo[] properties = data.Files.ElementAt(0).GetType().GetProperties();
                List<TabItem> tabItems = new List<TabItem>();

                foreach (PropertyInfo property in properties)
                {
                    List<resulto> listItemData = data.Files
                        .GroupBy(x => x.GetType().GetProperty(property.Name).GetValue(x, null))
                        .Select(
                            g => new resulto
                            {
                                Name = g.Key.ToString(),
                                Count = g.Count()
                            })
                        .ToList();

                    var newGrid = new DataGrid()
                    {
                        ItemsSource = listItemData,
                        Tag = property.Name
                    };
                    newGrid.AutoGeneratingColumn += dataGridAutoGeneratingColumn;
                    tabItems.Add(new TabItem()
                    {
                        Header = property.Name,
                        Content = newGrid
                    });
                }

                tabControlMain.ItemsSource = tabItems;
            }
        }

        private void dataGridAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName.StartsWith("Name"))
              e.Column.Header = ((DataGrid)sender).Tag;
        }

        class resulto
        {
            public string Name { get; set; }
            public int Count { get; set; }
        }
    }
}
