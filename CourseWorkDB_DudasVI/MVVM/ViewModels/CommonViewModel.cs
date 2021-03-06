﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Resources;
using CourseWorkDB_DudasVI.Views;
using CourseWorkDB_DudasVI.Views.Dialogs;
using CourseWorkDB_DudasVI.Views.UserControls;
using GalaSoft.MvvmLight.Ioc;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class CommonViewModel : ViewModelBaseInside
    {
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

        #region Dialog

        static CommonViewModel()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DialogViewModel>();
            SimpleIoc.Default.Register<IDialogCoordinator, DialogCoordinator>();
            SimpleIoc.Default.Register<DialogViewModel>();
        }

        public CommonViewModel()
        {
            InitializeViewModel();
        }

        public void InitializeViewModel()
        {
            CategoriesList = new ObservableCollection<string>();
            foreach (var category in Session.FactoryEntities.CATEGORY.ToList().Select(c => c.CATEGORY_TITLE).ToList())
            {
                CategoriesList.Add(category);
            }
            CategoriesList.Insert(0, "Всі категорії");
            PriceFrom = Session.FactoryEntities.PRODUCT_PRICE.ToList().Min(p => p.PRICE_VALUE);
            PriceTo = Session.FactoryEntities.PRODUCT_PRICE.ToList().Max(p => p.PRICE_VALUE);
            SelectedCategory = CategoriesList.First();
            ChangedText = "";
            LineSeriesInstance = new SeriesCollection();
            PieSeriesInstance = new SeriesCollection();
            BarSeriesInstance = new SeriesCollection();

            FromTime = API.getLastPlanDate(Session.User);
            ToTime = API.getTodayDate();
            Options = new Dictionary<string, RegionInfo>();
            Options.Add(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString(),
                new RegionInfo(0, FromTime, ToTime));
            OptionsList = new ObservableCollection<string>();
            foreach (var option in Options.Keys.ToList())
            {
                OptionsList.Add(option);
            }
            SelectedOption = OptionsList.First();
            Labels = new ObservableCollection<string>();
            Schedules = new ObservableCollection<PRODUCTION_SCHEDULE>();


            var temp = Session.FactoryEntities.CLIENT.ToList();
            Clients = new ObservableCollection<ClientListItem>();
            ClientsTitle = new ObservableCollection<string>();
            foreach (var client in temp)
            {
                Clients.Add(new ClientListItem(client));
            }
            Clients.ToList().Sort((client1, client2) =>
            {
                var name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME, true);
                var surname = string.Compare(client1.Client.CLIENT_SURNAME, client2.Client.CLIENT_SURNAME, true);
                var middleName = string.Compare(client1.Client.CLIENT_MIDDLE_NAME, client2.Client.CLIENT_MIDDLE_NAME);
                return surname == 0 ? name == 0 ? middleName == 0 ? 0 : middleName : name : surname;
            });
            foreach (var client in Clients)
            {
                ClientsTitle.Add(client.GeneralInfo);
            }
            if (Clients.Count > 0)
            {
                SelectedClient = Clients.First();
                SelectedClientTitle = ClientsTitle.First();
            }

            var tempProducts = Session.FactoryEntities.PRODUCT_INFO.ToList();
            ProductsList = new ObservableCollection<ProductListElement>();
            foreach (var product in tempProducts)
            {
                ProductsList.Add(new ProductListElement(product, this));
            }
            if (ProductsList.Count > 0)
            {
                SelectedProduct = ProductsList.First();
            }

            Distance = 0;
            var deliveries = Session.FactoryEntities.DELIVERY.ToList();
            deliveries.Sort(
                (del1, del2) =>
                {
                    return del1.DELIVERY_DATE > del2.DELIVERY_DATE
                        ? 1
                        : del1.DELIVERY_DATE == del2.DELIVERY_DATE ? 0 : -1;
                }
                );
            CostPerKm = deliveries.Last().COST_PER_KM;

            Warehouses = new ObservableCollection<WarehouseListItem>();
            WarehousesAddresss = new ObservableCollection<ADDRESS>();
            var tempWarehouses =
                Session.FactoryEntities.WAREHOUSE.ToList();
            foreach (var warehouse in tempWarehouses)
            {
                Warehouses.Add(new WarehouseListItem(warehouse));
                WarehousesAddresss.Add(warehouse.ADDRESS1);
            }
            if (Warehouses.Count > 0)
                CurrentWarehouse = Warehouses.First();
            WarehousesStrings = new ObservableCollection<string>();
            WarehousesAddresssStrings = new ObservableCollection<string>();
            var i = 0;
            foreach (var warehouse in Warehouses)
            {
                string result = "";
                var staff = warehouse.Warehouse.STAFF;
                if (staff != null)
                    result += staff.STAFF_NAME + " " + staff.STAFF_SURNAME + " " + staff.POST.POST_NAME + " " +
                              staff.MOBILE_PHONE;
                else result = "Фахівці з продажу";
                WarehousesStrings.Add(API.ConvertAddress(warehouse.Warehouse.ADDRESS1, ++i + ".", "Відповідальний :" + result));
                WarehousesAddresssStrings.Add(WarehousesStrings.Last());
            }
            if (WarehousesAddresssStrings.Count > 0)
            {
                SelectedWarehouseAddressString = WarehousesAddresssStrings.First();
            }
            if (!IsSaler)
                WarehousesStrings.Insert(0, ResourceClass.ALL_WAREHOUSES);
            CurrentWarehouseString = WarehousesStrings.First();
            WarehousesStringsWithoutAll = new ObservableCollection<string>();
            foreach (var title in _warehousesStrings)
            {
                if (!title.Equals(ResourceClass.ALL_WAREHOUSES))
                    WarehousesStringsWithoutAll.Add(title);
            }
            var groupedPackages =
                Session.FactoryEntities.ORDER_PRODUCT.ToList()
                    .GroupBy(pr => pr.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
            i = 0;
            ProductPackagesList = new ObservableCollection<OrderProductTransaction>();
            foreach (var group in groupedPackages)
            {
                ProductPackagesList.Add(new OrderProductTransaction(i++, group.Key, group.Value, Session.User));
            }

            SelectedProductPrice = API.getlastPrice(SelectedProduct.ProductInfo.PRODUCT_PRICE);
            ProductPriceValue = (double)SelectedProductPrice.PRICE_VALUE;
            ProductPricePersentage = (double)SelectedProductPrice.PERSENTAGE_VALUE;
            ProductPriceList = new ObservableCollection<ProductPriceListElement>();
            foreach (var price in SelectedProduct.ProductInfo.PRODUCT_PRICE.ToList())
            {
                ProductPriceList.Add(new ProductPriceListElement(price));
            }

            ProductsTitleList = new ObservableCollection<string>();
            foreach (var productTitle in ProductsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList())
            {
                ProductsTitleList.Add(productTitle);
            }
            ProductsTitleList.Insert(0, "Всі продукти");
            SelectedProductTitle = ProductsTitleList.First();
            CurrentProductionSchedule = new PRODUCTION_SCHEDULE();
            LostProducts = 0;
            LostMoney = 0;
            Days = 7;

            EmployeeList = new ObservableCollection<EmployeeListItem>();
            foreach (
                var employee in
                    Session.FactoryEntities.STAFF.ToList().FindAll(s => s.POST.DEPARTMENT.DEPARTMENT_ID == 3))
            {
                EmployeeList.Add(new EmployeeListItem(employee, employee.POST));
            }
            SelectedEmployee = EmployeeList.First();
            IsSaler = Session.userType == UserType.Saler;
            NewEmployee = InitializeNewEmployee();
            NewClient = InitializeNewClient();
            Posts = new ObservableCollection<POST>();
            PostsTitles = new ObservableCollection<string>();
            var tempPosts = Session.FactoryEntities.POST.ToList().Where(p => p.DEPARTMENT.DEPARTMENT_ID == 3).ToList();
            //відділ збуту
            foreach (var post in tempPosts)
            {
                Posts.Add(post);
                PostsTitles.Add(post.POST_NAME);
            }
            if (Posts.Count > 0)
            {
                SelectedPost = Posts.First();
                SelectedPostTitle = PostsTitles.First();
            }
        }

        private EmployeeListItem InitializeNewEmployee()
        {
            var newEmployeePattern = new STAFF();
            newEmployeePattern.POST = Session.User.POST;
            newEmployeePattern.BIRTH_DATE = API.getTodayDate();
            newEmployeePattern.STAFF_NAME = "Ім'я працівника";
            newEmployeePattern.STAFF_SURNAME = "Прізвище працівника";
            newEmployeePattern.EMAIL = "email@email.com";
            newEmployeePattern.LOGIN = "test";
            newEmployeePattern.PASSWORD = "test";
            var address = new ADDRESS();
            address.COUNTRY = "Країна";
            address.REGION = "Західний";
            address.CITY = "Київ";
            address.STREET = "Південна";
            address.BUILDING_NUMBER = 1;

            var temp = new EmployeeListItem(newEmployeePattern, newEmployeePattern.POST);
            temp.Employee.ADDRESS1 = address;
            return temp;
        }

        private ClientListItem InitializeNewClient()
        {
            var newClientPattern = new CLIENT();
            newClientPattern.CLIENT_NAME = "Ім'я клієнта";
            newClientPattern.CLIENT_SURNAME = "Прізвище клієнта";
            newClientPattern.CLIENT_MIDDLE_NAME = "По-батькові клієнта";
            newClientPattern.COMPANY_TITLE = "Нова";
            newClientPattern.MOBILE_PHONE = "02";
            newClientPattern.EMAIL = "Електронна пошта";
            newClientPattern.SALE_ORDER = new List<SALE_ORDER>();
            var address = new ADDRESS();
            address.COUNTRY = "Країна";
            address.REGION = "Західний";
            address.CITY = "Київ";
            address.STREET = "Південна";
            address.BUILDING_NUMBER = 1;

            var temp = new ClientListItem(newClientPattern);
            temp.Client.ADDRESS1 = address;
            return temp;
        }

        #endregion

        #region Common

        #region Variables

        private ObservableCollection<string> _categoriesList;
        private ObservableCollection<SCHEDULE_PRODUCT_INFO> _schedulePackages;
        private ObservableCollection<ClientListItem> _clients;
        private ClientListItem _selectedClient;
        private ObservableCollection<ProductListElement> _productsList;
        private ObservableCollection<string> _productsTitleList;
        private PRODUCT_PRICE _selectedProductPrice;
        private SALARY _EmployeePostSalary;
        private ObservableCollection<ProductPriceListElement> _productPriceList;
        private double _productPriceValue;
        private double _productPricePersentage;
        private PRODUCTION_SCHEDULE _currentProductionSchedule;
        private string _selectedProductTitle;
        private WarehouseListItem _currentWarehouse;
        private bool _extendedMode;
        private ObservableCollection<WarehouseListItem> _warehouses;
        private ObservableCollection<WarehouseProductTransaction> _inOutComeFlow;
        private ObservableCollection<string> _warehousesStrings;
        private ObservableCollection<string> _warehousesStringsWithoutAll;
        private ObservableCollection<ADDRESS> _warehousesAddresss;
        private ADDRESS _selectedWarehouseAddress;
        private ObservableCollection<string> _warehousesAddresssStrings;
        private string _selectedWarehouseAddressString;

        private string _currentWarehouseString;
        private string _dateFilterString;
        private string _valueRange;
        private string _totalIncome;
        private string _totalOutcome;
        private string _flowDirection;
        private ObservableCollection<ReleasedProductListItem> _productsOnWarehouse;
        private ReleasedProductListItem _selectedProductOnWarehouse;
        private decimal _engaged;
        private ObservableCollection<PRODUCTION_SCHEDULE> _schedules;
        private PRODUCTION_SCHEDULE _selectedProductionSchedule;
        private string _changedText;
        private DateTime _fromTime;
        private Dictionary<string, RegionInfo> _options;
        private ObservableCollection<string> _optionsList;
        private decimal _priceFrom;
        private decimal _priceTo;

        private string _selectedCategory;
        private string _selectedOption;
        private DateTime _toTime;

        private bool _filterByPrice;

        private ObservableCollection<string> _clientsTitle;
        private string _selectedClientTitle;
        private ProductListElement _selectedProduct;
        private WAREHOUSE _selectedWarehouse;
        private string _selectedWarehouseTitle;
        private decimal _distance;
        private decimal _costPerKm;
        private SeriesCollection _lineSeriesInstance;
        private SeriesCollection _pieSeriesInstance;
        private SeriesCollection _barSeriesInstance;
        private ObservableCollection<string> _labels;
        private string _xTitle;
        private string _yTitle;
        private int _tabIndex;
        private ObservableCollection<OrderProductTransaction> _productPackagesList;
        private bool _isSaler;
        private ObservableCollection<EmployeeListItem> _EmployeeList;
        private EmployeeListItem _SelectedEmployee;
        private ClientListItem _newClient;
        private EmployeeListItem _newEmployee;
        private bool _newClientEditing;
        private bool _newEmployeeEditing;
        private ObservableCollection<POST> _posts;
        private POST _selectedPost;
        private ObservableCollection<string> _postsTitles;
        private string _selectedPostTitle;



        public void CurrentWarehouseChanged()
        {
            ExtendedMode = _extendedMode;
        }

        public ClientListItem NewClient
        {
            get { return _newClient; }
            set
            {
                _newClient = value;
                OnPropertyChanged("NewClient");
            }
        }

        public EmployeeListItem NewEmployee
        {
            get { return _newEmployee; }
            set
            {
                _newEmployee = value;
                OnPropertyChanged("NewEmployee");
            }
        }

        public bool NewEmployeeEditing
        {
            get { return _newEmployeeEditing; }
            set
            {
                _newEmployeeEditing = value;
                if (!value)
                    VisibilityDuringAddingNew = true;
                OnPropertyChanged("NewEmployeeEditing");
            }
        }

        public bool NewClientEditing
        {
            get { return _newClientEditing; }
            set
            {
                _newClientEditing = value;
                if (!value)
                    VisibilityDuringAddingNew = true;
                OnPropertyChanged("NewClientEditing");
            }
        }

        public ADDRESS SelectedWarehouseAddress
        {
            get { return _selectedWarehouseAddress; }
            set
            {
                _selectedWarehouseAddress = value;
                SelectedClient.NewDelivery.DeliveryAddress.ADDRESS = _selectedWarehouseAddress;
                OnPropertyChanged("SelectedWarehouseAddress");
            }
        }

        public ObservableCollection<ADDRESS> WarehousesAddresss
        {
            get { return _warehousesAddresss; }
            set
            {
                _warehousesAddresss = value;
                OnPropertyChanged("WarehousesAddresss");
            }
        }

        public ObservableCollection<string> WarehousesAddresssStrings
        {
            get { return _warehousesAddresssStrings; }
            set
            {
                _warehousesAddresssStrings = value;
                OnPropertyChanged("WarehousesAddresssStrings");
            }
        }

        public string SelectedWarehouseAddressString
        {
            get { return _selectedWarehouseAddressString; }
            set
            {
                _selectedWarehouseAddressString = value;
                if (WarehousesAddresssStrings != null)
                {
                    int index = WarehousesAddresssStrings.ToList().IndexOf(_selectedWarehouseAddressString);
                    if (index >= 0 && WarehousesAddresss != null)
                    {
                        SelectedWarehouseAddress = WarehousesAddresss.ElementAt(index);
                    }
                }
                OnPropertyChanged("SelectedWarehouseAddressString");
            }
        }

        public void UpdateView()
        {
            var temProductsOnWarehouse = new ObservableCollection<ReleasedProductListItem>();
            if (_extendedMode)
            {
                //not grouping

                foreach (var product in tempProducts)
                {
                    var releasedProduct = new ReleasedProductListItem(product, CurrentWarehouse);
                    releasedProduct.Quantity = product.QUANTITY;
                    temProductsOnWarehouse.Add(releasedProduct);
                }
            }
            else
            {
                //grouping by title and sum quantity
                distinctProduct = tempProducts.GroupBy(p => p.PRODUCT_INFO.PRODUCT_TITLE)
                    .ToDictionary(group => group.Key, group => group.ToList());
                foreach (var product in distinctProduct)
                {
                    var releasedProduct = new ReleasedProductListItem(product.Value.First(), CurrentWarehouse);
                    foreach (var item in product.Value)
                    {
                        releasedProduct.Quantity += item.QUANTITY;
                    }
                    temProductsOnWarehouse.Add(releasedProduct);
                }
            }
            ProductsOnWarehouse = temProductsOnWarehouse;
        }

        #endregion

        #region Properties

        public WAREHOUSE SelectedWarehouse
        {
            get { return _selectedWarehouse; }
            set
            {
                _selectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }

        public string SelectedWarehouseTitle
        {
            get { return _selectedWarehouseTitle; }
            set
            {
                _selectedWarehouseTitle = value;
                OnPropertyChanged("SelectedWarehouseTitle");
            }
        }

        public string userNameSurname
        {
            get
            {
                return Session.User.POST.POST_NAME + " " + Session.User.STAFF_NAME + " " + Session.User.STAFF_SURNAME;
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

        public string DateFilterString
        {
            get { return _dateFilterString; }
            set
            {
                _dateFilterString = value;
                OnPropertyChanged("DateFilterString");
            }
        }

        public string FlowDirection
        {
            get { return _flowDirection; }
            set
            {
                _flowDirection = value;
                OnPropertyChanged("FlowDirection");
            }
        }

        public string TotalIncome
        {
            get { return _totalIncome; }
            set
            {
                _totalIncome = value;
                OnPropertyChanged("TotalIncome");
            }
        }

        public string TotalOutcome
        {
            get { return _totalOutcome; }
            set
            {
                _totalOutcome = value;
                OnPropertyChanged("TotalOutcome");
            }
        }

        public string ValueRange
        {
            get { return _valueRange; }
            set
            {
                _valueRange = value;
                OnPropertyChanged("ValueRange");
            }
        }

        public ObservableCollection<ReleasedProductListItem> ProductsOnWarehouse
        {
            get { return _productsOnWarehouse; }
            set
            {
                _productsOnWarehouse = value;
                if (_productsOnWarehouse.Count > 0)
                {
                    SelectedProductOnWarehouse = _productsOnWarehouse.First();
                    CurrentWarehouse.ItemsQuantity = _productsOnWarehouse.Count;
                }

                OnPropertyChanged("ProductsOnWarehouse");
            }
        }

        public bool IsSaler
        {
            get
            {
                bool temp = Session.userType == UserType.Saler;
                if (_isSaler != temp) IsSaler = temp;
                return _isSaler;
            }
            set
            {
                _isSaler = value;
                foreach (var product in ProductsList)
                {
                    product.IsntSaler = !_isSaler;
                }
                OnPropertyChanged("IsSaler");
            }
        }

        public ReleasedProductListItem SelectedProductOnWarehouse
        {
            get { return _selectedProductOnWarehouse; }
            set
            {
                _selectedProductOnWarehouse = value;
                OnPropertyChanged("SelectedProductOnWarehouse");
            }
        }

        public ClientListItem SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                if (SelectedClient != null)
                {
                    SelectedClientTitle = _selectedClient.GeneralInfo;
                }
                else
                {
                    if (SelectedClientTitle != null)
                        _selectedClient =
                            Clients.ToList().FindAll(c => c.GeneralInfo.Equals(SelectedClientTitle)).FirstOrDefault();
                }
                if (_selectedClient != null)
                    if (_selectedClient.Equals(NewClient))
                    {
                        if (NewClientEditing)
                        {
                            VisibilityDuringAddingNew = false;
                        }
                        else VisibilityDuringAddingNew = true;
                    }
                    else VisibilityDuringAddingNew = true;
                if (SelectedClient != null)
                    SelectedClient.init();
                OnPropertyChanged("SelectedClient");
            }
        }

        public string SelectedClientTitle
        {
            get { return _selectedClientTitle; }
            set
            {
                _selectedClientTitle = value;
                if (SelectedClient == null || !SelectedClient.GeneralInfo.Equals(SelectedClientTitle))
                {
                    SelectedClient =
                        Clients.ToList().FindAll(c => c.GeneralInfo.Equals(SelectedClientTitle)).FirstOrDefault();
                }
                OnPropertyChanged("SelectedClientTitle");
            }
        }

        public decimal CostPerKm
        {
            get { return _costPerKm; }
            set
            {
                _costPerKm = value;
                OnPropertyChanged("CostPerKm");
            }
        }

        public decimal Distance
        {
            get { return _distance; }
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }

        public ObservableCollection<ClientListItem> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                _clients.ToList().Sort((client1, client2) =>
                {
                    var name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME, true);
                    var surname = string.Compare(client1.Client.CLIENT_SURNAME, client2.Client.CLIENT_SURNAME, true);
                    var middle_name = string.Compare(client1.Client.CLIENT_MIDDLE_NAME,
                        client2.Client.CLIENT_MIDDLE_NAME);
                    return surname == 0 ? name == 0 ? middle_name == 0 ? 0 : middle_name : name : surname;
                });
                var titles = _clients.ToList().Select(c => c.GeneralInfo).ToList();

                ClientsTitle = new ObservableCollection<string>();
                foreach (var title in titles)
                {
                    ClientsTitle.Add(title);
                }
                if (SelectedClient != null)
                    SelectedClientTitle = SelectedClient.GeneralInfo;
                OnPropertyChanged("Clients");
            }
        }

        public bool isAllWarehouses
        {
            get { return !CurrentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES); }
        }

        private List<RELEASED_PRODUCT> tempProducts = new List<RELEASED_PRODUCT>();

        private Dictionary<string, List<RELEASED_PRODUCT>> distinctProduct =
            new Dictionary<string, List<RELEASED_PRODUCT>>();

        public WarehouseListItem CurrentWarehouse
        {
            get { return _currentWarehouse; }
            set
            {
                _currentWarehouse = value;

                InOutComeFlow = new ChartValues<WarehouseProductTransaction>();
                var temp = new List<WarehouseProductTransaction>();

                var order_products = new List<ORDER_PRODUCT>();
                var scheduleProductInfos = new List<SCHEDULE_PRODUCT_INFO>();
                tempProducts = new List<RELEASED_PRODUCT>();
                distinctProduct = new Dictionary<string, List<RELEASED_PRODUCT>>();
                Schedules = new ObservableCollection<PRODUCTION_SCHEDULE>();
                Engaged = 0;
                var tempSchedules = new List<PRODUCTION_SCHEDULE>();
                if (_currentWarehouse == null) //Get info about all warehouses
                {
                    var tempWarehouse = new WAREHOUSE();
                    tempWarehouse.CAPACITY = 0;
                    tempWarehouse.ENGAGED_SPACE = 0;
                    foreach (var warehouse in Warehouses)
                    {
                        tempWarehouse.CAPACITY += warehouse.Warehouse.CAPACITY;
                        tempWarehouse.ENGAGED_SPACE += warehouse.Warehouse.ENGAGED_SPACE;
                        order_products.AddRange(Session.FactoryEntities.ORDER_PRODUCT.ToList()
                            .Where(op => op.WAREHOUSE_ID == warehouse.Warehouse.WAREHOUSE_ID).ToList());

                        scheduleProductInfos.AddRange(Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                            .Where(psi => psi.PRODUCTION_SCHEDULE.WAREHOUSE_ID == warehouse.Warehouse.WAREHOUSE_ID)
                            .ToList());
                        tempSchedules.AddRange(
                            Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList().
                                Where(ps => ps.WAREHOUSE_ID == warehouse.Warehouse.WAREHOUSE_ID).ToList()
                            );
                        tempProducts.AddRange(Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                            .Where(rp => rp.WAREHOUSE_ID == warehouse.Warehouse.WAREHOUSE_ID).ToList());
                    }
                    _currentWarehouse = new WarehouseListItem(tempWarehouse);
                    _currentWarehouseString = ResourceClass.ALL_WAREHOUSES;
                }
                else
                {
                    order_products =
                        Session.FactoryEntities.ORDER_PRODUCT.ToList()
                            .Where(op => op.WAREHOUSE_ID == _currentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                    scheduleProductInfos = Session.FactoryEntities.SCHEDULE_PRODUCT_INFO.ToList()
                        .Where(psi => psi.PRODUCTION_SCHEDULE.WAREHOUSE_ID == _currentWarehouse.Warehouse.WAREHOUSE_ID)
                        .ToList();
                    tempSchedules.AddRange(
                        Session.FactoryEntities.PRODUCTION_SCHEDULE.ToList().
                            Where(ps => ps.WAREHOUSE_ID == _currentWarehouse.Warehouse.WAREHOUSE_ID).ToList()
                        );

                    tempProducts = Session.FactoryEntities.RELEASED_PRODUCT.ToList()
                        .Where(rp => rp.WAREHOUSE_ID == CurrentWarehouse.Warehouse.WAREHOUSE_ID).ToList();
                }
                foreach (var schedule in tempSchedules)
                {
                    Schedules.Add(schedule);
                }
                if (Schedules.Count > 0)
                {
                    SelectedProductionSchedule = _schedules.First();
                }
                UpdateView();

                if (CurrentWarehouseString != null)
                    if (!_extendedMode && !_currentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                    {
                        foreach (var product in _productsOnWarehouse)
                        {
                            product.IsBooked = _currentWarehouse.Contains(product.ReleasedProduct.PRODUCT_INFO);
                            if (product.IsBooked)
                            {
                                var tempProd =
                                    _currentWarehouse.ContainsProductInfo(product.ReleasedProduct.PRODUCT_INFO)
                                        .ProductInfo;
                                product.QuantityNeeded = tempProd == null
                                    ? product.Quantity
                                    : tempProd.QUANTITY_IN_SCHEDULE + product.Quantity;
                            }
                        }
                    }


                Engaged = _currentWarehouse.Warehouse.ENGAGED_SPACE;
                CurrentWarehouse.ItemsQuantity = ProductsOnWarehouse.Count;

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
                    _inOutComeFlow.Add(elem);
                }
                var times = _inOutComeFlow.Select(el => el.Date).ToList();
                if (times.Count > 0)
                    DateFilterString = times.Min().ToLongDateString() + " - " + times.Max().ToLongDateString();
                var values = _inOutComeFlow.Select(el => el.Quantity).ToList();
                var money = _inOutComeFlow.Select(el => el.MoneyQuantity).ToList();
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
                foreach (var element in _inOutComeFlow)
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
                if (ProductsList == null)
                    ProductsList = null;
                else ProductsList = _productsList;
                OnPropertyChanged("CurrentWarehouse");
                OnPropertyChanged("isAllWarehouses");
                CurrentWarehouseChanged();
            }
        }

        public ObservableCollection<string> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                OnPropertyChanged("CategoriesList");
            }
        }

        public decimal Engaged
        {
            get { return _engaged; }
            set
            {
                _engaged = value;
                OnPropertyChanged("Engaged");
            }
        }

        public ObservableCollection<WarehouseProductTransaction> InOutComeFlow
        {
            get { return _inOutComeFlow; }
            set
            {
                _inOutComeFlow = value;
                OnPropertyChanged("InOutComeFlow");
            }
        }

        public ObservableCollection<WarehouseListItem> Warehouses
        {
            get
            {
                if (_warehouses == null)
                    Warehouses = null;
                return _warehouses;
            }
            set
            {
                _warehouses = value;
                if (_warehouses == null)
                {
                    _warehouses = new ObservableCollection<WarehouseListItem>();
                    var tempWarehouses =
                        Session.FactoryEntities.WAREHOUSE.ToList();
                    if (!IsSaler)
                    {
                        tempWarehouses =
                            tempWarehouses.ToList().Where(w => w.STAFF_ID == Session.User.STAFF_ID).ToList();
                    }
                    foreach (var warehouse in tempWarehouses)
                    {
                        _warehouses.Add(new WarehouseListItem(warehouse));
                    }
                    WarehousesStrings = new ObservableCollection<string>();
                    var i = 0;
                    foreach (var warehouse in _warehouses)
                    {
                        string result = "";
                        var staff = warehouse.Warehouse.STAFF;
                        if (staff != null)
                            result += staff.STAFF_NAME + " " + staff.STAFF_SURNAME + " " + staff.POST.POST_NAME + " " +
                                      staff.MOBILE_PHONE;
                        else result = "Фахівці з продажу";
                        WarehousesStrings.Add(API.ConvertAddress(warehouse.Warehouse.ADDRESS1, ++i + ".", "Відповідальний :" + result));
                    }
                    if (!IsSaler)
                        WarehousesStrings.Insert(0, ResourceClass.ALL_WAREHOUSES);
                    CurrentWarehouseString = _warehousesStrings.First();
                }
                OnPropertyChanged("Warehouses");
            }
        }

        public string CurrentWarehouseString
        {
            get { return _currentWarehouseString; }
            set
            {
                _currentWarehouseString = value;
                if (_currentWarehouseString != null)
                {
                    if (!_currentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                    {
                        var index = WarehousesStrings.IndexOf(CurrentWarehouseString);
                        if (WarehousesStrings.Contains(ResourceClass.ALL_WAREHOUSES) && index > 0) index--;
                        CurrentWarehouse = Warehouses.ElementAt(index);
                    }
                    else
                    {
                        CurrentWarehouse = null;
                    }
                }
                else
                {
                    _currentWarehouseString = ResourceClass.ALL_WAREHOUSES;
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
                if (!IsSaler)
                {
                    WarehousesStringsWithoutAll = new ObservableCollection<string>();
                    foreach (var valueString in WarehousesStrings)
                    {
                        WarehousesStringsWithoutAll.Add(valueString);
                    }
                    if (WarehousesStrings.Contains(ResourceClass.ALL_WAREHOUSES))
                        WarehousesStringsWithoutAll.RemoveAt(0);
                }

                OnPropertyChanged("WarehousesStrings");
            }
        }

        public bool ChangeProductPriceValue;
        public bool ChangeProductPricePersentage;

        public double ProductPriceValue
        {
            get { return _productPriceValue; }
            set
            {
                _productPriceValue = value;
                if (value != null && SelectedProductPrice != null)
                    SelectedProductPrice.PRICE_VALUE = (decimal)_productPriceValue;
                if (ChangeProductPricePersentage)
                {
                    ChangeProductPriceValue = false; //to avoid endless cycle
                    ProductPricePersentage = ProductPriceValue / (double)SelectedProduct.ProductInfo.PRODUCTION_PRICE * 100;
                }
                OnPropertyChanged("ProductPriceValue");
            }
        }

        public double ProductPricePersentage
        {
            get { return _productPricePersentage; }
            set
            {
                _productPricePersentage = value;
                if (value != null && SelectedProductPrice != null)
                    SelectedProductPrice.PERSENTAGE_VALUE = (decimal)_productPricePersentage;
                if (ChangeProductPriceValue)
                {
                    ChangeProductPricePersentage = false;
                    ProductPriceValue = (double)SelectedProduct.ProductInfo.PRODUCTION_PRICE * ProductPricePersentage /
                                        100.0;
                }
                OnPropertyChanged("ProductPricePersentage");
            }
        }


        public PRODUCT_PRICE SelectedProductPrice
        {
            get { return _selectedProductPrice; }
            set
            {
                _selectedProductPrice = value;
                if (_selectedProductPrice != null)
                {
                    ChangeProductPriceValue = false;
                    ChangeProductPricePersentage = false;
                    ProductPriceValue = (double)SelectedProductPrice.PRICE_VALUE;
                    ProductPricePersentage = (double)SelectedProductPrice.PERSENTAGE_VALUE;
                }
                OnPropertyChanged("SelectedProductPrice");
            }
        }


        public ObservableCollection<ProductListElement> ProductsList
        {
            get
            {
                if (_productsList == null)
                {
                    //make new Product list
                    ProductsList = null;
                }
                return _productsList;
            }
            set
            {
                _productsList = value;
                if (_productsList == null)
                {
                    _productsList = new ObservableCollection<ProductListElement>();
                    var products = Session.FactoryEntities.PRODUCT_INFO.ToList();
                    foreach (var product in products)
                    {
                        _productsList.Add(new ProductListElement(product, this));
                        _productsList.Last().IsntSaler = !IsSaler;
                    }
                    if (_productsList.Count > 0)
                        SelectedProduct = _productsList.First();

                    ProductsTitleList = new ObservableCollection<string>();
                    foreach (var title in _productsList.Select(pr => pr.ProductInfo.PRODUCT_TITLE).ToList())
                    {
                        ProductsTitleList.Add(title);
                    }
                    if (!IsSaler)
                        ProductsTitleList.Insert(0, "Всі продукти");
                    SelectedProductTitle = ProductsTitleList.First();
                }

                if (ProductsList.Count > 0)
                    SelectedProduct = ProductsList.First();
                if (_currentWarehouseString != null)
                {
                    if (!IsSaler && !_currentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                    {
                        foreach (var product in ProductsList)
                        {
                            product.IsBooked = _currentWarehouse.Contains(product.ProductInfo);
                            var tempProd =
                                ProductsOnWarehouse.ToList()
                                    .Find(
                                        pr => pr.ReleasedProduct.PRODUCT_INFO_ID == product.ProductInfo.PRODUCT_INFO_ID);
                            if (tempProd != null)
                            {
                                product.Quantity = tempProd.Quantity;
                                product.QuantityNeeded = tempProd.QuantityNeeded;
                            }
                        }
                    }
                    else if (IsSaler)
                    {
                        if (SelectedClient == null) SelectedClient = null;
                        foreach (var product in ProductsList)
                        {
                            product.IsBooked = SelectedClient.Contains(product.ProductInfo);
                            var tempProdOrder = SelectedClient.ContainsOrderProduct(product.ProductInfo);
                            var tempProdWarehouse =
                                ProductsOnWarehouse.ToList()
                                    .Find(pr => pr.ReleasedProduct.PRODUCT_INFO_ID == product.ProductInfo.PRODUCT_INFO_ID);
                            if (tempProdWarehouse != null)
                            {
                                product.Quantity = tempProdWarehouse.Quantity;
                            }
                            if (tempProdOrder != null)
                            {
                                product.QuantityNeeded = tempProdOrder.QuantityInOrder;
                            }
                        }
                    }
                }

                OnPropertyChanged("ProductsList");
            }
        }

        public ProductListElement SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (value != null)
                {
                    _selectedProduct = value;
                    SelectedProductPrice = API.getlastPrice(
                        Session.FactoryEntities.PRODUCT_PRICE
                            .ToList()
                            .FindAll(pr => pr.PRODUCT_INFO_ID == SelectedProduct.ProductInfo.PRODUCT_INFO_ID));
                    ProductPriceList = new ObservableCollection<ProductPriceListElement>();
                    foreach (var price in SelectedProduct.ProductInfo.PRODUCT_PRICE)
                    {
                        ProductPriceList.Add(new ProductPriceListElement(price));
                    }
                    ChangeProductPricePersentage = false;
                    ChangeProductPriceValue = false;
                    ProductPriceValue = (double)SelectedProductPrice.PRICE_VALUE;
                    ProductPricePersentage = (double)SelectedProductPrice.PERSENTAGE_VALUE;
                    OnPropertyChanged("SelectedProduct");
                }
            }
        }

        public ObservableCollection<string> ClientsTitle
        {
            get { return _clientsTitle; }
            set
            {
                _clientsTitle = value;
                if (_clientsTitle.Count > 0)
                    SelectedClientTitle = _clientsTitle.First();
                OnPropertyChanged("ClientsTitle");
            }
        }

        public ObservableCollection<ProductPriceListElement> ProductPriceList
        {
            get { return _productPriceList; }
            set
            {
                _productPriceList = value;
                OnPropertyChanged("ProductPriceList");
            }
        }

        public PRODUCTION_SCHEDULE CurrentProductionSchedule
        {
            get { return _currentProductionSchedule; }
            set
            {
                _currentProductionSchedule = value;
                OnPropertyChanged("CurrentProductionSchedule");
            }
        }

        public string SelectedProductTitle
        {
            get { return _selectedProductTitle; }
            set
            {
                _selectedProductTitle = value;
                OnPropertyChanged("SelectedProductTitle");
            }
        }

        public ObservableCollection<string> ProductsTitleList
        {
            get { return _productsTitleList; }
            set
            {
                _productsTitleList = value;
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
            get { return _schedules; }
            set
            {
                _schedules = value;
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

        public ObservableCollection<string> WarehousesStringsWithoutAll
        {
            get { return _warehousesStringsWithoutAll; }
            set
            {
                _warehousesStringsWithoutAll = value;
                OnPropertyChanged("WarehousesStringsWithoutAll");
            }
        }

        public PRODUCTION_SCHEDULE SelectedProductionSchedule
        {
            get { return _selectedProductionSchedule; }
            set
            {
                _selectedProductionSchedule = value;
                if (_selectedProductionSchedule != null)
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


        public decimal PriceFrom
        {
            get { return _priceFrom; }
            set
            {
                _priceFrom = value;
                OnPropertyChanged("priceFrom");
            }
        }

        public decimal PriceTo
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
            get { return _fromTime; }
            set
            {
                _fromTime = value;
                OnPropertyChanged("FromTime");
            }
        }

        public DateTime ToTime
        {
            get { return _toTime; }
            set
            {
                _toTime = value;
                OnPropertyChanged("ToTime");
            }
        }

        public Dictionary<string, RegionInfo> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged("options");
            }
        }

        public string SelectedOption
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
            get { return _optionsList; }
            set
            {
                _optionsList = value;
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
            get { return _filterByPrice; }
            set
            {
                _filterByPrice = value;
                OnPropertyChanged("FilterByPrice");
            }
        }

        #endregion

        #region Commands

        #endregion

        #endregion

        #region Dialog

        public DialogViewModel Dialog { get; } = ServiceLocator.Current.GetInstance<DialogViewModel>();

        #endregion

        #region ChartViewModel

        #region General Charts

        #endregion

        #region Properties

        public int TabIndex
        {
            get { return _tabIndex; }
            set
            {
                _tabIndex = value;
                OnPropertyChanged("TabIndex");
            }
        }

        public SeriesCollection LineSeriesInstance
        {
            get { return _lineSeriesInstance; }
            set
            {
                _lineSeriesInstance = value;
                OnPropertyChanged("LineSeriesInstance");
            }
        }

        public SeriesCollection PieSeriesInstance
        {
            get { return _pieSeriesInstance; }
            set
            {
                _pieSeriesInstance = value;
                OnPropertyChanged("PieSeriesInstance");
            }
        }

        public SeriesCollection BarSeriesInstance
        {
            get { return _barSeriesInstance; }
            set
            {
                _barSeriesInstance = value;
                OnPropertyChanged("BarSeriesInstance");
            }
        }

        public ObservableCollection<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

        public ObservableCollection<OrderProductTransaction> ProductPackagesList
        {
            get { return _productPackagesList; }
            set
            {
                _productPackagesList = value;
                UpdateSeries();
                OnPropertyChanged("productPackagesList");
            }
        }

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
                if (_productsOnWarehouse != null)
                    foreach (var product in _productsOnWarehouse)
                    {
                        if (product.ReleasedProduct.EXPIRED_DATE <= ExpiredDate)
                        {
                            product.IsExpiring = ExtendedMode;
                            LostProducts += product.Quantity;
                            LostMoney += product.Quantity *
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
            get { return _extendedMode; }
            set
            {
                _extendedMode = value;
                ColumnVisibilityChanged();
                UpdateView();
                Days = _Days;
                if (_productsOnWarehouse != null)
                    foreach (var product in ProductsOnWarehouse)
                    {
                        product.IsExpiring &= _extendedMode;
                    }
                OnPropertyChanged("ExtendedMode");
            }
        }

        public ObservableCollection<EmployeeListItem> EmployeeList
        {
            get { return _EmployeeList; }
            set
            {
                _EmployeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        public EmployeeListItem SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                _SelectedEmployee = value;
                if (_SelectedEmployee != null)
                {
                    EmployeePostSalary = API.getlastSalary(Session.FactoryEntities
                        .SALARY.ToList().
                        FindAll(s => s.POST_ID == _SelectedEmployee.Employee.POST_ID));

                    ChangeEmployeeSalaryPersentage = false;
                    ChangeEmployeeSalaryValue = true;
                    EmployeeSalaryPersentage = (double)_SelectedEmployee.Employee.FULL_SALARY_PERSENTAGE;
                    SelectedEmployee.InitializeLists();
                    if (_SelectedEmployee != null)
                        if (_SelectedEmployee.Equals(NewClient))
                        {
                            if (NewEmployeeEditing)
                            {
                                VisibilityDuringAddingNew = false;
                            }
                            else VisibilityDuringAddingNew = true;
                        }
                        else VisibilityDuringAddingNew = true;
                }
                OnPropertyChanged("SelectedEmployee");
            }
        }

        #endregion

        #region Specialist

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

        public void UpdateLineSeries()
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
                                    seriePrice.Add((double)price.ProductPrice.PRICE_VALUE);
                                else
                                {
                                    seriePrice.Add((double)price.ProductPrice.PERSENTAGE_VALUE);
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

        public void UpdatePieSeries()
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

        public void UpdateBarSeries()
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
                                    serieQuantity.Add((double)price.ProductPrice.PRICE_VALUE);
                                }
                                else
                                {
                                    serieQuantity.Add((double)price.ProductPrice.PERSENTAGE_VALUE);
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
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result =
                    await
                        metroWindow.ShowMessageAsync("Попередження",
                            "Ви дійсно хочете підтвердити зміни?\nСкасувати цю дію буде не можливо.",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                var selected = connection.PRODUCT_PRICE;
                                SelectedProductPrice.CHANGED_DATE = API.getTodayDate();
                                SelectedProductPrice.PRICE_ID =
                                    connection.PRODUCT_PRICE.ToList().Max(price => price.PRICE_ID) + 1;
                                SelectedProductPrice.STAFF_ID = Session.User.STAFF_ID;
                                selected.Add(SelectedProductPrice);
                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                                await metroWindow.ShowMessageAsync("Вітання", "Зміни внесено! Нову ціну додано.");
                                UpdateDb();
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
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
                        var result = await specialistWindow.ShowMessageAsync("Змінити кількість?",
                            "У плані вже наявний продукт : " + existed.ProductInfo.PRODUCT_INFO.PRODUCT_TITLE +
                            "\nКількості : " + existed.Quantity +
                            ".\nБажаєте змінити кількість на " + selectedSchedule_Package.QUANTITY_IN_SCHEDULE + " ?", MessageDialogStyle.AffirmativeAndNegative);
                        if (result == MessageDialogResult.Affirmative)
                        {
                            CurrentWarehouse.addScheduleProduct(selectedSchedule_Package.PRODUCT_INFO,
                                selectedSchedule_Package.QUANTITY_IN_SCHEDULE);
                        }
                        //else quantity is the same
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
            IsSaler = IsSaler;
        }

        #endregion

        #endregion

        #region Director



        public class EmployeeListItem : ViewModelBaseInside
        {
            public STAFF _employee;
            private POST _post;
            private decimal _fullSalary;
            private decimal _MoneySalary;
            private ObservableCollection<WAREHOUSE> _warehousesItem;
            private ObservableCollection<WAREHOUSE> _otherWarehousesItem;
            private ObservableCollection<string> _warehouses;
            private ObservableCollection<string> _otherWarehouses;
            private string _selectedOtherWarehouses;

            public EmployeeListItem(STAFF employee, POST currentPost)
            {
                _employee = employee;
                Post = currentPost;
                FullSalary = employee.FULL_SALARY_PERSENTAGE == 0 ? 100 : employee.FULL_SALARY_PERSENTAGE;
                _employee.ADDRESS1 =
                    Session.FactoryEntities.ADDRESS.ToList().Find(a => a.ADDRESS_ID == _employee.ADDRESS);
                InitializeLists();
            }

            public void InitializeLists()
            {
                _warehousesItem = new ObservableCollection<WAREHOUSE>();
                _otherWarehousesItem = new ObservableCollection<WAREHOUSE>();
                Warehouses = new ObservableCollection<string>();
                OtherWarehouses = new ObservableCollection<string>();
                int i = 0;
                using (var connection = new SWEET_FACTORYEntities())
                {
                    foreach (var warehouse in connection.WAREHOUSE.ToList())
                    {
                        string result = "";
                        var staff = warehouse.STAFF;
                        if (staff != null)
                        {
                            result += staff.STAFF_NAME + " " + staff.STAFF_SURNAME + " " + staff.POST.POST_NAME + " " +
                                          staff.MOBILE_PHONE;
                            if (staff.STAFF_ID == _employee.STAFF_ID) //if it's mine
                            {
                                _warehousesItem.Add(warehouse);
                                Warehouses.Add(API.ConvertAddress(warehouse.ADDRESS1, ++i + ". ",
                                    "Відповідальний :" + result));
                            }
                            else
                            {
                                _otherWarehousesItem.Add(warehouse);
                                OtherWarehouses.Add(API.ConvertAddress(warehouse.ADDRESS1, ++i + ". ", "Відповідальний :" + result));
                            }
                        }
                    }
                    if (OtherWarehouses.Count > 0)
                        SelectedOtherWarehouses = OtherWarehouses.First();
                    isntVisible = true;//update
                }
            }
            public STAFF Employee
            {
                get { return _employee; }
                set
                {
                    _employee = value;
                    OnPropertyChanged("Employee");
                }
            }

            public ObservableCollection<string> Warehouses
            {
                get { return _warehouses; }
                set
                {
                    _warehouses = value;
                    OnPropertyChanged("Warehouses");
                }
            }

            public ObservableCollection<string> OtherWarehouses
            {
                get { return _otherWarehouses; }
                set { _otherWarehouses = value; OnPropertyChanged("OtherWarehouses"); }
            }

            public string SelectedOtherWarehouses
            {
                get { return _selectedOtherWarehouses; }
                set
                {
                    _selectedOtherWarehouses = value;
                    OnPropertyChanged("SelectedOtherWarehouses");
                }
            }

            public ICommand AddWarehouse
            {
                get { return new RelayCommand<object>(AddWarehouseFunc); }
            }

            public void AddWarehouseFunc(object obj)
            {
                int index = OtherWarehouses.ToList().IndexOf(SelectedOtherWarehouses);
                if (index >= 0)
                {
                    var Warehouse = _otherWarehousesItem.ToList().ElementAt(index);
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                var neededWarehouse =
                                    connection.WAREHOUSE.ToList().Find(w => w.WAREHOUSE_ID == Warehouse.WAREHOUSE_ID);
                                neededWarehouse.STAFF_ID = _employee.STAFF_ID;//change owner
                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                            }
                        }
                    }
                    InitializeLists();
                }
            }

            public decimal FullSalary
            {
                get { return _fullSalary; }
                set
                {
                    _fullSalary = value;
                    if (Employee.FULL_SALARY_PERSENTAGE != _fullSalary)
                        Employee.FULL_SALARY_PERSENTAGE = _fullSalary;
                    MoneySalary = API.getlastSalary(Post.SALARY).SALARY_VALUE * Employee.FULL_SALARY_PERSENTAGE / 100;
                    OnPropertyChanged("FullSalary");
                }
            }

            public bool isntVisible
            {
                get { return !(_otherWarehouses.Count == 0); }
                set
                {
                    OnPropertyChanged("isntVisible");
                }
            }

            public POST Post
            {
                get { return _post; }
                set
                {
                    _post = value;
                    Employee.POST_ID = _post.POST_ID;
                    OnPropertyChanged("Post");
                }
            }

            public decimal MoneySalary
            {
                get { return _MoneySalary; }
                set
                {
                    _MoneySalary = value;
                    OnPropertyChanged("MoneySalary");
                }
            }
        }



        private double _employeeSalaryValue;
        private double _employeeSalaryPersentage;
        public bool ChangeEmployeeSalaryValue;
        public bool ChangeEmployeeSalaryPersentage;



        public double EmployeeSalaryValue
        {
            get { return _employeeSalaryValue; }
            set
            {
                _employeeSalaryValue = value;

                if (ChangeEmployeeSalaryPersentage)
                {
                    ChangeEmployeeSalaryValue = false; //to avoid endless cycle
                    if (EmployeePostSalary != null)
                        EmployeeSalaryPersentage = (double)_employeeSalaryValue / (double)EmployeePostSalary.SALARY_VALUE * 100.0;
                }
                OnPropertyChanged("EmployeeSalaryValue");
            }
        }

        public ObservableCollection<POST> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged("Posts");
            }
        }

        public ObservableCollection<string> PostsTitles
        {
            get { return _postsTitles; }
            set { _postsTitles = value; OnPropertyChanged("PostsTitles"); }
        }

        public POST SelectedPost
        {
            get { return _selectedPost; }
            set
            {
                _selectedPost = value;
                if (_selectedPost != null)
                {
                    SelectedPostTitle = _selectedPost.POST_NAME;
                    EmployeePostSalary = API.getlastSalary(_selectedPost.SALARY);
                }

                OnPropertyChanged("SelectedPost");
            }
        }

        public string SelectedPostTitle
        {
            get { return _selectedPostTitle; }
            set
            {
                _selectedPostTitle = value;
                if (Posts != null && !_selectedPost.POST_NAME.Equals(_selectedPostTitle))
                    SelectedPost = Posts.ToList().Find(p => p.POST_NAME.Equals(_selectedPostTitle));
                OnPropertyChanged("SelectedPostTitle");
            }
        }
        public double EmployeeSalaryPersentage
        {
            get { return _employeeSalaryPersentage; }
            set
            {
                _employeeSalaryPersentage = value;
                if (ChangeEmployeeSalaryValue)
                {
                    ChangeEmployeeSalaryPersentage = false;
                    EmployeeSalaryValue = (double)EmployeePostSalary.SALARY_VALUE * _employeeSalaryPersentage / 100.0;
                }
                OnPropertyChanged("EmployeeSalaryPersentage");
            }
        }

        private bool _visibilityDuringAddingNew;

        public bool VisibilityDuringAddingNew
        {
            get { return _visibilityDuringAddingNew; }
            set
            {
                _visibilityDuringAddingNew = value;
                OnPropertyChanged("VisibilityDuringAddingNew");
            }
        }

        public SALARY EmployeePostSalary
        {
            get { return _EmployeePostSalary; }
            set
            {
                _EmployeePostSalary = value;
                ChangeEmployeeSalaryValue = false;
                ChangeEmployeeSalaryPersentage = false;
                EmployeeSalaryValue = (double)_EmployeePostSalary.SALARY_VALUE * _employeeSalaryPersentage / 100;
                OnPropertyChanged("EmployeePostSalary");
            }
        }
        #region Second

        #endregion

        #region Properties

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
                        if (Options.ContainsKey(FromTime.ToLongDateString() + " - " + ToTime.ToLongDateString()))
                        {
                            if (window != null)
                            {
                                window.ShowMessageAsync("Не можливо додати", "Ви уже обрали даний термін. Спробуйте інший");
                            }
                        }
                        else
                        {
                            foreach (var pr in ProductPackagesList)
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
                        if (SelectedOption != null)
                        {
                            var res = Options[SelectedOption];
                            foreach (var pr in ProductPackagesList)
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
            Options = new Dictionary<string, RegionInfo>();
            OptionsList = new ObservableCollection<string>();
            var i = 0;
            foreach (var quantity in ProductPackagesList.First().QuantityInOrders)
            {
                Options.Add(quantity.From.ToLongDateString() + " - " + quantity.To.ToLongDateString(),
                    new RegionInfo(i++, quantity.From, quantity.To));
            }
            foreach (var option in Options.Keys.ToList())
            {
                OptionsList.Add(option);
            }
            if (OptionsList.Count > 0)
                SelectedOption = OptionsList.First();
            UpdateSeries();
        }

        public void UpdateQuantity()
        {
            foreach (var product in ProductPackagesList)
            {
                product.QuantityInOrders.Clear();
                foreach (var option in Options)
                {
                    product.QuantityInOrders.Add(new OrderProductTransaction.QuantityInOrder(option.Value.from,
                        option.Value.to, product));
                }
            }
            UpdateSeries();
        }

        #endregion

        #endregion

        #endregion

        public ICommand AddNewClient
        {
            get { return new RelayCommand<object>(AddClient); }
        }

        public ICommand AddNewEmployee
        {
            get { return new RelayCommand<object>(AddEmployee); }
        }

        public ICommand SaveEmployeeChanges
        {
            get { return new RelayCommand<object>(SaveEmployeeChangesFunc); }
        }

        public ICommand SaveСlientChanges
        {
            get { return new RelayCommand<object>(SaveСlientChangesFunc); }
        }

        public ICommand CancelEmployeeChanges
        {
            get { return new RelayCommand<object>(CancelEmployeeChangesFunc); }
        }

        public ICommand CancelClientChanges
        {
            get { return new RelayCommand<object>(CancelClientChangesFunc); }
        }

        public ICommand SaveSalary
        {
            get { return new RelayCommand<object>(SaveSalaryFunc); }
        }

        public ICommand CancelSalaryChanges
        {
            get { return new RelayCommand<object>(CancelSalaryChangesFunc); }
        }

        public ICommand RefreshAll
        {
            get { return new RelayCommand<object>(RefreshAllFunc); }
        }

        private async void AddClient(object obj)
        {
            if (NewClientEditing)
            {
                //add to database
                var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
                var metroWindow = window as MetroWindow;
                if (metroWindow != null)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                NewClient.Client.CLIENT_ID = connection.CLIENT.ToList().Max(a => a.CLIENT_ID) + 1;
                                NewClient.Client.SALE_ORDER = new List<SALE_ORDER>();
                                NewClient.Client.ADDRESS1.ADDRESS_ID = connection.ADDRESS.ToList().Max(a => a.ADDRESS_ID) + 1;
                                NewClient.Client.ADDRESS = NewClient.Client.ADDRESS1.ADDRESS_ID;

                                var address = new ADDRESS();
                                API.CopyAddress(ref address, NewClient.Client.ADDRESS1);
                                connection.ADDRESS.Add(address);

                                var client = new CLIENT();
                                API.CopyClient(ref client, NewClient.Client);
                                connection.CLIENT.Add(client);
                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                                NewEmployeeEditing = false;
                                NewEmployee = InitializeNewEmployee();

                                NewClient = InitializeNewClient();
                                await metroWindow.ShowMessageAsync("Вітання",
                                        "Зміни внесено! Дані про клієнта збережено");
                                NewClientEditing = false;

                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
            else
            {
                NewClientEditing = true;
                Clients.Insert(0, NewClient);
                Clients = _clients;
                SelectedClient = Clients.First();
            }
        }

        private async void AddEmployee(object obj)
        {
            if (NewEmployeeEditing)
            {
                //add to database
                var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
                var metroWindow = window as MetroWindow;
                if (metroWindow != null)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                NewEmployee.Employee.STAFF_ID = connection.STAFF.ToList().Max(a => a.STAFF_ID) + 1;
                                NewEmployee.Employee.POST_ID = NewEmployee.Post.POST_ID;
                                NewEmployee.Employee.THEME = 11;
                                NewEmployee.Employee.PRODUCTION_SCHEDULE = new List<PRODUCTION_SCHEDULE>();
                                NewEmployee.Employee.SALE_ORDER = new List<SALE_ORDER>();
                                NewEmployee.Employee.ADDRESS1.ADDRESS_ID = connection.ADDRESS.ToList().Max(a => a.ADDRESS_ID) + 1;
                                NewEmployee.Employee.ADDRESS = NewEmployee.Employee.ADDRESS1.ADDRESS_ID;
                                var address = new ADDRESS();
                                API.CopyAddress(ref address, NewEmployee.Employee.ADDRESS1);
                                connection.ADDRESS.Add(address);
                                var staff = new STAFF();
                                API.CopyStaff(ref staff, NewEmployee.Employee);
                                connection.STAFF.Add(staff);
                                connection.SaveChanges();
                                dbContextTransaction.Commit();

                                NewEmployee = InitializeNewEmployee();
                                await
                                    metroWindow.ShowMessageAsync("Вітання",
                                        "Зміни внесено! Дані про працівника оновлено");
                                NewEmployeeEditing = false;

                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
            else
            {
                NewEmployeeEditing = true;
                EmployeeList.Insert(0, NewEmployee);
                SelectedEmployee = NewEmployee;
                EmployeeList = _EmployeeList;
            }
        }

        private async void SaveСlientChangesFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result = await metroWindow.ShowMessageAsync("Попередження", "Старі дані про клієнта будуть втрачені. Бажаєте продовжити?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                var selectedClientAddress =
                                    connection.ADDRESS.Where(a => a.ADDRESS_ID == SelectedClient.Client.ADDRESS)
                                        .FirstOrDefault();
                                var selectedClient =
                                    connection.CLIENT.Where(
                                        s => s.CLIENT_ID == SelectedClient.Client.CLIENT_ID).FirstOrDefault();
                                if (selectedClient != null && selectedClientAddress != null)
                                {
                                    API.CopyAddress(ref selectedClientAddress, SelectedClient.Client.ADDRESS1);
                                    int id = selectedClient.CLIENT_ID;
                                    API.CopyClient(ref selectedClient, SelectedClient.Client);
                                    selectedClient.CLIENT_ID = id;
                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();
                                    await metroWindow.ShowMessageAsync("Вітання", "Зміни внесено! Дані про клієнта оновлено");
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();
                                    await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                                }
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
        }

        private async void SaveEmployeeChangesFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                var result = await metroWindow.ShowMessageAsync("Попередження", "Старі дані про працівника будуть втрачені. Бажаєте продовжити?", MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    using (var connection = new SWEET_FACTORYEntities())
                    {
                        using (var dbContextTransaction = connection.Database.BeginTransaction())
                        {
                            try
                            {
                                var selectedEmployeeAddress =
                                    connection.ADDRESS.Where(a => a.ADDRESS_ID == SelectedEmployee.Employee.ADDRESS)
                                        .FirstOrDefault();
                                var selectedEmployee =
                                    connection.STAFF.Where(
                                        s => s.STAFF_ID == SelectedEmployee.Employee.STAFF_ID).FirstOrDefault();
                                if (selectedEmployee != null && selectedEmployeeAddress != null)
                                {
                                    API.CopyAddress(ref selectedEmployeeAddress, SelectedEmployee.Employee.ADDRESS1);
                                    SelectedEmployee.FullSalary = (decimal)EmployeeSalaryPersentage;
                                    SelectedEmployee.Post = SelectedPost;

                                    selectedEmployee.FULL_SALARY_PERSENTAGE = (decimal)EmployeeSalaryPersentage;
                                    API.CopyStaff(ref selectedEmployee, SelectedEmployee.Employee);
                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();
                                    await metroWindow.ShowMessageAsync("Вітання", "Зміни внесено! Дані про працівника оновлено");
                                }
                                else
                                {
                                    dbContextTransaction.Rollback();
                                    await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                                }
                            }
                            catch (Exception e)
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                        "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                    }
                }
            }
        }

        private void CancelClientChangesFunc(object obj)
        {
            CancelSalaryChangesFunc(obj);
            using (var conection = new SWEET_FACTORYEntities())
            {
                var selectedCl =
                    conection.CLIENT.Where(e => e.CLIENT_ID == SelectedClient.Client.CLIENT_ID).FirstOrDefault();
                if (selectedCl != null)
                {
                    SelectedClient.Client.ADDRESS1.COUNTRY = selectedCl.ADDRESS1.COUNTRY;
                    SelectedClient.Client.ADDRESS1.CITY = selectedCl.ADDRESS1.CITY;
                    SelectedClient.Client.ADDRESS1.REGION = selectedCl.ADDRESS1.REGION;
                    SelectedClient.Client.ADDRESS1.STREET = selectedCl.ADDRESS1.STREET;
                    SelectedClient.Client.ADDRESS1.BUILDING_NUMBER = selectedCl.ADDRESS1.BUILDING_NUMBER;
                    API.CopyClient(ref SelectedClient._Client, selectedCl);
                    SelectedClient = SelectedClient;
                    SelectedClient.Client = SelectedClient.Client;
                    SelectedClient.Client.ADDRESS1 = SelectedClient.Client.ADDRESS1;
                }
            }
        }

        private void CancelEmployeeChangesFunc(object obj)
        {
            CancelSalaryChangesFunc(obj);
            using (var conection = new SWEET_FACTORYEntities())
            {
                var selectedEmp =
                    conection.STAFF.Where(e => e.STAFF_ID == SelectedEmployee.Employee.STAFF_ID).FirstOrDefault();
                if (selectedEmp != null)
                {
                    SelectedEmployee.Employee.ADDRESS1.COUNTRY = selectedEmp.ADDRESS1.COUNTRY;
                    SelectedEmployee.Employee.ADDRESS1.CITY = selectedEmp.ADDRESS1.CITY;
                    SelectedEmployee.Employee.ADDRESS1.REGION = selectedEmp.ADDRESS1.REGION;
                    SelectedEmployee.Employee.ADDRESS1.STREET = selectedEmp.ADDRESS1.STREET;
                    SelectedEmployee.Employee.ADDRESS1.BUILDING_NUMBER = selectedEmp.ADDRESS1.BUILDING_NUMBER;
                    API.CopyStaff(ref SelectedEmployee._employee, selectedEmp);
                    SelectedEmployee.Post = selectedEmp.POST;
                    SelectedEmployee = SelectedEmployee;
                    SelectedEmployee.Employee = SelectedEmployee.Employee;
                    SelectedEmployee.Employee.ADDRESS1 = SelectedEmployee.Employee.ADDRESS1;
                }
            }

        }

        private async void SaveSalaryFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                using (var connection = new SWEET_FACTORYEntities())
                {
                    using (var dbContextTransaction = connection.Database.BeginTransaction())
                    {
                        try
                        {
                            var selectedEmployee =
                                connection.STAFF.Where(
                                    s => s.STAFF_ID == SelectedEmployee.Employee.STAFF_ID).FirstOrDefault();
                            if (selectedEmployee != null)
                            {
                                SelectedEmployee.FullSalary = (decimal)EmployeeSalaryPersentage;
                                selectedEmployee.FULL_SALARY_PERSENTAGE = (decimal)EmployeeSalaryPersentage;
                                connection.SaveChanges();
                                dbContextTransaction.Commit();
                                await metroWindow.ShowMessageAsync("Вітання", "Зміни внесено! Зарплату змінено.");
                            }
                            else
                            {
                                dbContextTransaction.Rollback();
                                await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                            }
                        }
                        catch (Exception)
                        {
                            dbContextTransaction.Rollback();
                            await metroWindow.ShowMessageAsync("Невдача",
                                    "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                        }
                    }
                }

            }
        }

        private void CancelSalaryChangesFunc(object obj)
        {
            if (SelectedEmployee != null)
            {
                ChangeEmployeeSalaryValue = true;
                EmployeeSalaryPersentage = (double)SelectedEmployee.Employee.FULL_SALARY_PERSENTAGE;
            }
        }

        private void RefreshAllFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var metroWindow = window as MetroWindow;
            if (metroWindow != null)
            {
                try
                {
                    Session.FactoryEntities = new SWEET_FACTORYEntities();
                    InitializeViewModel();
                    metroWindow.ShowMessageAsync("Успіх",
                        "З'єднання встановлено. Всі дані оновлені.");
                }
                catch (Exception)
                {
                    metroWindow.ShowMessageAsync("Невдача",
                       "На жаль, не вдалося внести зміни. Перевірте дані і спробуйте знову.");
                }

            }
        }

    }
}
