   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      S.clr-namespace:CourseWorkDB_DudasVI.Views.Rules CourseWorkDB_DudasVI.Views.Rules  5	      =    7CourseWorkDB_DudasVI.Views.UserControls.ProductListItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      @rules.clr-namespace:CourseWorkDB_DudasVI.Views.Rules     5	      J FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5
      .��5      �� 
������$��1,1,1,1q�6&   $��Stretch=�6@   $��Stretch=�5      $
��Auto��6)   ��5   
   �� 5        �� 
TargetType"  ��6    �� Triggers 5      �� 5       �� Binding 6"   �� *
IsBooked+ �� Value True6?   .a�5      ��  �� Property � �� �� Value
 ����67   �� 5       �@�ToolTip$* $У плані виробництва��64   5      �� 5       6"   �� *
IsBooked+	 False6?   .a�5      ��  � ��
 �����67   �� 5       �@�ToolTip$  ��64   5      5       � 	Resources 5   
   ;  5CourseWorkDB_DudasVI.Converters.ProductPriceConverter   PriceConverter9  3CourseWorkDB_DudasVI.Converters.VisibilityConverter  VisibilityConverter��%:   &	        &	        5      6/     5      6-   5      ._�5    
   � $
��Auto��6    � RowDefinitions 5!      �� 5"      $
��Auto �6    �� 5#      $
��Auto �6    �� 5$      $��* �6     � ColumnDefinitions 5&      �� 5'      $
��auto �6#   �� 5(      $��5 �6#   �� 5)      $
��auto �6#   �� 5*      $��5 �6#   �� 5+      $��* �6#   .+�5-      �� $��0��6   $��0��6!   $��3��61   $��50��6B   $��50��6N   $7��1pack://application:,,,/Resources/images/sweet.png��5.      $��Center=�6O   �� 5/      $
��Bold"�6   $��0��6*   	 � ToolTip$	 Назва товару��67   $��Center=�6N   $
��Wrap=�50      $��2��6,   $	��200��6X   ��6<   �� 
 �� Path$
 Title�.  (System.Windows.Interactivity.Interaction   	Behaviors 51      <   6CourseWorkDB_DudasVI.Views.Rules.TextHighlightBehavior  52         
Background
 ��  �   Text 62   �� *ChangedText+ �� Mode$ OneWayToSource=�6d   54      �� 55      $
��Bold"�6   $��1��6*   $��2��67   $#	 Назва категорії��6G   $��Center=�56       �� TextAlignment$
 Left=�63   $
��Left=�6H   ��57      �� $
 CategoryTitle� 58        59      
 ��  � 62   �� *ChangedText+$ OneWayToSource=�6d   5;      � 5<      $��1��6   $��4��6    $��5q�60   y�5=      �� $
 IsNumbersVisible� �� 	Converter# [  5>      �� 5?      �� 5@      $��5 �6$   �� 5A      .+�5C      �� $
��Bold"�6   $��0��6.   
��� � �$
��Left=�6N   $*	 $Кількість на складі��5E      $ Center=�6:   ��5D      �� $
 Quantity� �� UpdateSourceTrigger$ PropertyChanged=�5E   :   *  $MahApps.Metro.Controls.NumericUpDown  5F      $7	 1Бажана кількість на складі��6)   $��2��5G   )      Minimum$ 0Q�66      Interval$ 1Q�6B      Value 5H   )   �� $
 QuantityNeeded�$ TwoWay=�$ PropertyChanged=�5J      � 5L      $��2��6   $��2��6    $��2��60    5M      �� 5N      $
��auto �6'   �� 5O      $��5 �6'   �� 5P      $��* �6'   .+�5R      �� $
��Bold"�6   $��0��6.   
��� � �$
��Left=�6Q   $4	 .Остання встановлена ціна��5T      ��5S      �� $!
 ProductInfo.PRODUCT_INFO_ID�# [   �� ConverterParameter$ $��5T      �� 5U      $
��Bold"�6   $��2��6.   
��� � �$
��Left=�6Q   $J	 DУ відсотках від вартості виробництва��5X      y�5V      �� $
 	IsntSaler�# [ ��5W      �� $!
 ProductInfo.PRODUCT_INFO_ID�# [  $ %��5X      5Y      5Z      5[      5\      