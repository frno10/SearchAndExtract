using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Wpf.Frno.SearchAndExtract.IO;

namespace Wpf.Frno.SearchAndExtract.Extensions
{
    public enum SaveTrigger
    {
        Never,
        OnChange,
        OnAddItem
    }

    public class MemoryComboBox : ComboBox
    {
        private IList<string> _previouData;
        private readonly ISettings<string> _settings;

        public MemoryComboBox()
        {
            IsEditable = true;
            _settings = new Settings<string>();
            _previouData = new List<string>();
            this.Loaded += MemoryComboBox_Loaded;
            this.KeyUp += MemoryComboBox_KeyUp;
        }

        private void MemoryComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string text = ((ComboBox) sender).Text;

            if (!string.IsNullOrWhiteSpace(text) 
                && _previouData != null 
                && _previouData.Count > 0)
            {
                var newData = _previouData.Where(dataItem => 
                    CultureInfo.CurrentCulture.CompareInfo.IndexOf(dataItem, text, CompareOptions.IgnoreCase) >= 0).ToList();
                ItemsSource = newData;
                IsDropDownOpen = true;
            }
        }

        private void MemoryComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        #region DataFile

        public string DataFile
        {
            get { return (string)GetValue(DataFileProperty); }
            set
            {
                SetValue(DataFileProperty, value);
                UpdateSettings();
            }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataFileProperty =
            DependencyProperty.Register("DataFile", typeof(string), typeof(MemoryComboBox), new PropertyMetadata(string.Empty));

        #endregion DataFile

        #region SaveSettings

        public SaveTrigger SaveSettings
        {
            get { return (SaveTrigger)GetValue(SaveSettingsProperty); }
            set { SetValue(SaveSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WhatTriggersSaving.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaveSettingsProperty =
            DependencyProperty.Register("SaveSettings", typeof(SaveTrigger), typeof(MemoryComboBox), new PropertyMetadata(SaveTrigger.OnAddItem));

        #endregion SaveSettings

        public void AddItem(string value)
        {
            if (SaveSettings == SaveTrigger.OnAddItem)
            {
                SaveNewItem(value);
            }
        }

        public void AddCurrenytItem()
        {
            AddItem(this.Text);
        }

        private void SaveNewItem(string value)
        {
            if (_previouData.Contains(value))
            {
                _previouData.Remove(value);
            }
            _previouData.Insert(0, value);

            Save();
        }

        private void Save()
        {
            EnsureDataFileIsSet();
            if (!string.IsNullOrWhiteSpace(DataFile) 
                && _previouData.Count > 0)
            {
                _settings.Save(_previouData);
                this.ItemsSource = _previouData;
                //this.UpdateLayout();
            }
        }

        private void Load()
        {
            EnsureDataFileIsSet();
            
            var data = _settings.Load();

            if (data != null && data.Count > 0)
            {
                _previouData = data;
                this.ItemsSource = _previouData;
                this.SelectedIndex = 0;
            }
        }

        private void EnsureDataFileIsSet()
        {
            if (string.IsNullOrWhiteSpace(DataFile))
            {
                if (!string.IsNullOrWhiteSpace(Name))
                {
                    DataFile = string.Format("MemoryComboBox_{0}.data", Name);
                }
                else
                {
                    DataFile = "MemoryComboBox.data";
                }
            }
            UpdateSettings();
        }

        private void UpdateSettings()
        { 
            _settings.FileName = DataFile;
            _settings.Folder = Environment.CurrentDirectory;
        }
    }
}