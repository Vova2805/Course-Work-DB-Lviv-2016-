using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    internal class ResultsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var post_id = (int) value;
            int param;
            int.TryParse(parameter.ToString(), out param);
            return post_id == param ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}