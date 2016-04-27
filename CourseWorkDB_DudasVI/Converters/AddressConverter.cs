using System;
using System.Globalization;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    public class AddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var address = value as ADDRESS;
            var response = "";
            if (address != null)
            {
                response = address.COUNTRY + ", " + address.REGION + ", " + address.CITY + ", " + address.STREET + ", " +
                           address.BUILDING_NUMBER;
            }
            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}