using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    public class TabIndexBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tab_index;
            int.TryParse(value.ToString(), out tab_index);
            int index;
            int.TryParse(parameter.ToString(), out index);
            return tab_index == index ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}