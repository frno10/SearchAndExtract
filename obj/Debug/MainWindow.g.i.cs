﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8CB49BBA12E9A229535AF39E13C0AAE3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DataGridExtensions;
using DataGridExtensions.Behaviors;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Wpf.Frno.SearchAndExtract;
using Wpf.Frno.SearchAndExtract.Extensions;


namespace Wpf.Frno.SearchAndExtract {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 26 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label textBlock;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox ComboBoxFolderToSearch;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonPickItems;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSearch;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelStatus;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelResultsCount;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTime;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox ComboBoxFilePattern;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox ComboBoxSubfolderLevel;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox ComboBoxSearchString;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxSearchTypeSelection;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonViewList;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonGetAspInfo;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonGroupBySelectedColumn;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonExtractTags;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonExportGrid;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonShowLinkedFiles;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonShowLinkedFromFiles;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid listViewResults;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTotalResultItemsCount;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCurrentlySelectedItemsCount;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelFilteredItemsCount;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Wpf.Frno.SearchAndExtract;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textBlock = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.ComboBoxFolderToSearch = ((Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox)(target));
            return;
            case 3:
            this.buttonPickItems = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\MainWindow.xaml"
            this.buttonPickItems.Click += new System.Windows.RoutedEventHandler(this.buttonPickItems_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonSearch = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\MainWindow.xaml"
            this.buttonSearch.Click += new System.Windows.RoutedEventHandler(this.buttonSearch_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.labelStatus = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.labelResultsCount = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.labelTime = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.ComboBoxFilePattern = ((Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox)(target));
            return;
            case 9:
            this.ComboBoxSubfolderLevel = ((Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox)(target));
            return;
            case 10:
            this.ComboBoxSearchString = ((Wpf.Frno.SearchAndExtract.Extensions.MemoryComboBox)(target));
            return;
            case 11:
            this.comboBoxSearchTypeSelection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 49 "..\..\MainWindow.xaml"
            this.comboBoxSearchTypeSelection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBoxSearchTypeSelection_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.buttonViewList = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\MainWindow.xaml"
            this.buttonViewList.Click += new System.Windows.RoutedEventHandler(this.buttonViewList_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.buttonGetAspInfo = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\MainWindow.xaml"
            this.buttonGetAspInfo.Click += new System.Windows.RoutedEventHandler(this.buttonGetAspInfo_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.buttonGroupBySelectedColumn = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\MainWindow.xaml"
            this.buttonGroupBySelectedColumn.Click += new System.Windows.RoutedEventHandler(this.buttonViewGroupingsTags_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.buttonExtractTags = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\MainWindow.xaml"
            this.buttonExtractTags.Click += new System.Windows.RoutedEventHandler(this.buttonExtractTags_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.buttonExportGrid = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\MainWindow.xaml"
            this.buttonExportGrid.Click += new System.Windows.RoutedEventHandler(this.buttonExportGrid_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.buttonShowLinkedFiles = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\MainWindow.xaml"
            this.buttonShowLinkedFiles.Click += new System.Windows.RoutedEventHandler(this.buttonShowLinkedFiles_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.buttonShowLinkedFromFiles = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\MainWindow.xaml"
            this.buttonShowLinkedFromFiles.Click += new System.Windows.RoutedEventHandler(this.buttonShowLinkedFromFiles_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.listViewResults = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 21:
            this.labelTotalResultItemsCount = ((System.Windows.Controls.Label)(target));
            return;
            case 22:
            this.labelCurrentlySelectedItemsCount = ((System.Windows.Controls.Label)(target));
            return;
            case 23:
            this.labelFilteredItemsCount = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 20:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 88 "..\..\MainWindow.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.Row_DoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
