   M S B A M L   `   `   `  ����T  PWpf.Frno.SearchAndExtract, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Wpf.Frno.SearchAndExtractWpf.Frno.SearchAndExtract  5   	   <    6Wpf.Frno.SearchAndExtract.Screens.SimpleListViewWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Wpf.Frno.SearchAndExtract     5   	     � Title$  Simple results view��5   	   $	��640��6%   $	��480��62    � 
ResizeMode$ 	CanResize=�6>    � WindowStartupLocation$ CenterOwner=�6U   .��5	      � .+�5
   
   ��  �� Orientation$ 
Horizontal=�6   $	��Top=�6.   $��0 10 0 0q�6F   $��35��6X   .+�5      �� -   5         ��Name$ labelResults��5      .��6*   
Results:64   �� 5      -   5        $ labelSelectionInfo��5      
�������6D   5      �� 5   
   -   5         ��Name$ buttonExportToCsv��5      $��Export to CSV��6,   $��85��6D   $��25��6O   $��Right=�6[   $	��Top=�6w   $��0,5,5,0q�6�   6�   �� 5   
   $ 
Horizontal=�6   $��0 30 0 0q�6.   $��26��6@   $	��Top=�6L   .+�5      �� .��6   Unique only for :6'   �� 5      -   5         ��Name$ ComboBoxUniqueForColumn��5       �� DisplayMemberPath$	 Key��6v    �� SelectedValuePath$ Value��6�   $	��120��6�   6�   �� 5      .��6   column6   5      &   System.Windows.Controls.DataGrid  5   
   -   5        	  Name$	 listViewResults��5      $��5 55 5 5q�5      $��Stretch=�5      $��Stretch=�5      
   GridLinesVisibility$

 None=�5         
IsReadOnly . ��5      �� *.+   DebugConverter �� 	Converter# [  5      5      5       