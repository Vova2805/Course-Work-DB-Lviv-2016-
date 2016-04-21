using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using LiveCharts;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class DirectorViewModel : ViewModelBase
    {
        private ObservableCollection<STAFF> _EmployeeList;
        private STAFF _SelectedEmployee;

        #region Second

        private ObservableCollection<ClientListItem> _Clients;
        private ClientListItem _SelectedClient;
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

        public ObservableCollection<RELEASED_PRODUCT> ProductsOnWarehouse
        {
            get { return _ProductsOnWarehouse; }
            set
            {
                _ProductsOnWarehouse = value;
                OnPropertyChanged("ProductsOnWarehouse");
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
                var tempProducts = Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                    .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                foreach (var product in tempProducts)
                {
                    ProductsOnWarehouse.Add(product);
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
            get { return _warehousesStrings; }
            set
            {
                _warehousesStrings = value;
                OnPropertyChanged("WarehousesStrings");
            }
        }

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
            get { return _schedulePackages; }
            set
            {
                _schedulePackages = value;
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
            get { return _changedText; }
            set
            {
                _changedText = value;
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

        #endregion
    }
}