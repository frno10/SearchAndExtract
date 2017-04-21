using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Wpf.Frno.SearchAndExtract.Data;
using Wpf.Frno.SearchAndExtract.ExportTools;
using Wpf.Frno.SearchAndExtract.FileSearchAndExtract;
using Wpf.Frno.SearchAndExtract.Screens;
using Wpf.Frno.SearchAndExtract.Search;
using Wpf.Frno.SearchAndExtract.Search.Interfaces;
using Wpf.Frno.SearchAndExtract.Utilities;
using Wpf.Frno.SearchAndExtract.ViewModels;

namespace Wpf.Frno.SearchAndExtract
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel(Application.Current.Dispatcher);
            DataContext = viewModel;
        }

        private void buttonPickItems_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null &&
                viewModel.FileSearch != null)
            {
                ComboBoxFolderToSearch.Text = FileAndFolderBrowsers.Browse(viewModel.FileSearch.SearchIn);
            }
        }

        private async void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxFolderToSearch.AddCurrenytItem();
            ComboBoxFilePattern.AddCurrenytItem();
            ComboBoxSearchString.AddCurrenytItem();
            ComboBoxSubfolderLevel.AddCurrenytItem();

            await viewModel.Search();
        }

        private void buttonExtractTags_Click(object sender, RoutedEventArgs e)
        {
            ScreenHelper.Open<SimpleListViewWindow>(XmlDocumentExtractor.ExtractTagsFromXmlFiles(viewModel.FileSearch, ComboBoxFolderToSearch.Text));
        }

        private void buttonShowLinkedFiles_Click(object sender, RoutedEventArgs e)
        {
            FileDataLinked file = listViewResults.SelectedItem as FileDataLinked;

            if (file != null &&
                file.LinksToFiles != null &&
                file.LinksToFiles.Count > 0)
            {
                var dataContext = file.LinksToFiles.GroupBy(x => x).Select(group => new { Name = group.Key, Count = group.Count() }).ToList();
                ScreenHelper.Open<SimpleListViewWindow>(dataContext);
            }
        }

        private void buttonShowLinkedFromFiles_Click(object sender, RoutedEventArgs e)
        {
            FileDataLinked file = listViewResults.SelectedItem as FileDataLinked;

            if (file != null &&
                file.LinkedFromFiles != null &&
                file.LinkedFromFiles.Count > 0)
            {
                ScreenHelper.Open<SimpleListViewWindow>(file.LinkedFromFiles.Select(x => new { Name = x }).ToList());
            }
        }

        private void buttonViewGroupingsTags_Click(object sender, RoutedEventArgs e)
        {
            ScreenHelper.Open<GroupingDetailsWindow>(viewModel.FileSearch);
        }
        
        private void buttonExportGrid_Click(object sender, RoutedEventArgs e)
        {
            DataGridToCsv datagridToCsv = new DataGridToCsv();
            datagridToCsv.TryExport(listViewResults);
        }

        protected void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;

            if(row.DataContext is IFileData)
            {
                string fullFilePath = ((IFileData)row.DataContext).GetFullFilePath(ComboBoxFolderToSearch.Text);

                if (File.Exists(fullFilePath))
                {
                    using (Process.Start("notepad.exe", fullFilePath))
                    { }
                }
            }
        }

        private void comboBoxSearchTypeSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                IFileSearch searchType = e.AddedItems[0] as IFileSearch;

                if (searchType != null)
                {
                    viewModel.FileSearch = searchType;
                }
            }
        }

        private void buttonGroupBySelectedColumn_Click(object sender, RoutedEventArgs e)
        {
            if (listViewResults.CurrentColumn != null)
            {
                string columnName = listViewResults.CurrentColumn.Header.ToString();

                //var selectedColumn = listViewResults.col
            }

            CollectionViewSource viewSource = new CollectionViewSource();
            viewSource.Source = viewModel.FileSearch.Files;
            viewSource.GroupDescriptions.Add(new PropertyGroupDescription("RootElement"));
        }

        private void buttonGetAspInfo_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, List<string>> partialsAssignments = new Dictionary<string, List<string>>();
            foreach (FileDataLayoutLinked file in viewModel.FileSearch.Files)
            {
                if (file.Partials != null && file.Partials.Any())
                {
                    foreach (string partial in file.Partials)
                    {
                        if (!partialsAssignments.ContainsKey(partial))
                        {
                            partialsAssignments.Add(partial, new List<string>());
                        }

                        if (!partialsAssignments[partial].Contains(file.GetFullFilePath(file.Folder)))
                        {
                            partialsAssignments[partial].Add(file.GetFullFilePath(file.Folder));
                        }
                    }
                }
            }

            foreach (var partialsDictionary in partialsAssignments)
            {
                sb.AppendLine("Partial: " + partialsDictionary.Key);
                foreach (string fileUsingPartial in partialsDictionary.Value)
                {
                    sb.AppendLine("   " + fileUsingPartial);
                }
            }

            sb.AppendLine();

            List<string> allPartials = new List<string>();
            foreach (FileDataLayoutLinked file in viewModel.FileSearch.Files)
            {
                sb.AppendLine("File: " + file.GetFullFilePath(file.Folder));

                string layouts = file.LinksToLayouts == null ? "none" : string.Join(" | ", file.LinksToLayouts);
                sb.AppendLine("   Layout: " + layouts);

                if (file.Partials != null && file.Partials.Any())
                {
                    sb.AppendLine("   Partials:");
                    foreach (string partial in file.Partials)
                    {
                        sb.AppendLine("     " + partial);
                        if (!allPartials.Contains(partial))
                        {
                            allPartials.Add(partial);
                        }
                    }
                }
            }

            sb.AppendLine();
            sb.AppendLine("Layouts Hierarchy");
            sb.AppendLine();
            string indent = "     ";
            FindSubLayout(indent, sb, ((LayoutsFilesSearch)viewModel.FileSearch).Layouts.FirstOrDefault().Key);
            
            sb.AppendLine();

            if(viewModel.FileSearch is LayoutsFilesSearch)
            foreach (KeyValuePair<string, List<string>> layout in ((LayoutsFilesSearch)viewModel.FileSearch).Layouts)
            {
                sb.AppendLine("Layout: " + layout.Key);

                if (layout.Value != null && layout.Value.Any())
                {
                    sb.AppendLine("   Files using layout:");
                    foreach (string fileUsingLayout in layout.Value)
                    {
                        sb.AppendLine("     " + fileUsingLayout + 
                            (allPartials.Contains(fileUsingLayout) 
                            ? " [partial]" 
                            : string.Empty));
                    }
                }
            }

            //ScreenHelper.Open<SimpleTextViewWindow>(sb.ToString());
            SimpleTextViewWindow stvw = new SimpleTextViewWindow();
            stvw.DataContext = sb.ToString();
            stvw.ShowDialog();
        }

        private void buttonViewList_Click(object sender, RoutedEventArgs e)
        {
            SimpleListViewWindow stvw = new SimpleListViewWindow();
            stvw.DataContext = ((RegexSimpleSearch)viewModel.FileSearch).Results;
            stvw.ShowDialog();

            //ScreenHelper.Open<SimpleListViewWindow>(((RegexSimpleSearch)viewModel.FileSearch).Results.Select(x => new { Name = x }).ToList());
        }

        private void FindSubLayout(string indent, StringBuilder sb, string currentLayout)
        {
            sb.AppendLine(indent + currentLayout);

            if (((LayoutsFilesSearch) viewModel.FileSearch).Layouts.ContainsKey(currentLayout))
            {
                foreach (string subLayout in ((LayoutsFilesSearch) viewModel.FileSearch).Layouts[currentLayout])
                {
                    FindSubLayout(indent + "   ", sb, subLayout);
                }
            }
        }
    }
}
