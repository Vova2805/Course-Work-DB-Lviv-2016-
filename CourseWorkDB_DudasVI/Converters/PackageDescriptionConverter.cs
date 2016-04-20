using System;
using System.Globalization;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    public class PackageDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var package_description = value as PACKAGE_DESCRIPTION;
            var response = "";
            if (package_description != null)
            {
                response = " Висота :" + package_description.HEIGTH.ToString("N") +
                           " Ширина :" + package_description.WIDTH.ToString("N") +
                           " Довжина :" + package_description.LENGTH.ToString("N");
            }
            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}