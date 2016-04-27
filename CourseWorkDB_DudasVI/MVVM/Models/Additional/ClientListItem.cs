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
        public CLIENT _Client;
        private ObservableCollection<DeliveryListItem> _deliveryList;
        private NewOrderItem _newOrder;
        private ObservableCollection<DeliveryListItem> _newOrderDeliveries;
        private decimal _newOrderTotal;
        private ObservableCollection<OrderProductListItem> _packagesProducts;
        private ObservableCollection<NewOrderItem> _saleOrders;
        private NewOrderItem _selectedOrder;
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
                SaleOrders.Add(new NewOrderItem(order,this));
            }
            if (SaleOrders.Count > 0)
                SelectedOrder = SaleOrders.First();
            init();
        }

        public void init()
        {
            var newOrder = new SALE_ORDER();
            newOrder.ORDER_DATE = API.getTodayDate();
            newOrder.REQUIRED_DATE = API.getTodayDate();
            NewOrder = new NewOrderItem(newOrder,this);
            _packagesProducts = new ChartValues<OrderProductListItem>(); //empty
            NewOrderDeliveries = new ObservableCollection<DeliveryListItem>();
            NewDelivery = new DeliveryListItem(InitializeDelivery(), SelectedOrder != null ? SelectedOrder.SaleOrder.TOTAL : 0);
        }

        public DELIVERY InitializeDelivery()
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
            
            var addr = new ADDRESS();
            if(Client.ADDRESS1!=null)
            API.CopyAddress(ref addr, Client.ADDRESS1);
            address.ADDRESS1 = addr;
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
                    if (_selectedOrder.SaleOrder.ORDER_STATUS.Equals("Активне"))
                    {
                        _selectedOrder.State = OrderState.Active;
                    }
                    else if (_selectedOrder.SaleOrder.ORDER_STATUS.Equals("Реалізоване"))
                    {
                        _selectedOrder.State = OrderState.Done;
                    }
                    else if (_selectedOrder.SaleOrder.ORDER_STATUS.Equals("Скасоване"))
                    {
                        _selectedOrder.State = OrderState.Canceled;
                    }
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

        public enum OrderState
        {
            Active,
            Done,
            Canceled
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

        public ICommand ClearChangesInDelivery
        {
            get { return new RelayCommand<object>(ClearChangesInDeliveryFunc); }
        }

        public void ClearChangesInDeliveryFunc(object obj)
        {
            NewDelivery = new DeliveryListItem(InitializeDelivery(), SelectedOrder != null ? SelectedOrder.SaleOrder.TOTAL : 0);
        }
    }
}