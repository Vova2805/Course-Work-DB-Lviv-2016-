   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      H    BCourseWorkDB_DudasVI.Views.UserControls.ProductionScheduleListItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      .��5      �� 
�������
������$��1,1,1,1q�6>   $��5q�6X   ._�5	   
   � $
��auto��6   $��5q�6     � RowDefinitions  5
      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      �� $��Center=�6    �� TextAlignment$ Center=�65   $��0��6L   $��1��6Y   $��Дати��6i   $��5q�5      �� 5      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G    � ToolTip$( "Дата подання плану��6[   $��1��5      $��1��6%   $��5q�6X   ��65   ��  �� Path$ CREATED_DATE�6X   �� 5      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $9 3Дата кінця реалізації плану��5      $��2��6>   $��1��6K   $��5,0,5,5q�5   <   $��16��6M   $��Bottom=�6Y   ��6   �� $ REQUIRED_DATE�6Y   �� 5      $��Center=�6   $ Center=�65   $��0��6L   $��2��6Y   $��Статус��6i   $��5q�5       �� 5!      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��1��6[   $��2��6h   $0 *Статус виконання плану��5"      $��2��6^   $��Center=�5#      $��5q�63   ��5"   9   �� $ SCHEDULE_STATE�5#   3   �� 5%      $��Center=�6   $ Center=�65   $��0��6L   $��3��6Y   $��Загалом��6i   $��5q�5&      �� 5'      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��1��6[   $��3��6h   $��2��5(      
��� � �$��Center=�6<   $: 4Загальна вартість продукції��5)      $��5q�6c   ��6>   �� $ SCHEDULE_TOTAL�6c   5*      5+      5,      