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
            int number = 0;
            bool parsed = false;
             if(parameter!=null) parsed=int.TryParse(parameter.ToString(), out number);
            bool res;
            bool.TryParse(value.ToString(), out res);
            if (parsed && number == 1)
            {
                res = !res;
            }
            return res ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}