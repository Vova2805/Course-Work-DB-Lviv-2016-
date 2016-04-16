using System;
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
                }
            }
        }

        private void FilterByPrice(object sender, RoutedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                model.productPackagesList.Clear();
                var groupedPackages =
                    FactoryEntities.ORDER_PRODUCT.ToList()
                        .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                        .ToDictionary(group => group.Key, group => group.ToList());
                var i = 0;
                foreach (var group in groupedPackages)
                {
                    var price = API.getlastPrice(group.Value.First().PRODUCT_INFO.PRODUCT_PRICE);
                    if (price >= model.priceFrom && price <= model.priceTo)
                    {
                        model.productPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value,
                            Session.User));
                    }
                }
                model.UpdateQuantity();
            }
        }
    }
}