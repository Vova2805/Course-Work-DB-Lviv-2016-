   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      J FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5      S.clr-namespace:CourseWorkDB_DudasVI.Views.Rules CourseWorkDB_DudasVI.Views.Rules  5	      A    ;CourseWorkDB_DudasVI.Views.UserControls.ClientOrderListItem   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5      @rules.clr-namespace:CourseWorkDB_DudasVI.Views.Rules     5	      .��5      �� 
�������
������$��1,1,1,1q�6>   $��5q�6X   ._�5   
   � $
��auto��6   $��5q�6     � 	Resources  5         VisibilityConverter8   2CourseWorkDB_DudasVI.Converters.BooleanOrConverter  BooleanOrConverter  errorTemplateStyle ��Name J� IsHitTestVisible ��Name � AllowsTransparency � HorizontalOffset � VerticalOffset � PlacementTarget �� ElementName	 �� Path
 �� Mode �� Orientation � Points �� Stretch �� CornerRadius �� Padding��%�  &	        &	      &	 &     �� 5      6/     5      60   �� 5      .R�5      � .+�5      �� $ errorBorder��6�   ���	#FFCB2E2E$��1q�6=   ���	#11FF0000 .  6�   �� 5      $ placeholder��68   � 5       . $��Right=�6>   $ 0��6Z   $ 0��6o   $
��Fade=�6�   $��Right=�6�    5   $   �� $ errorBorder����6X   �� $ placeholder��$	 AdornedElement.IsFocused�$
 OneWay=�.��5   "   �� $ 
Horizontal=�6-   .+�5   &   � $��Center=�6/    �    	=  	=  	=  z ���	#FFCB2E2E$
 Fill=�6p   ���	#FFCB2E2E$��2��5   '   �� 5   &   ���	#FFCB2E2E$ 4��6D   $ 4q�6U   ._�5   *   �� $��Center=�64   
�������$
��Bold"�6d   $��2,0,0,0q�6v   ��5   4   �� $ placeholder��$8	 2AdornedElement.(Validation.Errors)[0].ErrorContent�$
 OneWay=�5   '   5   #   5      5      5       5!       � RowDefinitions 5"      �� 5#      $��* �6    �� 5$      $��* �6    �� 5%      $��* �6    �� 5&      $��* �6    �� 5'      $
��auto �6     � ColumnDefinitions 5)      �� 5*      $
��auto �6#   �� 5+      $
��auto �6#   �� 5,      $��* �6#   �� 5-      $
��auto �6#   �� 5.      $��* �6#   .+�50      ��  �� TextAlignment$ Center=�6   $��0��6/   $��0��6<   $��Оформив��6L   $��5q�6[   �� 51      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��0��6K   $��5q�52   L    � ToolTip$+ %Прізвище працівника��6W   ��6   �� $#	 SaleOrder.STAFF.STAFF_SURNAME�6W   �� 53      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��0��6K   $��5q�54   I   $" Ім'я працівника��6T   ��6   �� $ 	 SaleOrder.STAFF.STAFF_NAME�6T   �� 55      $
��Bold"�6   $
��Wrap=�6*   $��3��6>   $��0��6K   $��5q�56   M   $' !Посада працівника��6X   ��6   �� $$	 SaleOrder.STAFF.POST.POST_NAME�6X   �� 58      $ Center=�6   $��0��6/   $��2��6<   $��Дати��6L   $��5q�6X   �� 59      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��2��6K   $# Дата оформлення��5:   f   $��5q�5;      ��5:      �� $	 SaleOrder.ORDER_DATE� �� StringFormat$ {0:dd.MM.yyyy року}��5;      �� 5<      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��2��6K   $. (Дата кінця реалізації��5>      $��5q�68   ��5=      �� $	 SaleOrder.REQUIRED_DATE�$ {0:dd.MM.yyyy року }��5>   8   �� 5@      $ Center=�6   $��0��6/   $��3��6<   $��Статус��6L   $��5q�6Z   �� 5A      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��3��6K   $��5q�5B      $ Оплачено��6#   ��5A   [   �� $
	 Paid�5B   #   � 5C      $��1��6   $��3��6    y�61   �� $	 IsSalerReal� �� 	Converter# [   �� ConverterParameter$ 2�� 5D      �� 5E      $��* �6'   �� 5F      $
��Auto �6'   .+�5H      *  $MahApps.Metro.Controls.NumericUpDown  $��0��6)      Minimum$ 0Q�69   $
��auto��6E   $��2q�5I   9      Interval$ 1Q�5J   9   $��1q�5K   9    =� ErrorTemplate# [ 6M      Value 5L      �� 5M      $
	 Paid�6"   $
 TwoWay=�6.    �� UpdateSourceTrigger$ PropertyChanged=�6<    �� ValidatesOnExceptions .  �� ValidationRules 5N      1   +CourseWorkDB_DudasVI.Views.Rules.NumberRule  5O   "   5Q      5S      �� 5T      $��30��6   $��1��6$   $��1q�64   $��30��5U   )   $��Center=�5V   )   $2 ,Скасувати останню зміну��5W   )     ProgressBrush#���  6K     MetroCircleButtonStyle#���  5X   )   ��5T   ?   �� *CanselPaidChange+.��5Y      �� $��15��6    $��15��6+   ��5Z      �� 5[       �� 
TargetType" ��6$   .�5\   "   ��  �� Property � ��  �� Value  5]   &   !� 5^   *   ! j� Stretch$
! Fill=�66     appbar_cancel" !� Visual#" [ 5_   B   5a   #   # �� Triggers# 5b   "   P� 5c   &   $ P� Property$ �@J�IsMouseOver% P� Value% . . �5d   *   ��  � ��  5e   .   !� 5f   2   	Brush��$
! Fill=�6M     appbar_cancel_white#" [ 5g   J   5i   +   5j   '   5l      5n      5o      5p      �� 5r      $
��Bold"�6   $
��Wrap=�6*   $��2��6>   $��3��6K   $��5q�5s   :   $' !Статус замовлення��6E   ��6   �� $	 OrderStatus�6E   �� 5t      $��2��6   $��3��6$   $��2q�5v   :   $4 .Зміна статусу замовлення��6E   ��5t   4   �� $	 OrderStatusStrings�& �� SelectedItem& 6d   �� $	 OrderStatus�y�5u      �� $	 IsSaler�# [  $ 2��' �� Text' 5v      �� $	 OrderStatus�6E   �� 5w      $
��Bold"�6   $
��Wrap=�6*   $��3��6>   $��3��6K   $��5q�5x   =   $# Статус доставки��6H   ��6   �� $	 DeliveryStatus�6H   �� 5z      $ Center=�6   $��0��6/   $��4��6<   $��Загалом��6L   $��5q�6[   �� 5{      $
��Bold"�6   $
��Wrap=�6*   $��1��6>   $��4��6K   $��2��6[   
��� � �$��Center=�5|   +   $��Center=�6F   $��5q�5}   4   $< 6Загальна вартість замовлення��6?   ��6   �� $	 Total�6?   �� 5~      $
��Bold"�6   $
��Wrap=�6*   $��3��6>   $��4��6K   
���  ��$��Center=�5      $ Знижка��6T   $��5q�6e   ��65   �� $	 Discount�6e   5�      5�      5�      