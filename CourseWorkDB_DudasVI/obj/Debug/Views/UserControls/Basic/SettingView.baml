   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      ? ;Apex, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;)clr-namespace:Apex.Controls;assembly=ApexApex.Controls 5      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5      9    3CourseWorkDB_DudasVI.Views.UserControls.SettingView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      :apex)clr-namespace:Apex.Controls;assembly=Apex     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5        � 	Resources  5
      <   6CourseWorkDB_DudasVI.Converters.StringToColorConverter   stringToColorConv �� 
TargetType �� Property �� Value  AccentColorComboItemStyle �� 
TargetType �� Orientation �� 	Converter �� ConverterParameter  ThemeColorComboItemStyle  
BlackBrush�� 5   
   %�  &	        '��      &	 ~     7   &	     7     5      60   �� 5      " ��6   .�5      ��  � ��$ 14(�6-   �� 5       � ��$ 5q�6+   5      �� 5      " ��6   .�5      ��  � ��$ Stretch=�6?   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $ 
Horizontal=�6)   
������ .+�5   "   A� $��14��6*   $��14��66   $��2q�5   *   ��5   *   �� 8   $ ConvertToAccentColor��5   *   �� 5   "   $��14(�6=   $��2q�6K   ��6,   �� 6K   5      5      5       5!      �� 5#      " ��6   .�5$      ��  � ��$ Stretch=�6?   �� 5%       � �� 5&      �� 5'      " ��6*   .R�5(      �� $ 
Horizontal=�6)   
������ .+�5)   "   A� $��14��6*   $��14��66   #���  6A   $��2q�5+   *   ��5*   *   �� 8   $ ConvertToBaseColor��5+   *   �� 5,   "   $��14(�6=   $��2q�6K   ��6,   �� 6K   5-      5.      50      51      52      .��54      � 
������ $��5q�6$    � RowDefinitions 55   
   �� 56      $
��auto �6   �� 57      $
��auto �6   �� 58      $
��auto �6   �� 59      $
��auto �6   �� 5:      $��15 �6   �� 5;      $
��auto �6   �� 5<      $
��auto �6   �� 5=      $
��auto �6   �� 5>      $
��auto �6   �� 5?      $
��auto �6   	 � ColumnDefinitions	 5A   
   �� 5B      $
��auto �6   �� 5C      $
��auto �6   .+�5F   
   �� $��0��6   $��0��6!   $��2��61   $��18(�6E   $!��Змінити пароль��6S   �� 5H   
   $��1��6   $��0��6!   $��20,5,5,5q�61   $��Старий пароль��6C   G� 5I   
   $��1��6   $��1��6#   $	��200��63   $��2q�6?   �� 5K   
   $��2��6   $��0��6!   $��20,5,5,5q�61   $��Новий пароль��6C   G� 5L   
   $��2��6   $��1��6#   $	��200��63   $��2q�6?   �� 5N   
   $��3��6   $��0��6!   $��20,5,5,5q�61   $��Новий пароль��6C   G� 5O   
   $��3��6   $��1��6#   $	��200��63   $��2q�5P      �� 5R   
   $��Підтвердити��6   $��5��6'   $��1��64   $��5q�6D   $��Right=�6O   $��50��6k   ��5S      �� *ChangePasswordCommand+�� 5U   
   $��6��6   $��0��6!   $��2��61   $��18(�6E   $��Вигляд��6S   �� 5W   
   $��7��6   $��0��6!   $��20,5,5,5q�61   $��Теми��6C   �� 5X   
   $��7��6   $��1��6    $��2q�60   #��[ 5Y   :   
 �� SelectedItem
 5X   ;   �� *SelectedTheme+��5Y      �� *ThemeColorlist+6:   �� 5[   
   $��8��6   $��0��6!   $��20,5,5,5q�61   $��Кольори��6C   �� 5\   
   $��8��6   $��1��6    $��2q�60   #��[ 5^      
 5\   ;   �� *SelectedAccent+��5]      �� *AccentColorlist+5^      5`      5a      