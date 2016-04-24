using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Views;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SpecialistViewModel : ChartViewModel
    {
        public SpecialistViewModel():base()
        {
            AddPermition = true;
            

        }


        //TabPages
        



        #region Functions

        #region Charts

        public override void UpdateLineSeries()
        {
            switch (TabIndex)
            {
                case 0:
                {
                    #region First

                    var i = 0;
                    foreach (var quantity in productPackagesList.First().QuantityInOrders)
                    {
                        var serieQuantity = new List<double>();
                        foreach (var product in productPackagesList)
                        {
                            if (product.isChecked)
                            {
                                serieQuantity.Add(product.QuantityInOrders.ElementAt(i).Quantity);
                                Labels.Add("№ " + product.Number);
                            }
                        }
                        var chartValues = new ChartValues<double>();
                        chartValues.AddRange(serieQuantity);
                        var newSerie = new LineSeries
                        {
                            Title = quantity.From.ToShortDateString() + "\n" + quantity.To.ToShortDateString(),
                            Values = chartValues,
                            PointRadius = 3
                        };
                        LineSeriesInstance.Add(newSerie);
                        i++;
                    }

                    #endregion
                }
                    break;
                case 1:
                {
                    #region Second

                    for (var i = 0; i < 2; i++)
                    {
                        var seriePrice = new List<double>();
                        foreach (var price in ProductPriceList)
                        {
                            if (i == 0)
                                seriePrice.Add((double) price.ProductPrice.PRICE_VALUE);
                            else
                            {
                                seriePrice.Add((double) price.ProductPrice.PERSENTAGE_VALUE);
                            }
                            Labels.Add(price.ProductPrice.CHANGED_DATE.ToString("g"));
                        }
                        var chartValues = new ChartValues<double>();
                        chartValues.AddRange(seriePrice);
                        var newSerie = new LineSeries
                        {
                            Title = i == 0 ? "У валюті" : "У відсотках",
                            Values = chartValues,
                            PointRadius = 3
                        };
                        LineSeriesInstance.Add(newSerie);
                    }

                    #endregion
                }
                    break;
            }
        }

        public override void UpdatePieSeries()
        {
            switch (TabIndex)
            {
                case 0:
                {
                    #region First

                    foreach (var product in productPackagesList)
                    {
                        if (product.isChecked)
                        {
                            var serieQuantity = new List<double>();
                            foreach (var quantity in product.QuantityInOrders)
                            {
                                serieQuantity.Add(quantity.Quantity);
                            }
                            var pieValues = new ChartValues<double>();
                            pieValues.AddRange(serieQuantity);
                            var newPSerie = new PieSeries
                            {
                                Title = product.ProductTitle,
                                Values = pieValues
                            };
                            PieSeriesInstance.Add(newPSerie);
                            Labels.Add("№ " + product.Number + ". " + product.ProductTitle);
                        }
                    }

                    #endregion
                }
                    break;
                case 1:
                {
                    #region Second

                    foreach (var price in ProductPriceList)
                    {
                        var serieQuantity = new List<double>();
                        for (var i = 0; i < 2; i++)
                        {
                            if (i == 1)
                            {
                                serieQuantity.Add((double) price.ProductPrice.PRICE_VALUE);
                            }
                            else
                            {
                                serieQuantity.Add((double) price.ProductPrice.PERSENTAGE_VALUE);
                            }
                        }
                        var pieValues = new ChartValues<double>();
                        pieValues.AddRange(serieQuantity);
                        var newPSerie = new PieSeries
                        {
                            Title = price.ProductPrice.CHANGED_DATE.ToLongDateString(),
                            Values = pieValues
                        };
                        PieSeriesInstance.Add(newPSerie);
                        Labels.Add(price.ProductPrice.CHANGED_DATE.ToLongDateString());
                    }

                    #endregion
                }
                    break;
            }
        }

        public override void UpdateBarSeries()
        {
            switch (TabIndex)
            {
                case 0:
                {
                    #region First

                    var i = 0;
                    var lineChartValues = new List<double>();
                    foreach (var quantity in productPackagesList.First().QuantityInOrders)
                    {
                        var serieQuantity = new List<double>();
                        var j = 0;
                        foreach (var product in productPackagesList)
                        {
                            if (product.isChecked)
                            {
                                if (i == 0)
                                {
                                    lineChartValues.Add(0);
                                    lineChartValues[lineChartValues.Count - 1] +=
                                        product.QuantityInOrders.ElementAt(i).Quantity;
                                }
                                else
                                {
                                    lineChartValues[j++] += product.QuantityInOrders.ElementAt(i).Quantity;
                                }
                                serieQuantity.Add(product.QuantityInOrders.ElementAt(i).Quantity);

                                Labels.Add("№ " + product.Number);
                            }
                        }
                        var chartValues = new ChartValues<double>();
                        chartValues.AddRange(serieQuantity);
                        var newSerie = new BarSeries
                        {
                            Title = quantity.From.ToShortDateString() + "\n" + quantity.To.ToShortDateString(),
                            Values = chartValues
                        };
                        BarSeriesInstance.Add(newSerie);
                        i++;
                    }
                    for (i = 0; i < lineChartValues.Count; i++)
                    {
                        lineChartValues[i] /= productPackagesList.First().QuantityInOrders.Count;
                    }
                    var chartLineValues = new ChartValues<double>();
                    chartLineValues.AddRange(lineChartValues);
                    var newLineSerie = new LineSeries
                    {
                        Title = "Середнє значення",
                        Values = chartLineValues,
                        PointRadius = 3,
                        Fill = new SolidColorBrush(Colors.Transparent),
                        Stroke = new SolidColorBrush(Colors.DarkOrange)
                    };
                    BarSeriesInstance.Add(newLineSerie);

                    #endregion
                }
                    break;
                case 1:
                {
                    #region Second

                    var lineChartValues = new List<double>();
                    for (var i = 0; i < 2; i++)
                    {
                        var serieQuantity = new List<double>();
                        foreach (var price in ProductPriceList)
                        {
                            if (i == 0)
                            {
                                serieQuantity.Add((double) price.ProductPrice.PRICE_VALUE);
                            }
                            else
                            {
                                serieQuantity.Add((double) price.ProductPrice.PERSENTAGE_VALUE);
                                lineChartValues.Add(100);
                            }
                        }
                        var chartValues = new ChartValues<double>();
                        chartValues.AddRange(serieQuantity);
                        var newSerie = new BarSeries
                        {
                            Title = i == 0 ? "У валюті (грн.)" : "У відсотках %",
                            Values = chartValues
                        };
                        BarSeriesInstance.Add(newSerie);
                    }

                    var chartLineValues = new ChartValues<double>();
                    chartLineValues.AddRange(lineChartValues);
                    var newLineSerie = new LineSeries
                    {
                        Title = "Лінія безбитковості",
                        Values = chartLineValues,
                        PointRadius = 3,
                        Fill = new SolidColorBrush(Colors.Transparent),
                        Stroke = new SolidColorBrush(Colors.DarkRed)
                    };
                    BarSeriesInstance.Add(newLineSerie);
                }

                    #endregion

                    break;
            }
        }

        public override void UpdateSeries()
        {
            if (LineSeriesInstance != null)
            {
                //LineSeriesInstance.Clear();
                //PieSeriesInstance.Clear();
                //BarSeriesInstance.Clear();
                //Labels.Clear();
                //UpdateLineSeries();
                //UpdatePieSeries();
                //UpdateBarSeries();
                //switch (TabIndex)
                //{
                //    case 0:
                //        {
                //            XTitle = "Номер продукції";
                //            YTitle = "Кількість замовлено";
                //        }
                //        break;
                //    case 1:
                //        {
                //            XTitle = "Дата зміни ціни";
                //            YTitle = "Значення ціни";
                //        }
                //        break;
                //}
            }
        }

        #endregion

        #region First

        public void Update()
        {
            options = new Dictionary<string, RegionInfo>();
            OptionsList = new ObservableCollection<string>();
            var i = 0;
            foreach (var quantity in productPackagesList.First().QuantityInOrders)
            {
                options.Add(quantity.From.ToLongDateString() + " - " + quantity.To.ToLongDateString(),
                    new RegionInfo(i++, quantity.From, quantity.To));
            }
            foreach (var option in options.Keys.ToList())
            {
                OptionsList.Add(option);
            }
            if (OptionsList.Count > 0)
                selectedOption = OptionsList.First();
            UpdateSeries();
        }

        public void UpdateQuantity()
        {
            foreach (var product in productPackagesList)
            {
                product.QuantityInOrders.Clear();
                foreach (var option in options)
                {
                    product.QuantityInOrders.Add(new OrderProductTransaction.QuantityInOrder(option.Value.from,
                        option.Value.to, product));
                }
            }
            UpdateSeries();
        }

        #endregion

        #endregion

        #region Properties

        public bool ChangeProductPriceValue;
        public bool ChangeProductPricePersentage;

       

        #endregion

        #region Commands

        public ICommand ChangeCommand
        {
            get { return new RelayCommand<string>(DoChange); }
        }

        public ICommand CanselPriceChanges
        {
            get { return new RelayCommand<object>(CanselChanges); }
        }

        public ICommand SubmitPriceChanges
        {
            get { return new RelayCommand<object>(SubmitChanges); }
        }

        public void DoChange(string parameter)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();

            switch (parameter)
            {
                case "Add":
                {
                    if (options.ContainsKey(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString()))
                    {
                        if (window != null)
                        {
                            window.ShowMessageAsync("Не можливо додати", "Ви уже обрали даний термін. Спробуйте інший");
                        }
                    }
                    else
                    {
                        foreach (var pr in productPackagesList)
                        {
                            pr.QuantityInOrders.Add(new OrderProductTransaction.QuantityInOrder(FromTime, ToTime, pr));
                        }
                        if (window != null)
                        {
                            var specialistWindow = window as HomeWindowSpecialist;
                            if (specialistWindow != null)
                            {
                                specialistWindow.addColumns();
                                Update();
                            }
                        }
                    }
                }
                    break;
                case "Remove":
                {
                    if (selectedOption != null)
                    {
                        var res = options[selectedOption];
                        foreach (var pr in productPackagesList)
                        {
                            pr.QuantityInOrders.RemoveAt(res.index);
                        }
                        if (window != null)
                        {
                            var specialistWindow = window as HomeWindowSpecialist;
                            if (specialistWindow != null)
                            {
                                specialistWindow.addColumns();
                                Update();
                            }
                        }
                    }
                }
                    break;
            }
        }

        public void CanselChanges(object obj)
        {
            SelectedProductPrice = API.getlastPrice(SelectedProduct.ProductInfo.PRODUCT_PRICE);
        }

        public async void SubmitChanges(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                var result =
                    await
                        specialistWindow.ShowMessageAsync("Попередження",
                            "Ви дійсно хочете підтвердити зміни?\nСкасувати цю дію буде не можливо.",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var dbContextTransaction = Session.FactoryEntities.Database.BeginTransaction())
                    {
                        try
                        {
                            var selected = Session.FactoryEntities.PRODUCT_PRICE;
                            SelectedProductPrice.CHANGED_DATE = API.getTodayDate();
                            SelectedProductPrice.PRICE_ID =
                                Session.FactoryEntities.PRODUCT_PRICE.ToList().Max(price => price.PRICE_ID) + 1;
                            SelectedProductPrice.STAFF_ID = Session.User.STAFF_ID;
                            selected.Add(SelectedProductPrice);
                            Session.FactoryEntities.SaveChanges();
                            dbContextTransaction.Commit();
                            await specialistWindow.ShowMessageAsync("Вітання", "Зміни внесено! Нову ціну додано.");
                            UpdateDb();
                        }
                        catch (Exception e)
                        {
                            dbContextTransaction.Rollback();
                            await
                                specialistWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                        }
                    }
                }
            }
        }


        private void UpdateDb()
        {
            var temp = Session.FactoryEntities.PRODUCT_INFO.ToList();
            ProductsList.Clear();
            foreach (var product in temp)
            {
                ProductsList.Add(new ProductListElement(product, this));
            }
            if (ProductsList != null && ProductsList.Count > 0)
                SelectedProduct =
                    ProductsList.FirstOrDefault(
                        pr => pr.ProductInfo.PRODUCT_INFO_ID == SelectedProduct.ProductInfo.PRODUCT_INFO_ID);
        }

        #endregion

        public override void ColumnVisibilityChanged()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                specialistWindow.WarehouseDataGrid.Columns[6].Visibility = ExtendedMode
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                specialistWindow.WarehouseDataGrid.Columns[3].Visibility = !ExtendedMode
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                specialistWindow.WarehouseDataGrid.Columns[4].Visibility = ExtendedMode
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                specialistWindow.WarehouseDataGrid.Columns[5].Visibility = ExtendedMode
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
            
        }
    }
}