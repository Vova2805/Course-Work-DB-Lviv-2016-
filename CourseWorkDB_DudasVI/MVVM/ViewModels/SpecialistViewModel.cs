using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Views;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SpecialistViewModel : ViewModelBase
    {
        private ObservableCollection<string> _CategoriesList;

        private WAREHOUSE _CurrentWarehouse;
        private DateTime _FromTime;
        private ObservableCollection<string> _Labels;
        private Dictionary<string, SpecialistModel.RegionInfo> _options;
        private List<string> _OptionsList;
        private decimal _priceFrom;
        private decimal _priceTo;
        private ObservableCollection<OrderProductTransaction> _productPackagesList;
        private string _selectedCategory;
        private string _selectedOption;
        private SeriesCollection _LineSeriesInstance;
        private SeriesCollection _PieSeriesInstance;
        private SeriesCollection _BarSeriesInstance;
        private DateTime _ToTime;
        private string _xTitle;
        private string _yTitle;
        private bool filterByPrice;

        public void Update()
        {
            options = new Dictionary<string, SpecialistModel.RegionInfo>();
            OptionsList = new List<string>();
            var i = 0;
            foreach (var quantity in productPackagesList.First().QuantityInOrders)
            {
                options.Add(quantity.From.ToLongDateString() + " - " + quantity.To.ToLongDateString(),
                    new SpecialistModel.RegionInfo(i++, quantity.From, quantity.To));
            }
            OptionsList = options.Keys.ToList();
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

        public void UpdateSeries()
        {
            LineSeriesInstance.Clear();
            PieSeriesInstance.Clear();
            BarSeriesInstance.Clear();
            Labels.Clear();
            UpdateLineSeries();
            UpdatePieSeries();

            XTitle = "Номер продукції";
            YTitle = "Кількість замовлено";
        }

        private void UpdateLineSeries()
        {
            int i = 0;
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
        }
        private void UpdatePieSeries()
        {
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
        }
        private void UpdateBarSeries()
        {

        }

        #region Properties

        public ObservableCollection<string> Labels
        {
            get { return _Labels; }
            set
            {
                _Labels = value;
                OnPropertyChanged("Labels");
            }
        }
        

        public string XTitle
        {
            get { return _xTitle; }
            set
            {
                _xTitle = value;
                OnPropertyChanged("XTitle");
            }
        }

        public string YTitle
        {
            get { return _yTitle; }
            set
            {
                _yTitle = value;
                OnPropertyChanged("YTitle");
            }
        }

        public SeriesCollection LineSeriesInstance
        {
            get { return _LineSeriesInstance; }
            set
            {
                _LineSeriesInstance = value;
                OnPropertyChanged("LineSeriesInstance");
            }
        }

        public SeriesCollection PieSeriesInstance
        {
            get { return _PieSeriesInstance; }
            set
            {
                _PieSeriesInstance = value;
                OnPropertyChanged("PieSeriesInstance");
            }
        }

        public SeriesCollection BarSeriesInstance
        {
            get { return _BarSeriesInstance; }
            set
            {
                _BarSeriesInstance = value;
                OnPropertyChanged("BarSeriesInstance");
            }
        }

        public ObservableCollection<string> CategoriesList
        {
            get { return _CategoriesList; }
            set
            {
                _CategoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }

        public bool FilterByPrice
        {
            get { return filterByPrice; }
            set
            {
                filterByPrice = value;
                OnPropertyChanged("FilterByPrice");
            }
        }

        public string selectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("selectedCategory");
            }
        }

        public decimal priceFrom
        {
            get { return _priceFrom; }
            set
            {
                _priceFrom = value;
                OnPropertyChanged("priceFrom");
            }
        }

        public decimal priceTo
        {
            get { return _priceTo; }
            set
            {
                _priceTo = value;
                OnPropertyChanged("priceTo");
            }
        }

        public WAREHOUSE CurrentWarehouse
        {
            get { return _CurrentWarehouse; }
            set
            {
                _CurrentWarehouse = value;
                OnPropertyChanged("CurrentWarehouse");
            }
        }

        public ObservableCollection<OrderProductTransaction> productPackagesList
        {
            get { return _productPackagesList; }
            set
            {
                _productPackagesList = value;
                UpdateSeries();
                OnPropertyChanged("productPackagesList");
            }
        }

        public DateTime FromTime
        {
            get { return _FromTime; }
            set
            {
                _FromTime = value;
                OnPropertyChanged("FromTime");
            }
        }

        public DateTime ToTime
        {
            get { return _ToTime; }
            set
            {
                _ToTime = value;
                OnPropertyChanged("ToTime");
            }
        }

        public Dictionary<string, SpecialistModel.RegionInfo> options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged("options");
            }
        }

        public string selectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged("selectedOption");
            }
        }

        public List<string> OptionsList
        {
            get { return _OptionsList; }
            set
            {
                _OptionsList = value;
                OnPropertyChanged("OptionsList");
            }
        }

        private string _userNameSurname;

        public string userNameSurname
        {
            get { return Session.User.STAFF_NAME + " " + Session.User.STAFF_SURNAME; }
            set
            {
                _userNameSurname = value;
                OnPropertyChanged("userNameSurname");
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

        #endregion
    }
}