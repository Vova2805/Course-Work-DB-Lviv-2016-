   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      O KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters 5      @   :CourseWorkDB_DudasVI.Views.UserControls.ProductViewForSale   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q	сontrols;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatButton.xaml?�6%   %    5      .��5      �� 
�������
���iii�$��2q�6;   ._�5   
   �� $
��Auto=�6   .��5      �� $��Stretch=�6   $��Stretch=�65     5      *   $MahApps.Metro.Controls.NumericUpDown �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5      "  6    5      P� 5       �@ 
IsReadOnly . . �5   "   ��  �@ HideUpDownButtons . 5   )   5      5      5      .+�5      � $��Stretch=�6   $��Stretch=�63   	 � ColumnDefinitions	 5      �� 5       $
��auto �6+   �� 5!      $��* �6+   
 � RowDefinitions
 5#      �� 5$      �� 5%      .+�5'      �� $��0��6    $��1��6-   $��5q�6=   $+��%Детальна інформація��6H   $��20(�6c   � 5(      $��0��6   $��0��6+   50      �� 51       �� Orientation$ Vertical=�6!   $��Stretch=�68   $��1��6T   $��1��6d   $��2��52   !   $��5q�62   .+�54      9� $)��#Загальні відомості��6#    9� 
IsExpanded . .��55      �   56   "   A  ;CourseWorkDB_DudasVI.Converters.PackageDescriptionConverter   DescriptionConverter��%   &	          57   &   6M   58   #   
 59   "   �� 5:   &   $
��Auto �64   �� 5;   &   $
��Auto �64   �� 5<   &   $
��Auto �64   �� 5=   &   $
��auto �64   �� 5>   &   $
��Auto �64   �� 5?   &   $
��Auto �64   �� 5@   &   $
��Auto �64   	 5B   "   �� 5C   &   $	��120 �67   �� 5D   &   $��5 �67   �� 5E   &   $��* �67   .+�5G   "   �� $��1��6,   $��0��69   $��5q�6I   $!��Назва продукту��6T   �� 5H   "   $
��Bold"�6,   $
��Wrap=�6>   $��1��6R   $��2��6_   $��5,5,5,0q�5I   ,    =� ErrorTemplate 6=   Z� ��5J   ,   ��  �� Path$ ProductInfo.PRODUCT_TITLE��� 5K   "   $��2��6,   $��0��69   $��5,5,0,5q�6I   $��Категорія��6Z   �� 5L   "   $
��Bold"�6,   $��2��6>   $��2��6K   $��5,5,5,0q�5M   ,    6=   Z� ��5N   ,   �� $) #ProductInfo.CATEGORY.CATEGORY_TITLE��� 5O   "   $��6��6,   $��0��69   $��5q�6I   $��Ціна��6T   �� 5P   "   $
��Bold"�6,   $��6��6>   $��2��6K   $��5q�6[   ��5Q   ,   �� $ ProductPrice��� 5S   "   $��4��6,   $��0��69   $��5q�6I   $(��"Кількість в пакеті��6T   $��Center=�5T   ,   �� 5U   "   $��5��6,   $��0��69   $��5q�6I   $��Опис пакету��6T   $��Center=�5V   ,   �� 5W   "   $
��Bold"�6,   $��4��5X   ,   $��2��69   $��5q�5Y   ,   $��Center=�6[   ��5W   >   �� $% ProductInfo.QUANTITY_IN_PACKAGE� 5Y   7   Z� 6[   �� 5Z   "   $
��Bold"�6,   $��5��5\   ,   $��2��69   $��5q�5]   ,   $��Center=�6[   ��5[   ,   �� $% ProductInfo.PACKAGE_DESCRIPTION� �� 	Converter# [   5]   7   Z� 6[   �� 5_   "   $��3��6,   $��0��69   $��5q�6I   $��Опис��6T   $
��Wrap=�6`   $��Center=�5`   ,   �� 5a   "   $
��Bold"�6,   $��3��6>   $��2��5c   ,   $
��Wrap=�6<   $��Center=�6P   $��5q�6k   ��5b   ,   �� $% ProductInfo.PRODUCT_DESCRIPTION� 5d   ,   Z� 5f      5g      5i      5j      5k      5l      5m      5n      