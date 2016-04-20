using System;
using System.Globalization;
using System.Windows.Data;

namespace CourseWorkDB_DudasVI.Converters
{
    public class HeaderTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int tab_index;
            int.TryParse(value.ToString(), out tab_index);
            switch (tab_index)
            {
                case 0:
                {
                    return "Фільтрувати замовлення";
                }
                    break;
                case 1:
                {
                    return "Фільтрувати товари";
                }
                    break;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}