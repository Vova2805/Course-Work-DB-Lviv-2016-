using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ClientListItem : ViewModelBaseInside
    {
        private CLIENT _Client;
        private ObservableCollection<DeliveryListItem> _deliveryList;
        private NewOrderItem _newOrder;
        private ObservableCollection<DeliveryListItem> _newOrderDeliveries;
        private decimal _newOrderTotal;
        private ObservableCollection<OrderProductListItem> _packagesProducts;
        private ObservableCollection<SALE_ORDER> _saleOrders;
        private SALE_ORDER _selectedOrder;
        private int _totalQuantity;
        private DeliveryListItem selectedDelivery;

        private DeliveryListItem _newDelivery;



        public ClientListItem(CLIENT client)
        {
            Client = client;
            var temp = client.SALE_ORDER.ToList();
            _saleOrders = new ObservableCollection<SALE_ORDER>();
            foreach (var order in temp)
            {
                SaleOrders.Add(order);
            }
            if (SaleOrders.Count > 0)
                SelectedOrder = SaleOrders.First();
            var newOrder = new SALE_ORDER();
            newOrder.ORDER_DATE = API.getTodayDate();
            newOrder.REQUIRED_DATE = API.getTodayDate();
            NewOrder = new NewOrderItem(newOrder);
            _packagesProducts = new ChartValues<OrderProductListItem>(); //empty
            NewOrderDeliveries = new ObservableCollection<DeliveryListItem>();
            NewDelivery = new DeliveryListItem(InitializeDelivery());
        }

        private DELIVERY InitializeDelivery()
        {
            var temp = new DELIVERY();
            temp.COST_PER_KM = API.getLastCostPerKM();
            temp.DELIVERY_DATE = API.getTodayDate();
            temp.DELIVERY_TOTAL = 0;

            var address = new DELIVERY_ADDRESS();
            address.DISTANCE = 0;

            temp.DELIVERY_ADDRESS = address;
            return temp;
        }

        public CLIENT Client
        {
            get { return _Client; }
            set
            {
                _Client = value;
                OnPropertyChanged("Client");
            }
        }

        public ObservableCollection<DeliveryListItem> NewOrderDeliveries
        {
            get { return _newOrderDeliveries; }
            set
            {
                _newOrderDeliveries = value;
                OnPropertyChanged("NewOrderDeliveries");
            }
        }

        public ObservableCollection<SALE_ORDER> SaleOrders
        {
            get { return _saleOrders; }
            set
            {
                _saleOrders = value;
                OnPropertyChanged("SaleOrders");
            }
        }

        public SALE_ORDER SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                if (_selectedOrder != null)
                {
                    DeliveryList = new ObservableCollection<DeliveryListItem>();
                    var tempDelivery = _selectedOrder.DELIVERY.ToList();
                    foreach (var delivery in tempDelivery)
                    {
                        DeliveryList.Add(new DeliveryListItem(delivery));
                    }
                    if (DeliveryList.Count > 0)
                        selectedDelivery = DeliveryList.First();
                }
                OnPropertyChanged("SelectedOrder");
            }
        }

        public string GeneralInfo
        {
            get
            {
                return _Client.CLIENT_SURNAME + " " + _Client.CLIENT_NAME + " " + _Client.CLIENT_MIDDLE_NAME +
                       " email:  " + _Client.EMAIL + " Всього замовлень : " + _saleOrders.Count;
            }
        }

        public NewOrderItem NewOrder
        {
            get { return _newOrder; }
            set
            {
                _newOrder = value;
                OnPropertyChanged("NewOrder");
            }
        }

        public ObservableCollection<OrderProductListItem> PackagesProducts
        {
            get { return _packagesProducts; }
            set
            {
                _packagesProducts = value;
                UpdateTotals();
                OnPropertyChanged("PackagesProducts");
            }
        }

        public decimal TotalPrice
        {
            get { return _newOrderTotal; }
            set
            {
                _newOrderTotal = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public int TotalQuantity
        {
            get { return _totalQuantity; }
            set
            {
                _totalQuantity = value;
                OnPropertyChanged("TotalQuantity");
            }
        }

        public ObservableCollection<DeliveryListItem> DeliveryList
        {
            get { return _deliveryList; }
            set
            {
                _deliveryList = value;
                OnPropertyChanged("DeliveryList");
            }
        }

        public DeliveryListItem SelectedDelivery
        {
            get { return selectedDelivery; }
            set
            {
                selectedDelivery = value;
                OnPropertyChanged("SelectedDelivery");
            }
        }

        public DeliveryListItem NewDelivery
        {
            get { return _newDelivery; }
            set
            {
                _newDelivery = value;
                OnPropertyChanged("NewDelivery");
            }
        }

        public bool addOrderProduct(PRODUCT_INFO product, int quantity)
        {
            try
            {
                var existed =
                    PackagesProducts.ToList().Find(pr => pr.OrderProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
                if (existed != null)
                {
                    existed.QuantityInOrder = quantity;
                }
                else
                {
                    if (quantity > 0)
                    {
                        var orderProduct = InitializeOrder(product);
                        PackagesProducts.Add(new OrderProductListItem(orderProduct, quantity, this));
                    }
                }
                UpdateTotals();
                PackagesProducts = _packagesProducts;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void UpdateTotals()
        {
            TotalQuantity = 0;
            TotalPrice = 0;
            foreach (var product in PackagesProducts)
            {
                TotalQuantity += product.QuantityInOrder;
                TotalPrice += product.PackageTotal;
            }
        }

        private ORDER_PRODUCT InitializeOrder(PRODUCT_INFO product)
        {
            var orderProduct = new ORDER_PRODUCT();
            orderProduct.PRODUCT_INFO_ID = product.PRODUCT_INFO_ID;
            orderProduct.PRODUCT_INFO = product;
            return orderProduct;
        }

        public bool removeOrderProduct(PRODUCT_INFO product)
        {
            var existedOrderProduct = PackagesProducts.ToList()
                .Find(p => p.OrderProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            if (existedOrderProduct != null)
                if (PackagesProducts.Remove(existedOrderProduct))
                {
                    UpdateTotals();
                    return true;
                }
            return false;
        }

        public class NewOrderItem : ViewModelBaseInside
        {
            private SALE_ORDER _saleOrder;

            public NewOrderItem(SALE_ORDER saleOrder)
            {
                _saleOrder = saleOrder;
            }

            public SALE_ORDER SaleOrder
            {
                get { return _saleOrder; }
                set
                {
                    _saleOrder = value;
                    OnPropertyChanged("SaleOrder");
                }
            }
        }

        public class OrderProductListItem : ViewModelBaseInside
        {
            private readonly ClientListItem DataContext;
            private ORDER_PRODUCT _orderProduct;
            private decimal _packageTotal;
            private int _QuantityInOrder = 0;

            public OrderProductListItem(ORDER_PRODUCT orderProduct, int quantity, ClientListItem dataContext)
            {
                DataContext = dataContext;
                _orderProduct = orderProduct;
                QuantityInOrder = quantity;
                PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE*_QuantityInOrder;
            }

            public decimal PackageTotal
            {
                get { return _packageTotal; }
                set
                {
                    _packageTotal = value;
                    OnPropertyChanged("PackageTotal");
                }
            }

            public ORDER_PRODUCT OrderProduct
            {
                get { return _orderProduct; }
                set
                {
                    _orderProduct = value;
                    OnPropertyChanged("OrderProduct");
                }
            }

            public int QuantityInOrder
            {
                get { return _QuantityInOrder; }
                set
                {
                    _QuantityInOrder = value;
                    PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE*
                                   _QuantityInOrder;
                    _orderProduct.QUANTITY_IN_ORDER = _QuantityInOrder;
                    OnPropertyChanged("QuantityInOrder");
                    DataContext.PackagesProducts =
                        DataContext.PackagesProducts;
                }
            }

            public ICommand RemoveProductFromOrder
            {
                get { return new RelayCommand<object>(RemoveProductFromOrderFunc); }
            }

            private void RemoveProductFromOrderFunc(object obj)
            {
                _QuantityInOrder = 0; //remove product from order
            }
        }

        public class DeliveryListItem : ViewModelBaseInside
        {
            private DELIVERY _delivery;
            private DELIVERY_ADDRESS _deliveryAddress;

            public DeliveryListItem(DELIVERY delivery)
            {
                _delivery = delivery;
                _deliveryAddress = delivery.DELIVERY_ADDRESS;
            }

            public DELIVERY Delivery
            {
                get { return _delivery; }
                set
                {
                    _delivery = value;
                    OnPropertyChanged("Delivery");
                }
            }

            public DELIVERY_ADDRESS DeliveryAddress
            {
                get { return _deliveryAddress; }
                set
                {
                    _deliveryAddress = value;
                    OnPropertyChanged("DeliveryAddress");
                }
            }
        }

        #region Funcs

        public bool Contains(PRODUCT_INFO product)
        {
            return PackagesProducts.ToList().Find(pr => pr.OrderProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID) !=
                   null;
        }

        public OrderProductListItem ContainsOrderProduct(PRODUCT_INFO product)
        {
            return PackagesProducts.ToList().Find(pr => pr.OrderProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
        }

        #endregion
    }
}