   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      >    8CourseWorkDB_DudasVI.Views.UserControls.ScheduleListItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      .��5      �� 
�������
������$��1,1,1,1q�6>   $��5q�6X   ._�5	   
   � $
��auto��6   $��5q�6     � RowDefinitions  5
      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      ��  �� TextAlignment$ Center=�6   $��0��6/   $��0��6<   $��Оформив��6L   $��5q�6[   �� 5      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��1��6[   $��0��6h    � ToolTip$+ %Прізвище працівника��5      $��5q�6`   ��66   ��  �� Path$ STAFF.STAFF_SURNAME�6`   �� 5      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��2��6[   $��0��6h   $" Ім'я працівника��5      $��5q�6Y   ��62   �� $ STAFF.STAFF_NAME�6Y   �� 5      $��Center=�6   $ Center=�65   $��0��6L   $��1��6Y   $��Дати��6i   $��5q�5      �� 5      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $( "Дата подання плану��6[   $��1��5      $��1��6%   $��5q�6X   ��65   �� $ CREATED_DATE�6X   �� 5       $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $9 3Дата кінця реалізації плану��5!      $��2��6>   $��1��6K   $��5,0,5,5q�5"   <   $��16��6M   $��Bottom=�6Y   ��6   �� $ REQUIRED_DATE�6Y   �� 5$      $��Center=�6   $ Center=�65   $��0��6L   $��2��6Y   $��Статус��6i   $��5q�5%      �� 5&      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��1��6[   $��2��6h   $' !Статус замовлення��5'      $��2��6Y   $��Center=�5(      $��5q�63   ��5'   4   �� $ SCHEDULE_STATE�5(   3   �� 5*      $��Center=�6   $ Center=�65   $��0��6L   $��3��6Y   $��Загалом��6i   $��5q�5+      �� 5,      $��Center=�6   $
��Bold"�65   $
��Wrap=�6G   $��1��6[   $��3��6h   $��2��5-      
��� � �$��Center=�6<   $: 4Загальна вартість продукції��5.      $��5q�6c   ��6>   �� $ SCHEDULE_TOTAL�6c   9� 50      $A��;Детальніше про план виробництва��6    9� 
IsExpanded .  $��0��6S   $��3��6c   $��4��51      $��5q�6+   .��52      �� .��53      �� $��Stretch=�6      AccentColorBrush4#���   6:   ��54      �� *SCHEDULE_PRODUCT_INFO+��55      �� 56      .R�57   "   E   ?CourseWorkDB_DudasVI.Views.UserControls.ProductScheduleListItem  58      5:      5;      5<      5=      5>      5?      