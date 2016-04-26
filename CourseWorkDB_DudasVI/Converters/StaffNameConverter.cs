using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Converters
{
    public class StaffNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = "";
            var warehouse = value as WAREHOUSE;
            if (warehouse != null)
            {
                var staff = warehouse.STAFF;
                if (staff != null)
                    result += staff.STAFF_NAME + " " + staff.STAFF_SURNAME + " " + staff.POST.POST_NAME + " " +
                              staff.MOBILE_PHONE;
                else result = "Фахівці з продажу";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}