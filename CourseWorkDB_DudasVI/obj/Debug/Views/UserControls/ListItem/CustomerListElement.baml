   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5      A    ;CourseWorkDB_DudasVI.Views.UserControls.CustomerListElement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5      .��5	      �� 
�������
������$��1,1,1,1q�6>     � 	Resources  5
   
   9  3CourseWorkDB_DudasVI.Converters.StaffImageConverter   	Converter��%   &	          5      6-   5      ._�5   
   � $
��auto��6   $��5q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��5 �6#   �� 5      $��* �6#   .+�5      �� $��0��6   $��0��6!   $��3��61   $��50��6B   $��50��6N   $6��0pack://application:,,,/Resources/images/user.png��5      $��Center=�6N   �� 5      $��0��6   $��2��6&   $��Center=�66   .+�5      �  5      �� 5      $
��Auto �6+   �� 5      $��5 �6+   �� 5       $
��Auto �6+   �� 5!      $��5 �6+   �� 5"      $��* �6+   .+�5$      �� $��2��6I    � ToolTip$ Ім'я клієнта��6Y   ��6    ��  �� Path$ Client.CLIENT_NAME�6Y   �� 5%       �� TextAlignment$
 Left=�6    $��0��6a   $% Прізвище клієнта��5&       ��5%   5   �� $ Client.CLIENT_SURNAME�5&       �� 5'      $
 Left=�6    $��4��6e   $* $По-батькові клієнта��5(       ��5'   5   �� $ Client.CLIENT_MIDDLE_NAME�5(       5)      5*      �� 5+      $
��Bold"�6   $��1��6*   $��2��67   $! Назва компанії��5,   C   ��6   �� $ Client.COMPANY_TITLE�6C   �� 5-      $
��Bold"�6   $��2��6*   $��2��67   $' !Мобільний телефон��5.   B   $��5q�6^   ��6   �� $ Client.MOBILE_PHONE�6^   �� 5/      $
��Bold"�6   $��3��6*   $��2��67   $' !Електронна адреса��50   ;   ��6   �� $ Client.EMAIL�6;   51      52      54      