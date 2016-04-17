using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class EditOrder : UserControl
    {
        public static DateTime from = DateTime.Now;
        public static DateTime to = DateTime.Now;
        private readonly SWEET_FACTORYEntities FactoryEntities = new SWEET_FACTORYEntities();

        public EditOrder()
        {
            InitializeComponent();
            FromDatePicker.DisplayDateEnd = DateTime.Now;
            ToDatePicker.DisplayDateEnd = DateTime.Now;
        }

        private void ToTimeChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            var date = picker.SelectedDate;
            if (date != null)
            {
                to = (DateTime) date;
            }
            FromDatePicker.SelectedDate = FromDatePicker.SelectedDate;
        }

        private void FromTimeChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            var date = picker.SelectedDate;
            if (date != null)
            {
                from = (DateTime) date;
            }
            ToDatePicker.SelectedDate = ToDatePicker.SelectedDate;
        }

        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                model.productPackagesList.Clear();
                if (model.selectedCategory.Equals("Всі категорії"))
                {
                    var groupedPackages =
                        FactoryEntities.ORDER_PRODUCT.ToList()
                            .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                            .ToDictionary(group => group.Key, group => group.ToList());
                    var i = 0;
                    foreach (var group in groupedPackages)
                    {
                        model.productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value,
                            Session.User));
                    }
                    model.UpdateQuantity();
                }
                else
                {
                    var groupedPackages =
                        FactoryEntities.ORDER_PRODUCT.ToList()
                            .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                            .ToDictionary(group => group.Key, group => group.ToList());
                    var i = 0;
                    foreach (var group in groupedPackages)
                    {
                        if (group.Value.First().PRODUCT_INFO.CATEGORY.CATEGORY_TITLE.Equals(model.selectedCategory))
                        {
                            model.productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value,
                                Session.User));
                        }
                    }
                    model.UpdateQuantity();
                    FilterByPrice(sender,e);
                }
                List<string> titles =
                    model.productPackagesList.ToList().Select(p => p.packages.First().PRODUCT_INFO.PRODUCT_TITLE.ToString()).ToList();
                List<PRODUCT_PRICE> prices =
                    FactoryEntities.PRODUCT_PRICE.ToList()
                        .FindAll(pr => titles.Contains(pr.PRODUCT_INFO.PRODUCT_TITLE))
                        .ToList();
                model.priceFrom = prices.Min(p => p.PRICE_VALUE);
                model.priceTo = prices.Max(p => p.PRICE_VALUE);
            }
        }

        private void FilterByPrice(object sender, RoutedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null && model.FilterByPrice)
            {
                var i = 0;
                ObservableCollection<OrderProductTransaction> temp = new ObservableCollection<OrderProductTransaction>();
                foreach (var product in model.productPackagesList)
                {
                    var price = API.getlastPrice(product.packages.First().PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE;
                    if (price >= model.priceFrom && price <= model.priceTo)
                    {
                        temp.Add(new OrderProductTransaction(i++, product.packages.First().PRODUCT_INFO.PRODUCT_TITLE, product.packages,Session.User));
                    }
                }
                model.productPackagesList = temp;
                model.UpdateQuantity();
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                if (model.productPackagesList != null)
                {
                    CategorySelectionChanged(sender, null);
                    if (!model.FilterByPrice)
                    model.UpdateSeries();
                }
            }
               
        }
    }
}