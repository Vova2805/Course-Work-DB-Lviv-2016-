   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5      =    7CourseWorkDB_DudasVI.Views.UserControls.UserListElement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5      .��5	      �� 
�������
������$��1,1,1,1q�6>     � 	Resources  5
   
   9  3CourseWorkDB_DudasVI.Converters.StaffImageConverter   	Converter��%   &	          5      6-   5      ._�5   
   � $
��auto��6    � RowDefinitions 5      �� 5      $��* �6    �� 5      $��5 �6    �� 5      $��* �6    �� 5      $��5 �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��5 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6!   $��3��61   $��50��6B   $��50��6N   $��Center=�5   �   ��6   ��  �� Path$
 Post� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� 	Converter# [  6�   �� 5      $��0��6   $��2��6&   $��Center=�66   .+�5      �  5      �� 5      $
��Auto �6+   �� 5       $��5 �6+   �� 5!      $��* �6+   .+�5#      �� $��0��6J   ��6    �� $ Employee.STAFF_NAME�6J   �� 5$       �� TextAlignment$
 Left=�6    $��2��6b   ��65   �� $ Employee.STAFF_SURNAME�6b   5%      5&      �� 5'      $��2��6   $��2��6&   $��Center=�66   .+�5(      �  5)      �� 5*      $
��Auto �6+   �� 5+      $��5 �6+   �� 5,      $��* �6+   .+�5.      �� $��0��6Y    � ToolTip$, &Зарплата у відсотках��6i   ��6    �� $ 
FullSalary�	 �� StringFormat$	 {0:F0}%��6i   �� 5/      $
 Left=�6    
���/kU�$��2��6�   $&  Зарплата у валюті��6�   ��6Q   �� $ MoneySalary�$	 {0:F0} грн.��6�   50      51      �� 52      -   5        
 ��Name$

 post��52   *   $
��Bold"�6   $��4��68   $��2��6E   ��53      �� $ Post.POST_NAME�$ TwoWay=�$ PropertyChanged=�54      55      57      