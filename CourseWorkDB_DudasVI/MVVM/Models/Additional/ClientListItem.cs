using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using LiveCharts;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private ObservableCollection<NewOrderItem> _saleOrders;
        private NewOrderItem _selectedOrder;
        private bool _isSelectedOrderActive;
        private int _totalQuantity;
        private DeliveryListItem selectedDelivery;

        private DeliveryListItem _newDelivery;



        public ClientListItem(CLIENT client)
        {
            Client = client;
            var temp = client.SALE_ORDER.ToList();
            _saleOrders = new ObservableCollection<NewOrderItem>();
            foreach (var order in temp)
            {
                SaleOrders.Add(new NewOrderItem(order));
            }
            if (SaleOrders.Count > 0)
                SelectedOrder = SaleOrders.First();
            var newOrder = new SALE_ORDER();
            newOrder.ORDER_DATE = API.getTodayDate();
            newOrder.REQUIRED_DATE = API.getTodayDate();
            NewOrder = new NewOrderItem(newOrder);
            _packagesProducts = new ChartValues<OrderProductListItem>(); //empty
            NewOrderDeliveries = new ObservableCollection<DeliveryListItem>();
            NewDelivery = new DeliveryListItem(InitializeDelivery(), SelectedOrder != null?SelectedOrder.SaleOrder.TOTAL:0);
        }

        private DELIVERY InitializeDelivery()
        {
            var temp = new DELIVERY();
            temp.COST_PER_KM = API.getLastCostPerKM();
            temp.DELIVERY_DATE = API.getTodayDate();
            temp.DELIVERY_TOTAL = 0;

            var address = new DELIVERY_ADDRESS();
            address.DISTANCE = 0;
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            if (window != null)
            {
                var context = window.DataContext as CommonViewModel;
                if (context != null)
                {
                    address.ADDRESS = context.CurrentWarehouse.Warehouse.ADDRESS1;
                }
            }
            address.ADDRESS1 = Client.ADDRESS1;

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

        public ObservableCollection<NewOrderItem> SaleOrders
        {
            get { return _saleOrders; }
            set
            {
                _saleOrders = value;
                OnPropertyChanged("SaleOrders");
            }
        }

        public NewOrderItem SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                if (_selectedOrder != null)
                {
                    DeliveryList = new ObservableCollection<DeliveryListItem>();
                    var tempDelivery = _selectedOrder.SaleOrder.DELIVERY.ToList();
                    foreach (var delivery in tempDelivery)
                    {
                        DeliveryList.Add(new DeliveryListItem(delivery, SelectedOrder != null ? SelectedOrder.SaleOrder.TOTAL : 0));
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

            public ICommand AddNewOrder
            {
                get { return new RelayCommand<object>(AddNewOrderFuc); }
            }

            public async void AddNewOrderFuc(object obj)
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
                                //add new delivery order to current
                                var dataContext = metroWindow.DataContext as CommonViewModel;
                                if (dataContext != null)
                                {
                                    ADDRESS addr = new ADDRESS();
                                    
                                    var newDeliveryAddress = dataContext.SelectedClient.NewDelivery.DeliveryAddress;
                                    API.CopyAddress(ref addr, newDeliveryAddress.ADDRESS1);
                                    int id = connection.ADDRESS.ToList().Max(a => a.ADDRESS_ID) + 1;
                                    addr.ADDRESS_ID = id;
                                    connection.ADDRESS.Add(addr);
                                    connection.SaveChanges();

                                    
                                    newDeliveryAddress.DELIVERY_ADDRESS_FROM = newDeliveryAddress.ADDRESS.ADDRESS_ID;
                                    newDeliveryAddress.DELIVERY_ADDRESS_TO = addr.ADDRESS_ID;
                                    newDeliveryAddress.ADDRESS1 = null;
                                    newDeliveryAddress.DELIVERY = null;
                                    newDeliveryAddress.ADDRESS = null;
                                    newDeliveryAddress.DEL_ADDRESS_ID =
                                        connection.DELIVERY_ADDRESS.ToList().Max(a => a.DEL_ADDRESS_ID) + 1;
                                    connection.DELIVERY_ADDRESS.Add(newDeliveryAddress);
                                    connection.SaveChanges();

                                    var newDelivery = dataContext.SelectedClient.NewDelivery.Delivery;

                                    newDelivery.SALE_ORDER_ID = this.SaleOrder.SALE_ORDER_ID;
                                    newDelivery.SALE_ORDER = null;
                                    newDelivery.DEL_ADDRESS_ID = newDeliveryAddress.DEL_ADDRESS_ID;
                                    newDelivery.DELIVERY_ID =
                                        connection.DELIVERY.ToList().Max(a => a.DELIVERY_ID) + 1;
                                    newDelivery.DELIVERY_ADDRESS = null;
                                    newDelivery.DELIVERY_DATE = API.getTodayDate();
                                    newDelivery.RAWSTUFF_ORDER = null;
                                    connection.DELIVERY.Add(newDelivery);

                                    connection.SaveChanges();
                                    dbContextTransaction.Commit();
                                    await metroWindow.ShowMessageAsync("Вітання",
                                    "Зміни внесено! Дані про клієнта збережено");
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
            private decimal _total;
            private decimal _costPerKm;
            private decimal _Kms;
            private decimal _OrderTotal;

            public DeliveryListItem(DELIVERY delivery,decimal orderTotal)
            {
                _delivery = delivery;
                _deliveryAddress = delivery.DELIVERY_ADDRESS;
                Total = 0;
                CostPerKm = delivery.COST_PER_KM;
                Kms = DeliveryAddress.DISTANCE;
                this.OrderTotal = orderTotal;
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
                    _deliveryAddress  = value;
                    OnPropertyChanged("DeliveryAddress");
                }
            }
            
            public decimal Total
            {
                get { return _total; }
                set
                {
                    _total = value;
                    Delivery.DELIVERY_TOTAL = _total;
                    OnPropertyChanged("Total");
                }
            }

            public decimal CostPerKm
            {
                get { return _costPerKm; }
                set
                {
                    _costPerKm = value;
                    Total = 0;
                    Total = ((decimal)(OrderTotal < 50000 ? 0.02 : 0.01) * OrderTotal) + _Kms * _costPerKm;
                    Delivery.COST_PER_KM = _costPerKm;
                    OnPropertyChanged("CostPerKm");
                }
            }

            public decimal Kms
            {
                get { return _Kms; }
                set
                {
                    _Kms = value;
                    Total = 0;
                    Total = ((decimal)(OrderTotal < 50000 ? 0.02 : 0.01)* OrderTotal) + _Kms*_costPerKm;
                    DeliveryAddress.DISTANCE = _Kms;
                    OnPropertyChanged("Kms");
                }
            }

            public decimal OrderTotal
            {
                get { return _OrderTotal; }
                set
                {
                    _OrderTotal = value;
                    Total = 0;
                    Total = ((decimal)(OrderTotal < 50000 ? 0.02 : 0.01) * OrderTotal) + _Kms * _costPerKm;
                    OnPropertyChanged("OrderTotal");
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