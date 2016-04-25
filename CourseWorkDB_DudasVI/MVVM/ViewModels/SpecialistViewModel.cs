using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Views;
using CourseWorkDB_DudasVI.Views.Dialogs;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SpecialistViewModel : ChartViewModel
    {
        public SpecialistViewModel()
        {
            AddPermition = true;
        }

        public void ColumnVisibilityChanged()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                specialistWindow.WarehouseDataGrid.Columns[6].Visibility = ExtendedMode
                    ? Visibility.Visible
                    : Visibility.Collapsed;
                specialistWindow.WarehouseDataGrid.Columns[3].Visibility = !ExtendedMode && isAllWarehouses
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

        public override void CurrentWarehouseChanged()
        {
            ExtendedMode = _ExtendedMode;
        }

        public class Meil : ViewModelBaseInside
        {
            private bool _Checked;
            private string _MeilTitle;

            public Meil(string meilTitle)
            {
                MeilTitle = meilTitle;
            }

            public bool Checked
            {
                get { return _Checked; }
                set
                {
                    _Checked = value;
                    OnPropertyChanged("Checked");
                }
            }

            public string MeilTitle
            {
                get { return _MeilTitle; }
                set
                {
                    _MeilTitle = value;
                    OnPropertyChanged("MeilTitle");
                }
            }
        }

        #region Variables

        //Expired period (calculation of expenses)
        private int _Days;
        private int _LostProducts;
        private decimal _LostMoney;
        
        #endregion

        #region Functions

        #region Charts

        public override void UpdateLineSeries()
        {
            switch (TabIndex)
            {
                case 0:
                {
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
        

        #endregion

        #region Properties

        public int Days
        {
            get { return _Days; }
            set
            {
                _Days = value;
                var ExpiredDate = API.getTodayDate();
                ExpiredDate = ExpiredDate.AddDays(_Days);
                LostMoney = 0;
                LostProducts = 0;
                if (_ProductsOnWarehouse != null)
                    foreach (var product in _ProductsOnWarehouse)
                    {
                        if (product.ReleasedProduct.EXPIRED_DATE <= ExpiredDate)
                        {
                            product.IsExpiring = true;
                            LostProducts += product.Quantity;
                            LostMoney += product.Quantity*
                                         API.getlastPrice(product.ReleasedProduct.PRODUCT_INFO.PRODUCT_PRICE)
                                             .PRICE_VALUE;
                        }
                        else
                        {
                            product.IsExpiring = false;
                        }
                    }
                OnPropertyChanged("Days");
            }
        }

        public decimal LostMoney
        {
            get { return _LostMoney; }
            set
            {
                _LostMoney = value;
                OnPropertyChanged("LostMoney");
            }
        }

        public int LostProducts
        {
            get { return _LostProducts; }
            set
            {
                _LostProducts = value;
                OnPropertyChanged("LostProducts");
            }
        }

        public bool ExtendedMode
        {
            get { return _ExtendedMode; }
            set
            {
                _ExtendedMode = value;
                ColumnVisibilityChanged();
                UpdateView();
                Days = _Days;
                if (_ProductsOnWarehouse != null)
                    foreach (var product in ProductsOnWarehouse)
                    {
                        product.IsExpiring &= _ExtendedMode;
                    }
                OnPropertyChanged("ExtendedMode");
            }
        }

        #endregion

        #region Commands

        public ICommand CanselPriceChanges
        {
            get { return new RelayCommand<object>(CanselChanges); }
        }

        public ICommand SubmitPriceChanges
        {
            get { return new RelayCommand<object>(SubmitChanges); }
        }

        public ICommand CloneScheduleToCurrent
        {
            get { return new RelayCommand<object>(CloneSchedule); }
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

        public async void CloneSchedule(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            foreach (var selectedSchedule_Package in SchedulePackages)
            {
                var existed = CurrentWarehouse.ContainsProductInfo(selectedSchedule_Package.PRODUCT_INFO);
                if (existed != null && existed.Quantity != selectedSchedule_Package.QUANTITY_IN_SCHEDULE)
                {
                    if (specialistWindow != null)
                    {
                        Dialog.Initialize("Змінити кількість?", "У плані вже наявний продукт : " + existed.ProductInfo.PRODUCT_INFO.PRODUCT_TITLE +
                            "\nКількості : " + existed.Quantity +
                            ".\nБажаєте змінити кількість на " + selectedSchedule_Package.QUANTITY_IN_SCHEDULE + " ?");
                        if(!Dialog.ForAll)
                        Dialog.ShowDialog();
                        if (Dialog.DialogResult == DialogResponse.Yes)
                        {
                            CurrentWarehouse.addScheduleProduct(selectedSchedule_Package.PRODUCT_INFO,
                                selectedSchedule_Package.QUANTITY_IN_SCHEDULE);
                        }
                    }
                }
                else
                {
                    CurrentWarehouse.addScheduleProduct(selectedSchedule_Package.PRODUCT_INFO,
                        selectedSchedule_Package.QUANTITY_IN_SCHEDULE);
                }
            }
            if (specialistWindow != null)
            {
                await specialistWindow.ShowMessageAsync("Зміни внесено",
                    "Усі зміни в поточний план виробництва успішно внесено.");
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
    }
}