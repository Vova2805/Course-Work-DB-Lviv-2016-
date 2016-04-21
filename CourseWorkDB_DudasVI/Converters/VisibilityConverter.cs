using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    internal class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool res;
            bool.TryParse(value.ToString(), out res);
            return res ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}