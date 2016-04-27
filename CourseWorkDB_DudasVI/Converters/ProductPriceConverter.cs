using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using CourseWorkDB_DudasVI.General;

namespace CourseWorkDB_DudasVI.Converters
{
    internal class ProductPriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var product_id = (int) value;
            var param = parameter.ToString();
            var resString = "";
            switch (param)
            {
                case "$":
                {
                    resString =
                        API.getlastPrice(
                            Session.FactoryEntities.PRODUCT_INFO.ToList()
                                .Find(pr => pr.PRODUCT_INFO_ID == product_id)
                                .PRODUCT_PRICE)
                            .PRICE_VALUE.ToString("N") + " грн.";
                }
                    break;
                case "%":
                {
                    resString =
                        API.getlastPrice(
                            Session.FactoryEntities.PRODUCT_INFO.ToList()
                                .Find(pr => pr.PRODUCT_INFO_ID == product_id)
                                .PRODUCT_PRICE)
                            .PERSENTAGE_VALUE.ToString("N") + " %";
                }
                    break;
            }
            return resString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}