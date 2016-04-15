using System;
using System.Globalization;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    class StaffImageConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base_url = "pack://application:,,,/Resources/images/";
            string image = "test.png";
            int val = (int) value;
            switch (val)
            {
                case 1: { image = "director.png"; } break;
                case 2: { image = "saler.png"; } break;
                case 3: { image = "specialist.png"; } break;
            }
            return base_url + image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
