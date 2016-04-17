using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
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
        #region General Charts

        private SeriesCollection _LineSeriesInstance;
        private SeriesCollection _PieSeriesInstance;
        private SeriesCollection _BarSeriesInstance;
        private int _tabIndex;

        #endregion

        //TabPages

        #region First

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
        private DateTime _ToTime;
        private string _xTitle;
        private string _yTitle;
        private bool filterByPrice;

        #endregion

        #region Second

        private ObservableCollection<PRODUCT_INFO> _ProductsList;
        private PRODUCT_INFO _SelectedProduct;
        private PRODUCT_PRICE _SelectedProductPrice;
        private ObservableCollection<ProductPriceListElement> _ProductPriceList;
        private double _ProductPriceValue;
        private double _ProductPricePersentage;

        #endregion

        #region Functions

        #region Charts

        private void UpdateLineSeries()
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

        private void UpdatePieSeries()
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
                            for(int i = 0 ;i<2;i++)
                            {
                                if (i == 1)
                                {
                                    serieQuantity.Add((double)price.ProductPrice.PRICE_VALUE);
                                }
                                else
                                {
                                    serieQuantity.Add((double)price.ProductPrice.PERSENTAGE_VALUE);
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

        private void UpdateBarSeries()
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
            }
        }

        public void UpdateSeries()
        {
            if (LineSeriesInstance != null)
            {
                LineSeriesInstance.Clear();
                PieSeriesInstance.Clear();
                BarSeriesInstance.Clear();
                Labels.Clear();
                UpdateLineSeries();
                UpdatePieSeries();
                UpdateBarSeries();
                switch (TabIndex)
                {
                    case 0:
                    {
                        XTitle = "Номер продукції";
                        YTitle = "Кількість замовлено";
                    }
                        break;
                    case 1:
                    {
                        XTitle = "Дата зміни ціни";
                        YTitle = "Значення ціни";
                    }
                        break;
                }
            }
        }

        #endregion

        #region First

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

        #endregion

        #endregion

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

        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                _tabIndex = value;
                UpdateSeries();
                OnPropertyChanged("TabIndex");
            }
        }

        public double ProductPriceValue
        {
            get { return _ProductPriceValue; }
            set
            {
                _ProductPriceValue = value;
                if (value != null && SelectedProductPrice != null)
                    SelectedProductPrice.PRICE_VALUE = (decimal) _ProductPriceValue;
                if (ChangeProductPricePersentage)
                {
                    ChangeProductPriceValue = false; //to avoid endless cycle
                    ProductPricePersentage = ProductPriceValue/(double) SelectedProduct.PRODUCTION_PRICE*100;
                }
                OnPropertyChanged("ProductPriceValue");
            }
        }

        public double ProductPricePersentage
        {
            get { return _ProductPricePersentage; }
            set
            {
                _ProductPricePersentage = value;
                if (value != null && SelectedProductPrice != null)
                    SelectedProductPrice.PERSENTAGE_VALUE = (decimal) _ProductPricePersentage;
                if (ChangeProductPriceValue)
                {
                    ChangeProductPricePersentage = false;
                    ProductPriceValue = (double) SelectedProduct.PRODUCTION_PRICE*ProductPricePersentage/100.0;
                }
                OnPropertyChanged("ProductPricePersentage");
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

        public bool ChangeProductPriceValue;
        public bool ChangeProductPricePersentage;

        public PRODUCT_PRICE SelectedProductPrice
        {
            get { return _SelectedProductPrice; }
            set
            {
                _SelectedProductPrice = value;
                ChangeProductPriceValue = false;
                ChangeProductPricePersentage = false;
                ProductPriceValue = (double) SelectedProductPrice.PRICE_VALUE;
                ProductPricePersentage = (double) SelectedProductPrice.PERSENTAGE_VALUE;
                OnPropertyChanged("SelectedProductPrice");
            }
        }

        public ObservableCollection<PRODUCT_INFO> ProductsList
        {
            get { return _ProductsList; }
            set
            {
                _ProductsList = value;
                OnPropertyChanged("ProductsList");
            }
        }

        public PRODUCT_INFO SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                if (value != null)
                {
                    _SelectedProduct = value;
                    SelectedProductPrice = API.getlastPrice(SelectedProduct.PRODUCT_PRICE);
                    ProductPriceList = new ObservableCollection<ProductPriceListElement>();
                    foreach (var price in SelectedProduct.PRODUCT_PRICE.ToList())
                    {
                        ProductPriceList.Add(new ProductPriceListElement(price));
                    }
                    UpdateSeries();
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ObservableCollection<ProductPriceListElement> ProductPriceList
        {
            get { return _ProductPriceList; }
            set
            {
                _ProductPriceList = value;
                OnPropertyChanged("ProductPriceList");
            }
        }

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
            SelectedProductPrice = API.getlastPrice(SelectedProduct.PRODUCT_PRICE);
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
                    using (var factoryEntities = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = factoryEntities.Database.BeginTransaction())
                        {
                            try
                            {
                                var selected = factoryEntities.PRODUCT_PRICE;
                                SelectedProductPrice.CHANGED_DATE = API.getTodayDate();
                                SelectedProductPrice.PRICE_ID =
                                    factoryEntities.PRODUCT_PRICE.ToList().Max(price => price.PRICE_ID) + 1;
                                SelectedProductPrice.STAFF_ID = Session.User.STAFF_ID;
                                selected.Add(SelectedProductPrice);
                                factoryEntities.SaveChanges();
                                dbContextTransaction.Commit();
                                await specialistWindow.ShowMessageAsync("Вітання", "Зміни внесено! Нову ціну додано.");
                                UpdateDb();
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await specialistWindow.ShowMessageAsync("Невдача", "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
        }

        private void UpdateDb()
        {
            using (var factoryEntities = new SWEET_FACTORYEntities())
            {
                List<PRODUCT_INFO> temp = factoryEntities.PRODUCT_INFO.ToList();
                ProductsList.Clear();
                foreach (var product in temp)
                {
                    ProductsList.Add(product);
                }
                SelectedProduct =
                    ProductsList.FirstOrDefault(pr => pr.PRODUCT_INFO_ID == SelectedProduct.PRODUCT_INFO_ID);
            }
        }

        #endregion
    }
}