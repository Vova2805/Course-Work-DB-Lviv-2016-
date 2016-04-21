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
        public SpecialistViewModel()
        {
            AddPermition = true;
        }

        #region General Charts

        private SeriesCollection _LineSeriesInstance;
        private SeriesCollection _PieSeriesInstance;
        private SeriesCollection _BarSeriesInstance;
        private int _tabIndex;

        #endregion

        //TabPages

        #region First

        private ObservableCollection<string> _CategoriesList;
        private DateTime _FromTime;
        private ObservableCollection<string> _Labels;
        private Dictionary<string, SpecialistModel.RegionInfo> _options;
        private ObservableCollection<string> _OptionsList;
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

        private ObservableCollection<ProductListElement> _ProductsList;
        private ObservableCollection<string> _ProductsTitleList;
        private ProductListElement _SelectedProduct;
        private PRODUCT_PRICE _SelectedProductPrice;
        private ObservableCollection<ProductPriceListElement> _ProductPriceList;
        private double _ProductPriceValue;
        private double _ProductPricePersentage;
        private PRODUCTION_SCHEDULE _CurrentProductionSchedule;
        private string _SelectedProductTitle;

        #endregion

        #region Third

        private WarehouseListItem _CurrentWarehouse;
        private ObservableCollection<WarehouseListItem> _warehouses;
        private ObservableCollection<WarehouseProductTransaction> _InOutComeFlow;
        private ObservableCollection<string> _warehousesStrings;
        private string _CurrentWarehouseString;
        private string _DateFilterString;
        private string _ValueRange;
        private string _TotalIncome;
        private string _TotalOutcome;
        private string _FlowDirection;
        private ObservableCollection<RELEASED_PRODUCT> _ProductsOnWarehouse;
        private decimal _Engaged;

        #endregion

        #region So on

        private ObservableCollection<PRODUCTION_SCHEDULE> _Schedules;
        private PRODUCTION_SCHEDULE _SelectedProductionSchedule;
        private ObservableCollection<SCHEDULE_PRODUCT_INFO> _schedulePackages;
        private string _changedText;

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
            OptionsList = new ObservableCollection<string>();
            var i = 0;
            foreach (var quantity in productPackagesList.First().QuantityInOrders)
            {
                options.Add(quantity.From.ToLongDateString() + " - " + quantity.To.ToLongDateString(),
                    new SpecialistModel.RegionInfo(i++, quantity.From, quantity.To));
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

        public string DateFilterString
        {
            get { return _DateFilterString; }
            set
            {
                _DateFilterString = value;
                OnPropertyChanged("DateFilterString");
            }
        }

        public string FlowDirection
        {
            get { return _FlowDirection; }
            set
            {
                _FlowDirection = value;
                OnPropertyChanged("FlowDirection");
            }
        }

        public string ChangedText
        {
            get { return _changedText; }
            set
            {
                _changedText = value;
                OnPropertyChanged("ChangedText");
            }
        }


        public string TotalIncome
        {
            get { return _TotalIncome; }
            set
            {
                _TotalIncome = value;
                OnPropertyChanged("TotalIncome");
            }
        }

        public string TotalOutcome
        {
            get { return _TotalOutcome; }
            set
            {
                _TotalOutcome = value;
                OnPropertyChanged("TotalOutcome");
            }
        }

        public string ValueRange
        {
            get { return _ValueRange; }
            set
            {
                _ValueRange = value;
                OnPropertyChanged("ValueRange");
            }
        }

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
                    ProductPricePersentage = ProductPriceValue/(double) SelectedProduct.ProductInfo.PRODUCTION_PRICE*100;
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
                    ProductPriceValue = (double) SelectedProduct.ProductInfo.PRODUCTION_PRICE*ProductPricePersentage/
                                        100.0;
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

        public ObservableCollection<RELEASED_PRODUCT> ProductsOnWarehouse
        {
            get { return _ProductsOnWarehouse; }
            set
            {
                _ProductsOnWarehouse = value;
                OnPropertyChanged("ProductsOnWarehouse");
            }
        }

        public WarehouseListItem CurrentWarehouse
        {
            get { return _CurrentWarehouse; }
            set
            {
                _CurrentWarehouse = value;
                InOutComeFlow = new ChartValues<WarehouseProductTransaction>();
                var temp = new List<WarehouseProductTransaction>();
                var order_products =
                    Session.FactoryEntities.ORDER_PRODUCT.ToList()
                        .Where(op => op.WAREHOUSE_ID == _CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                var scheduleProductInfos = Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                    .Where(psi => psi.PRODUCTION_SCHEDULE.WAREHOUSE_ID == _CurrentWarehouse.Warehouse.WAREHOUSE_ID)
                    .ToList();
                foreach (var package in order_products)
                {
                    temp.Add(new WarehouseProductTransaction(package));
                }
                foreach (var package in scheduleProductInfos)
                {
                    temp.Add(new WarehouseProductTransaction(package));
                }
                temp.Sort(
                    (transaction1, transaction2) =>
                    {
                        return transaction1.Date > transaction2.Date
                            ? 1
                            : transaction1.Date == transaction2.Date ? 0 : -1;
                    });
                foreach (var elem in temp)
                {
                    _InOutComeFlow.Add(elem);
                }
                var times = _InOutComeFlow.Select(el => el.Date).ToList();
                DateFilterString = times.Min().ToLongDateString() + " - " + times.Max().ToLongDateString();
                var values = _InOutComeFlow.Select(el => el.Quantity).ToList();
                var money = _InOutComeFlow.Select(el => el.MoneyQuantity).ToList();
                ValueRange = values.Min() + "шт. - " + values.Max() + "шт. і " + money.Min().ToString("N2") + " грн. - " +
                             money.Max().ToString("N2") + " грн.";
                var income = 0;
                var outcome = 0;
                double incomeMoney = 0;
                double outcomeMoney = 0;
                var direct = true;
                var reverse = true;
                foreach (var element in _InOutComeFlow)
                {
                    if (element.FlowType)
                    {
                        reverse = false;
                        income += element.Quantity;
                        incomeMoney += element.MoneyQuantity;
                    }
                    else
                    {
                        direct = false;
                        outcome += element.Quantity;
                        outcomeMoney += element.MoneyQuantity;
                    }
                }
                TotalIncome = income + " шт. або " + incomeMoney.ToString("N2") + " грн.";
                TotalOutcome = outcome + " шт. або " + outcomeMoney.ToString("N2") + " грн.";
                if ((!direct && !reverse) || (direct && reverse))
                {
                    FlowDirection = "Двосторонній";
                }
                else
                {
                    if (direct) FlowDirection = " Вхідний";
                    else FlowDirection = " Вихідний";
                }
                ProductsOnWarehouse.Clear();
                var tempProducts = Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                foreach (var product in tempProducts)
                {
                    ProductsOnWarehouse.Add(product);
                }
                Engaged = _CurrentWarehouse.Warehouse.CAPACITY - _CurrentWarehouse.Warehouse.FREE_SPACE;
                Schedules = new ChartValues<PRODUCTION_SCHEDULE>();

                var tempSchedules =
                    Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList()
                        .Where(prs => prs.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID)
                        .ToList();
                foreach (var schedule in tempSchedules)
                {
                    _Schedules.Add(schedule);
                }
                if (Schedules.Count > 0)
                    SelectedProductionSchedule = Schedules.First();
                else
                {
                    SelectedProductionSchedule = null;
                }
                OnPropertyChanged("CurrentWarehouse");
            }
        }

        public ObservableCollection<PRODUCTION_SCHEDULE> Schedules
        {
            get { return _Schedules; }
            set
            {
                _Schedules = value;
                OnPropertyChanged("Schedules");
            }
        }

        public PRODUCTION_SCHEDULE SelectedProductionSchedule
        {
            get { return _SelectedProductionSchedule; }
            set
            {
                _SelectedProductionSchedule = value;
                if (_SelectedProductionSchedule != null)
                {
                    SchedulePackages = new ObservableCollection<SCHEDULE_PRODUCT_INFO>();
                    var temp =
                        Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                            .Where(s => s.SCHEDULE_ID == SelectedProductionSchedule.SCHEDULE_ID)
                            .ToList();
                    foreach (var pack in temp)
                    {
                        SchedulePackages.Add(pack);
                    }
                    OnPropertyChanged("SelectedProductionSchedule");
                }
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

        public ObservableCollection<string> OptionsList
        {
            get { return _OptionsList; }
            set
            {
                _OptionsList = value;
                OnPropertyChanged("OptionsList");
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

        public ObservableCollection<ProductListElement> ProductsList
        {
            get { return _ProductsList; }
            set
            {
                _ProductsList = value;
                if (ProductsList.Count > 0)
                    SelectedProduct = ProductsList.First();
                OnPropertyChanged("ProductsList");
            }
        }

        public ProductListElement SelectedProduct
        {
            get { return _SelectedProduct; }
            set
            {
                if (value != null)
                {
                    _SelectedProduct = value;
                    SelectedProductPrice = API.getlastPrice(
                        Session.FactoryEntities.PRODUCT_PRICE
                            .ToList()
                            .FindAll(pr => pr.PRODUCT_INFO_ID == SelectedProduct.ProductInfo.PRODUCT_INFO_ID));
                    ProductPriceList = new ObservableCollection<ProductPriceListElement>();
                    foreach (var price in SelectedProduct.ProductInfo.PRODUCT_PRICE)
                    {
                        ProductPriceList.Add(new ProductPriceListElement(price));
                    }
                    UpdateSeries();
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ObservableCollection<SCHEDULE_PRODUCT_INFO> SchedulePackages
        {
            get { return _schedulePackages; }
            set
            {
                _schedulePackages = value;
                OnPropertyChanged("SchedulePackages");
            }
        }

        public decimal Engaged
        {
            get { return _Engaged; }
            set
            {
                _Engaged = value;
                OnPropertyChanged("Engaged");
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

        public PRODUCTION_SCHEDULE CurrentProductionSchedule
        {
            get { return _CurrentProductionSchedule; }
            set
            {
                _CurrentProductionSchedule = value;
                OnPropertyChanged("CurrentProductionSchedule");
            }
        }

        public string SelectedProductTitle
        {
            get { return _SelectedProductTitle; }
            set
            {
                _SelectedProductTitle = value;
                OnPropertyChanged("SelectedProductTitle");
            }
        }

        public ObservableCollection<string> ProductsTitleList
        {
            get { return _ProductsTitleList; }
            set
            {
                _ProductsTitleList = value;
                OnPropertyChanged("ProductsTitleList");
            }
        }

        public ObservableCollection<WarehouseProductTransaction> InOutComeFlow
        {
            get { return _InOutComeFlow; }
            set
            {
                _InOutComeFlow = value;
                OnPropertyChanged("InOutComeFlow");
            }
        }

        public ObservableCollection<WarehouseListItem> Warehouses
        {
            get { return _warehouses; }
            set
            {
                _warehouses = value;
                OnPropertyChanged("Warehouses");
            }
        }

        public string CurrentWarehouseString
        {
            get { return _CurrentWarehouseString; }
            set
            {
                _CurrentWarehouseString = value;
                if (WarehousesStrings != null)
                {
                    var index = WarehousesStrings.IndexOf(CurrentWarehouseString);
                    CurrentWarehouse = Warehouses.ElementAt(index);
                }
                OnPropertyChanged("CurrentWarehouseString");
            }
        }

        public ObservableCollection<string> WarehousesStrings
        {
            get { return _warehousesStrings; }
            set
            {
                _warehousesStrings = value;
                OnPropertyChanged("WarehousesStrings");
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
    }
}