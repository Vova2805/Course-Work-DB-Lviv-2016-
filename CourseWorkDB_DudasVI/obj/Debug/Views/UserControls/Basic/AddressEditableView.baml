   M S B A M L   `   `   `  ����O  KCourseWorkDB_DudasVI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulla5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls'CourseWorkDB_DudasVI.Views.UserControls  5      J FMahApps.Metro, Version=1.1.3.229, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      S.clr-namespace:CourseWorkDB_DudasVI.Views.Rules CourseWorkDB_DudasVI.Views.Rules  5	      Q-clr-namespace:CourseWorkDB_DudasVI.ConvertersCourseWorkDB_DudasVI.Converters  5
      A    ;CourseWorkDB_DudasVI.Views.UserControls.AddressEditableView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Glocal5clr-namespace:CourseWorkDB_DudasVI.Views.UserControls     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      @rules.clr-namespace:CourseWorkDB_DudasVI.Views.Rules     5	      D
converters-clr-namespace:CourseWorkDB_DudasVI.Converters     5
      .��5      � $��1��6   $��0��6   $��2��6(   $��Stretch=�69     � 	Resources  5   
   8   2CourseWorkDB_DudasVI.Converters.BooleanOrConverter   BooleanOrConverter  errorTemplateStyle ��Name J� IsHitTestVisible ��Name � AllowsTransparency � HorizontalOffset � VerticalOffset � PlacementTarget �� ElementName	 �� Path
 �� Mode �� Orientation � Points �� Stretch �� CornerRadius �� Padding��%m  &	        &	        5      6,   �� 5      .R�5      � .+�5      �� $ errorBorder��6y   ���	#FFCB2E2E$��1q�65   ���	#11FF0000 .  6y   �� 5      $ placeholder��60   � 5       . $��Right=�66   $ 0��6R   $ 0��6g   $
��Fade=�6z   $��Right=�6�    5   (   �� $ errorBorder����6\   �� $ placeholder��$	 AdornedElement.IsFocused�$
 OneWay=�.��5      �� $ 
Horizontal=�6%   .+�5      � $��Center=�6'    �    	=  	=  	=  z ���	#FFCB2E2E$
 Fill=�6h   ���	#FFCB2E2E$��2��5   +   �� 5      ���	#FFCB2E2E$ 4��6<   $ 4q�6M   ._�5   "   �� $��Center=�6,   
�������$
��Bold"�6\   $��2,0,0,0q�6n   ��5   8   �� $ placeholder��$8	 2AdornedElement.(Validation.Errors)[0].ErrorContent�$
 OneWay=�5      5      5      5      5      5       � RowDefinitions 5    
   �� 5!      $��3* �6   �� 5"      $��1* �6   �� 5#      $��1* �6   �� 5$      $��1* �6   �� 5%      $��1* �6   �� 5&      $��1* �6   �� 5'      $��1* �6   �� 5(      $��1* �6   �� 5)      $��1* �6   �� 5*      $��1* �6    � ColumnDefinitions 5,   
   �� 5-      $	��120 �6   �� 5.      $��* �6   .+�50   
   �� $��1��6   $��0��6!   $��5q�61   $��Країна��6<   �� 51   
   $��1��6   $��1��6   $��5,5,5,0q�6/    =� ErrorTemplate# [ 6@   .��6   ��52      �� 53      $	 COUNTRY�6   $
 TwoWay=�6)    �� UpdateSourceTrigger$ PropertyChanged=�67    �� ValidatesOnExceptions .  �� ValidationRules 54      0   *CourseWorkDB_DudasVI.Views.Rules.EmptyRule  55      57      .��58      59      �� 5;   
   $��2��6   $��0��6!   $��5,5,0,5q�61   $��Регіон��6B   # [ 6P   �� 5<   
   $��2��6   $��1��6   $��5,5,5,0q�6/   .��6A   ��5=      �� 5>      $	 REGION�6   $
 TwoWay=�6(   $ PropertyChanged=�66    .  5?        5@      5B      .��5C      5D      �� 5F   
   $��5��6   $��0��6!   $��5q�61   $��
Місто��6<   �� 5G   
   -   5         ��Name$ asdas��5G      $��5��6!   $��1��6.   $��5,5,5,0q�6>   # [ 6O   .��6�   ��5H      �� 5I      $
	 CITY�6   $
 TwoWay=�6&   $ PropertyChanged=�64    .  5J        5K      5M      .��5N      5O      �� 5Q   
   $��6��6   $��0��6!   $��5q�61   $��Вулиця��6<   �� 5R   
   $��6��6   $��1��6   $��5,5,5,0q�6/   # [ 6@   .��6   ��5S      �� 5T      $	 STREET�6   $
 TwoWay=�6(   $ PropertyChanged=�66    .  5U        5V      5X      .��5Y      5Z      �� 5\   
   $��7��6   $��0��6!   $��5q�61   $��Номер будинку��6<   $��Center=�6Q   �� 5]   
   $��1q�6   $��7��6'   $��1��5^      $��5q�6&   # [ 61   $��Stretch=�5_      .��63   ��5`      �� 5a      $	 BUILDING_NUMBER�6   $
 TwoWay=�61   $ PropertyChanged=�6?    .  5b        5c      1   +CourseWorkDB_DudasVI.Views.Rules.NumberRule  5d      5f      .��5g      5h      5i      5j      