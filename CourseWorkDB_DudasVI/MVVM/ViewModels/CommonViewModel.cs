using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public abstract class CommonViewModel : ViewModelBase
    {
        protected CommonViewModel()
        {
          
        }

        private bool _AddPermition;

        public bool AddPermition
        {
            get { return _AddPermition; }
            set
            {
                _AddPermition = value;
                OnPropertyChanged("AddPermition");
            }
        }

        public string userNameSurname
        {
            get
            {
                return Session.User.POST.POST_NAME + " " + Session.User.STAFF_NAME + " " + Session.User.STAFF_SURNAME;
            }
        }

        #region Common

        #region Variables

        protected ObservableCollection<ClientListItem> _Clients;
        protected ClientListItem _SelectedClient;
        protected ObservableCollection<ProductListElement> _ProductsList;
        protected ObservableCollection<string> _ProductsTitleList;
        protected ProductListElement _SelectedProduct;
        protected PRODUCT_PRICE _SelectedProductPrice;
        protected ObservableCollection<ProductPriceListElement> _ProductPriceList;
        protected double _ProductPriceValue;
        protected double _ProductPricePersentage;
        protected PRODUCTION_SCHEDULE _CurrentProductionSchedule;
        protected string _SelectedProductTitle;
        protected WarehouseListItem _CurrentWarehouse;
        private bool _ExtendedMode;
        protected ObservableCollection<WarehouseListItem> _Warehouses;
        protected WarehouseListItem _AllWarehouses;
        protected ObservableCollection<WarehouseProductTransaction> _InOutComeFlow;
        protected ObservableCollection<string> _WarehousesStrings;
        protected string _CurrentWarehouseString;
        protected string _DateFilterString;
        protected string _ValueRange;
        protected string _TotalIncome;
        protected string _TotalOutcome;
        protected string _FlowDirection;
        protected ObservableCollection<ReleasedProductListItem> _ProductsOnWarehouse;
        protected ReleasedProductListItem _SelectedProductOnWarehouse;
        protected decimal _Engaged;
        protected ObservableCollection<PRODUCTION_SCHEDULE> _Schedules;
        protected PRODUCTION_SCHEDULE _SelectedProductionSchedule;
        protected ObservableCollection<SCHEDULE_PRODUCT_INFO> _SchedulePackages;
        protected string _ChangedText;

        private DateTime _FromTime;

        private Dictionary<string, RegionInfo> _options;

        public class RegionInfo
        {
            public RegionInfo(int index, DateTime from, DateTime to)
            {
                this.index = index;
                this.from = from;
                this.to = to;
            }

            public int index { get; set; }
            public DateTime from { get; set; }
            public DateTime to { get; set; }
        }

        private ObservableCollection<string> _OptionsList;
        private decimal _priceFrom;
        private decimal _priceTo;

        private string _selectedCategory;
        private string _selectedOption;
        private DateTime _ToTime;

        private bool filterByPrice;

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

        public ObservableCollection<ReleasedProductListItem> ProductsOnWarehouse
        {
            get { return _ProductsOnWarehouse; }
            set
            {
                _ProductsOnWarehouse = value;
                if (_ProductsOnWarehouse.Count > 0)
                    SelectedProductOnWarehouse = _ProductsOnWarehouse.First();
                OnPropertyChanged("ProductsOnWarehouse");
            }
        }

        public ReleasedProductListItem SelectedProductOnWarehouse
        {
            get { return _SelectedProductOnWarehouse; }
            set
            {
                _SelectedProductOnWarehouse = value;
                OnPropertyChanged("SelectedProductOnWarehouse");
            }
        }

        public ObservableCollection<ClientListItem> Clients
        {
            get { return _Clients; }
            set
            {
                _Clients = value;
                _Clients.ToList().Sort((client1, client2) =>
                {
                    var name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME, true);
                    var surname = string.Compare(client1.Client.CLIENT_SURNAME, client2.Client.CLIENT_SURNAME, true);
                    var middle_name = string.Compare(client1.Client.CLIENT_MIDDLE_NAME,
                        client2.Client.CLIENT_MIDDLE_NAME);
                    return surname == 0 ? name == 0 ? middle_name == 0 ? 0 : middle_name : name : surname;
                });
                OnPropertyChanged("Clients");
            }
        }

        public ClientListItem SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                _SelectedClient = value;
                OnPropertyChanged("SelectedClient");
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
                if (times.Count > 0)
                    DateFilterString = times.Min().ToLongDateString() + " - " + times.Max().ToLongDateString();
                var values = _InOutComeFlow.Select(el => el.Quantity).ToList();
                var money = _InOutComeFlow.Select(el => el.MoneyQuantity).ToList();
                if (values.Count > 0 && money.Count > 0)
                    ValueRange = values.Min() + "шт. - " + values.Max() + "шт. і " + money.Min().ToString("N2") +
                                 " грн. - " +
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
                if (ExtendedMode)
                {
                    
                }
                else
                {
                    
                }
                List<RELEASED_PRODUCT> tempProducts = Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                var distinctProduct = tempProducts.GroupBy(p => p.PRODUCT_INFO.PRODUCT_TITLE).ToDictionary(group => group.Key, group => group.ToList());

                foreach (var product in distinctProduct)
                {
                    var releasedProduct = new ReleasedProductListItem(product.Value.First());
                    foreach (var item in product.Value)
                    {
                        releasedProduct.Quantity += item.QUANTITY;
                    }
                    this.ProductsOnWarehouse.Add(releasedProduct);
                }
                Engaged = _CurrentWarehouse.Warehouse.CAPACITY - _CurrentWarehouse.Warehouse.FREE_SPACE;
                OnPropertyChanged("CurrentWarehouse");
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
            get { return _Warehouses; }
            set
            {
                _Warehouses = value;
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
                    if (!_CurrentWarehouseString.Equals("Всі склади"))
                    {
                        var index = WarehousesStrings.IndexOf(CurrentWarehouseString);
                        CurrentWarehouse = Warehouses.ElementAt(index - 1);
                    }
                    else
                    {
                        CurrentWarehouse = Warehouses.ElementAt(0);
                    }
                }
                OnPropertyChanged("CurrentWarehouseString");
            }
        }

        public ObservableCollection<string> WarehousesStrings
        {
            get { return _WarehousesStrings; }
            set
            {
                _WarehousesStrings = value;
                OnPropertyChanged("WarehousesStrings");
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
        public bool ExtendedMode
        {
            get { return _ExtendedMode; }
            set
            {
                _ExtendedMode = value;
                ColumnVisibilityChanged();
                OnPropertyChanged("ExtendedMode");
            }
        }

        private bool ChangeProductPriceValue;
        private bool ChangeProductPricePersentage;

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

        public ObservableCollection<SCHEDULE_PRODUCT_INFO> SchedulePackages
        {
            get { return _SchedulePackages; }
            set
            {
                _SchedulePackages = value;
                OnPropertyChanged("SchedulePackages");
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

        public string ChangedText
        {
            get { return _ChangedText; }
            set
            {
                _ChangedText = value;
                OnPropertyChanged("ChangedText");
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

        public Dictionary<string, RegionInfo> options
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

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged("SelectedCategory");
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

        #endregion

        #region Commands

        #endregion

        #endregion

        public abstract void ColumnVisibilityChanged();
    }
}