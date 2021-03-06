﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Converters
{
    public class AddedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var param = parameter.ToString();
            var response = "";
            switch (param)
            {
                case "Add":
                {
                    var additional = " до плану";
                    var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
                    var currentWindow = window as HomeWindowSale;
                    if (currentWindow != null)
                    {
                        additional = " до замовлення";
                    }
                    bool added;
                    bool.TryParse(value.ToString(), out added);
                    response = added ? "Додано" + additional : "Додати" + additional;
                }
                    break;
                case "Remove":
                {
                    var additional = " із плану";
                    var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
                    var currentWindow = window as HomeWindowSale;
                    if (currentWindow != null)
                    {
                        additional = " із замовлення";
                    }
                    bool added;
                    bool.TryParse(value.ToString(), out added);
                    response = "Видалити";
                }
                    break;
            }

            return response;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}