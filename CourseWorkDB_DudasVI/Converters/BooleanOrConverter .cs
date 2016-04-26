using System;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    public class BooleanOrConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            foreach (object value in values)
            {
                try
                {
                    if ((bool)value == true)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {}
            }
            return false;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}