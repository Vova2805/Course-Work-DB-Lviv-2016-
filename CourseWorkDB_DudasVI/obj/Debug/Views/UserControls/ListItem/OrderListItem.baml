   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      J FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      ;    5CourseWorkDB_DudasVI.Views.UserControls.OrderListItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      .��5	      �� 
�������
������$��1,1,1,1q�6>   $��5q�6X   ._�5
   
   � $
��auto��6   $��5q�6     � RowDefinitions  5      �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $��* �6    �� 5      $
��auto �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   �� 5      $��* �6#   .+�5      ��  �� TextAlignment$ Center=�6   $��0��6/   $��0��6<   $��Оформив��6L   $��5q�6[   �� 5      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��0��6K   $��5q�5   B   ��6   ��  �� Path$ STAFF.STAFF_SURNAME�6B   �� 5      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��0��6K   $��5q�5   ?   ��6   �� $ STAFF.STAFF_NAME�6?   �� 5      $ Center=�6   $��0��6/   $��1��6<   $��Замовник��6L   $��5q�6\   �� 5       $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��1��6K   $��5q�5!   D   ��6   �� $ CLIENT.CLIENT_SURNAME�6D   �� 5"      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��1��6K   $��5q�5#   A   ��6   �� $ CLIENT.CLIENT_NAME�6A   �� 5%      $ Center=�6   $��0��6/   $��2��6<   $��Дати��6L   $��5q�6X   �� 5&      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��2��6K   $��5q�5'   9   ��6   �� $ 
ORDER_DATE�69   �� 5(      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��2��6K   $��5q�5)   <   ��6   �� $ REQUIRED_DATE�6<   �� 5+      $ Center=�6   $��0��6/   $��3��6<   $��Статус��6L   $��5q�6Z   �� 5,      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��3��6K   $��5q�5-      ��5,   [   �� $
 PAID�5-      �� 5.      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��3��6K   $��5q�5/   ;   ��6   �� $ ORDER_STATUS�6;   �� 50      $
��Bold"�6   $
��Wrap=�6*   $��3��6>   $��3��6K   $��5q�51   >   ��6   �� $ DELIVERY_STATUS�6>   �� 53      $ Center=�6   $��0��6/   $��4��6<   $��Загалом��6L   $��5q�6[   �� 54      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��4��6K   $��2��6[   
��� � �$��Center=�55   +   $��Center=�6F   $��5q�56   4   ��6   �� $ TOTAL�64   �� 57      $
��Bold"�6   $
��Wrap=�6*   $��3��6>   $��4��6K   
���  ��$��Center=�58      $��5q�6T   ��65   �� $ DISCOUNT�6T   9� 5:      $4��.Детальніше про замолення��6    9� 
IsExpanded .  $��0��6L   $��4��6\   $��5��5;      $��5q�6+   .��5<      �� .��5=      �� $��Stretch=�6      AccentColorBrush4#���   6:   ��5>      �� *ORDER_PRODUCT+��5?      �� 5@      .R�5A   "   B   <CourseWorkDB_DudasVI.Views.UserControls.ProductOrderListItem  5B      5D      5E      5F      5G      5H      5I      