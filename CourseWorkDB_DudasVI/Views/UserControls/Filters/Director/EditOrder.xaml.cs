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
    public partial class EditOrderOrProduct : UserControl
    {
        public static DateTime from = DateTime.Now;
        public static DateTime to = DateTime.Now;

        public EditOrderOrProduct()
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
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                switch (model.TabIndex)
                {
                    case 0:
                    {
                        #region First

                        model.ProductPackagesList.Clear();
                        if (model.SelectedCategory.Equals("Всі категорії"))
                        {
                            var groupedPackages =
                                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                                    .ToDictionary(group => group.Key, group => group.ToList());
                            var i = 0;
                            foreach (var group in groupedPackages)
                            {
                                model.ProductPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value,
                                    Session.User));
                            }
                            model.UpdateQuantity();
                        }
                        else
                        {
                            var groupedPackages =
                                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                                    .ToDictionary(group => group.Key, group => group.ToList());
                            var i = 0;
                            foreach (var group in groupedPackages)
                            {
                                if (
                                    group.Value.First()
                                        .PRODUCT_INFO.CATEGORY.CATEGORY_TITLE.Equals(model.SelectedCategory))
                                {
                                    model.ProductPackagesList.Add(new OrderProductTransaction(i++, group.Key,
                                        group.Value,
                                        Session.User));
                                }
                            }
                            model.UpdateQuantity();
                            FilterByPrice(sender, e);
                        }
                        var titles =
                            model.ProductPackagesList.ToList()
                                .Select(p => p.packages.First().PRODUCT_INFO.PRODUCT_TITLE.ToString())
                                .ToList();
                        var prices =
                            Session.FactoryEntities.PRODUCT_PRICE.ToList()
                                .FindAll(pr => titles.Contains(pr.PRODUCT_INFO.PRODUCT_TITLE))
                                .ToList();
                        model.PriceFrom = prices.Min(p => p.PRICE_VALUE);
                        model.PriceTo = prices.Max(p => p.PRICE_VALUE);

                        #endregion
                    }
                        break;
                    case 1:
                    {
                        #region Second

                        //model.ProductsList.Clear();
                        //var productprices = new List<double>();
                        //model.ProductsList = new ObservableCollection<ProductListElement>();
                        //foreach (var product in Session.FactoryEntities.PRODUCT_INFO.ToList())
                        //{
                        //    model.ProductsList.Add(new ProductListElement(product, model));
                        //}
                        //if (!model.SelectedCategory.Equals("Всі категорії"))
                        //{
                        //    var temp = new ObservableCollection<ProductListElement>();
                        //    foreach (var product in model.ProductsList)
                        //    {
                        //        if (
                        //            Session.FactoryEntities.CATEGORY.Find(product.ProductInfo.CATEGORY_ID)
                        //                .CATEGORY_TITLE.Equals(model.SelectedCategory))
                        //        {
                        //            temp.Add(product);
                        //            productprices.Add((double) API.getlastPrice(
                        //                Session.FactoryEntities.PRODUCT_PRICE.ToList()
                        //                    .FindAll(pr => pr.PRODUCT_INFO_ID == product.ProductInfo.PRODUCT_INFO_ID))
                        //                .PRICE_VALUE);
                        //        }
                        //    }
                        //    model.ProductsList = temp;
                        //    FilterByPrice(sender, e);
                        //}
                        //if (productprices.Count > 0)
                        //{
                        //    model.PriceFrom = (decimal) productprices.Min(p => p);
                        //    model.PriceTo = (decimal) productprices.Max(p => p);
                        //}

                        #endregion
                    }
                        break;
                }
            }
        }

        private void FilterByPrice(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null && model.FilterByPrice)
            {
                switch (model.TabIndex)
                {
                    case 0:
                    {
                        var i = 0;
                        var temp = new ObservableCollection<OrderProductTransaction>();
                        foreach (var product in model.ProductPackagesList)
                        {
                            var price =
                                API.getlastPrice(product.packages.First().PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE;
                            if (price >= model.PriceFrom && price <= model.PriceTo)
                            {
                                temp.Add(new OrderProductTransaction(i++,
                                    product.packages.First().PRODUCT_INFO.PRODUCT_TITLE,
                                    product.packages, Session.User));
                            }
                        }
                        model.ProductPackagesList = temp;
                        model.UpdateQuantity();
                    }
                        break;
                    case 1:
                    {
                        var i = 0;
                        var temp = new ObservableCollection<ProductListElement>();
                        foreach (var product in model.ProductsList)
                        {
                            var price = API.getlastPrice(
                                Session.FactoryEntities.PRODUCT_PRICE.ToList().
                                    FindAll(pr => pr.PRODUCT_INFO_ID == product.ProductInfo.PRODUCT_INFO_ID))
                                .PRICE_VALUE;
                            if (price >= model.PriceFrom && price <= model.PriceTo)
                            {
                                temp.Add(product);
                            }
                        }
                        model.ProductsList = temp;
                    }
                        break;
                }
            }
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                switch (model.TabIndex)
                {
                    case 0:
                    {
                        if (model.ProductPackagesList != null)
                        {
                            CategorySelectionChanged(sender, null);
                            if (!model.FilterByPrice)
                                model.UpdateSeries();
                        }
                    }
                        break;
                    case 1:
                    {
                    }
                        break;
                }
            }
        }

        private void ProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null && model.FilterByPrice)
            {
                CategorySelectionChanged(sender, e);
                if (!model.SelectedProductTitle.Equals("Всі продукти"))
                {
                    var productprices = new List<double>();
                    var temp = new ObservableCollection<ProductListElement>();
                    foreach (var product in model.ProductsList)
                    {
                        if (product.ProductInfo.PRODUCT_TITLE.Equals(model.SelectedProductTitle))
                        {
                            temp.Add(product);
                            productprices.Add((double) API.getlastPrice(product.ProductInfo.PRODUCT_PRICE).PRICE_VALUE);
                        }
                    }
                    model.ProductsList = temp;
                    FilterByPrice(sender, e);
                    model.PriceFrom = (decimal) productprices.Min(p => p);
                    model.PriceTo = (decimal) productprices.Max(p => p);
                }
            }
        }
    }
}