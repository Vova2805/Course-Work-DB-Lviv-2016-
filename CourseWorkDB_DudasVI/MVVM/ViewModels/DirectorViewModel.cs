using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Views;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class DirectorViewModel : ChartViewModel
    {
        private ObservableCollection<STAFF> _EmployeeList;
        private STAFF _SelectedEmployee;

        public DirectorViewModel() : base()
        {
        }

        public override void UpdateBarSeries()
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

        public override void UpdateLineSeries()
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

        public override void UpdatePieSeries()
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

        public override void UpdateSeries()
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

        #region Second

        #endregion

        #region Properties

        public ObservableCollection<STAFF> EmployeeList
        {
            get { return _EmployeeList; }
            set
            {
                _EmployeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        public STAFF SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }

        #endregion

        #region Commands
        public ICommand ChangeCommand
        {
            get { return new RelayCommand<string>(DoChange); }
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
                                var adminWindow = window as HomeWindowAdmin;
                                if (adminWindow != null)
                                {
                                    adminWindow.addColumns();
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
                                var adminWindow = window as HomeWindowAdmin;
                                if (adminWindow != null)
                                {
                                    adminWindow.addColumns();
                                    Update();
                                }
                            }
                        }
                    }
                    break;
            }
        }

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

        public override void CurrentWarehouseChanged()
        {
            throw new System.NotImplementedException();
        }
    }
}