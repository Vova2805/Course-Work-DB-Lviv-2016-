   M S B A M L   `   `   `  ����J  FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      O KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters 5      9   3CourseWorkDB_DudasVI.Views.UserControls.ProductView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Q	сontrols;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5        � 	Resources  5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $K Epack://application:,,,/MahApps.Metro;component/Styles/FlatButton.xaml?�6%   %    5      .��5      �� ._�5   
   �� $
��Auto=�6   .��5      �� $��Stretch=�6   $��Stretch=�65     5      *   $MahApps.Metro.Controls.NumericUpDown �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '        �� 5      "  6    5      P� 5       �@ 
IsReadOnly . . �5   "   ��  �@ HideUpDownButtons . 5   )   5      5      5      .+�5      � $��Stretch=�6   $��Stretch=�63   	 � ColumnDefinitions	 5      �� 5       $
��auto �6+   �� 5!      $��* �6+   
 � RowDefinitions
 5#      �� 5$      �� 5%      .+�5'      �� $��0��6    $��1��6-   $��5q�6=   $A��;Детальна інформація про продукт��6H   $��15(�5(       � 5)      $��0��6   $��0��6+   51      �� 52       �� Orientation$ Vertical=�6!   $��Stretch=�68   $��1��6T   $��1��6d   $��5q�53   !   .+�55      9� $)��#Загальні відомості��6#    9� 
IsExpanded . .��56      �   57   "   ; 5CourseWorkDB_DudasVI.Converters.ProductPriceConverter   PriceConverter��%   &	          58   &   6G   59   #   
 5:   "   �� 5;   &   $
��Auto �64   �� 5<   &   $
��Auto �64   �� 5=   &   $
��Auto �64   �� 5>   &   $
��auto �64   �� 5?   &   $
��Auto �64   �� 5@   &   $
��Auto �64   �� 5A   &   $
��Auto �64   �� 5B   &   $
��Auto �64   	 5D   "   �� 5E   &   $	��140 �67   �� 5F   &   $��5 �67   �� 5G   &   $��* �67   .+�5I   "   �� $��1��6,   $��0��69   $��5q�6I   $!��Назва продукту��6T   �� 5J   "   $
��Bold"�6,   $��1��6>   $��2��6K   $
��Wrap=�6[   $��5,5,5,0q�5K   ,   ��5L   ,   ��  �� Path$/ )SelectedProduct.ProductInfo.PRODUCT_TITLE��� 5M   "   $��2��6,   $��0��69   $��5,5,0,5q�6I   $��Категорія��6Z   �� 5N   "   $
��Bold"�6,   $��2��6>   $��2��6K   $��5,5,5,0q�5O   ,   ��5P   ,   �� $9 3SelectedProduct.ProductInfo.CATEGORY.CATEGORY_TITLE��� 5Q   "   $��5��6,   $��0��69   $��5q�6I   $%��Ціна виробництва��6T   �� 5R   "   $
��Bold"�6,   $��5��6>   $��2��6K   $��5,5,5,0q�6[   ��5S   ,   �� $2 ,SelectedProduct.ProductInfo.PRODUCTION_PRICE��� 5T   "   $��6��6,   $��0��69   $��5q�6I   $��Ціна продажу��6T   � 5U   "   $��6��6'   $��2��64   	 5V   &   �� 5W   *   $
��auto �6;   �� 5X   *   $��60 �6;   
 5Z   &   �� 5[   *   $
��Auto �68   �� 5\   *   $
��Auto �68   .+�5^   &     -   5           Minimum$ 0Q�5^   =   $
��auto��6I   $��0��6V      Interval$ 1Q�6f   $��1q�5`   =   $��5,0,0,5q�6Q      Value 5_   =   �� $ ProductPriceValue� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5a   =   �� 5b   &   $��1��60   $�� грн��6@   $��Center=�6L     5c   &   -   5        $ 0Q�5c   =   $
��auto��6I   $��0��6V   $��1��6f   $ 1Q�5d   =   $��1q�5f   =   $��5,0,0,5q�5g   =    5e   =   �� $ ProductPricePersentage�$ TwoWay=�$ PropertyChanged=�5g   N   �� 5h   &   $��1��60   $��1��6@   $�� % націнки��6M   $��Center=�5i   0   $
��Left=�5j   0   5k   #   �� 5l   "   $��4��6,   $��0��69   $��5q�6I   $(��"Кількість в пакеті��6T   $��Center=�5m   ,   �� 5n   "   $
��Bold"�6,   $��4��5p   ,   $��2��69   $��5q�5q   ,   $��Center=�67   ��5o   ,   �� $5 /SelectedProduct.ProductInfo.QUANTITY_IN_PACKAGE�5q   7   �� 5s   "   $��3��6,   $��0��69   $��5q�6I   $��Опис��6T   $
��Wrap=�6`   $��Center=�5t   ,   �� 5u   "   $
��Bold"�6,   $��3��6>   $��2��5w   ,   $
��Wrap=�6<   $��Center=�6P   $��5q�6k   ��5v   ,   �� $5 /SelectedProduct.ProductInfo.PRODUCT_DESCRIPTION�5w   k   � 5x   "   $��7��6'   $��0��64   $��3��6D   	 5y   &   �� 5z   *   �� 5{   *   .+�5}   &   �� $��1��6-   $��5q�6=   $��Center=�6H   $(��"Зберегти нову ціну��5~   -   ��6J   �� $ SubmitPriceChanges��� 5   &   $��0��6-   $��5q�6=   $��Center=�6H   $#��Скасувати зміни��5�   -   ��6G   �� $ CanselPriceChanges�5�   #   5�      5�      9� 5�      $��Зміни ціни��6#    . .��5�      �� .��5�   "   &   System.Windows.Controls.DataGrid  -   5        $��	PriceGrid��5�   +      AutoGenerateColumns .     CanUserResizeColumns . ��6<   �� $ ProductPriceList�   Columns 5�   &   0  *System.Windows.Controls.DataGridTextColumn  5�   *   ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6=      Header$ Дата зміни��6J      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 5�   =   �� $ ProductPrice.CHANGED_DATE� �� StringFormat$ {0:MM-dd-yyyy o H:mm:ss}��  5�   *   $
 Auto 6=   $ Значення грн.��6J    .  5�   =   �� $ ProductPrice.PRICE_VALUE�$ {0:N}��  5�   *   $
 Auto 6=   $ Значення %��6J    .  5�   =   �� $# ProductPrice.PERSENTAGE_VALUE�$ {0:N}��4  .System.Windows.Controls.DataGridTemplateColumn  5�   *   $ Змінив��6A    .    CellTemplate 5�   .   �� 5�   2   .R�5�   6   �� $ 
Horizontal=�6A   .+�5�   :   �� $��0,0,5,0q�5�   D   ��5�   D   �� $ Staff.STAFF_NAME�5�   D   �� 5�   :   ��6D   �� $ Staff.STAFF_SURNAME�5�   7   5�   3   5�   +     5�   *   $ Посада��6A    .  5�   .   �� 5�   2   .R�5�   6   �� $ 
Horizontal=�6A   .+�5�   :   �� $��5,0,5,0q�5�   D   ��5�   D   �� $ Post.POST_NAME�5�   D   5�   7   5�   3   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      