using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ClientListItem : ViewModelBase
    {
        private CLIENT _Client;
        private ObservableCollection<SALE_ORDER> _saleOrders;
        private SALE_ORDER _selectedOrder;
        private SALE_ORDER _newOrder;
        private ObservableCollection<OrderProductListItem> _packagesProducts;
        private int _totalQuantity = 0;
        private decimal _newOrderTotal = 0;
        private ObservableCollection<DeliveryListItem> _deliveryList;
        private DeliveryListItem selectedDelivery; 

        public class OrderProductListItem:ViewModelBase
        {
            private ORDER_PRODUCT _orderProduct;
            private int _QuantityInOrder;
            private decimal _packageTotal;

            public OrderProductListItem(ORDER_PRODUCT orderProduct)
            {
                _orderProduct = orderProduct;
                QuantityInOrder = _orderProduct.QUANTITY_IN_ORDER;
                this.PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE* _QuantityInOrder;
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
                    PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE * _QuantityInOrder;
                    OnPropertyChanged("QuantityInOrder");
                }
            }
        }

        public class DeliveryListItem:ViewModelBase
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


        public ClientListItem(CLIENT client)
        {
            this.Client = client;
            List<SALE_ORDER> temp = client.SALE_ORDER.ToList();
            _saleOrders = new ObservableCollection<SALE_ORDER>();
            foreach (var order in temp)
            {
               SaleOrders.Add(order); 
            }
            if(SaleOrders.Count>0)
            SelectedOrder = SaleOrders.First();
            NewOrder = new SALE_ORDER();
            NewOrder.ORDER_DATE = API.getTodayDate();
            NewOrder.REQUIRED_DATE = API.getTodayDate();
            _packagesProducts = new ChartValues<OrderProductListItem>();//empty
        }

        public bool addOrderProduct(ORDER_PRODUCT product)
        {
            try
            {
                product.SALE_ORDER_ID = NewOrder.SALE_ORDER_ID;
                PackagesProducts.Add(new OrderProductListItem(product));
                _totalQuantity += product.QUANTITY_IN_ORDER;
                _newOrderTotal += _totalQuantity * API.getlastPrice(product.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool removeOrderProduct(ORDER_PRODUCT product)
        {
            var exestedOrderProduct =
                PackagesProducts.ToList().FindAll(p => p.OrderProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID).FirstOrDefault();
            if (exestedOrderProduct != null)
                if (PackagesProducts.Remove(exestedOrderProduct))
                {
                    _totalQuantity -= exestedOrderProduct.OrderProduct.QUANTITY_IN_ORDER;
                    _newOrderTotal -= _totalQuantity * API.getlastPrice(exestedOrderProduct.OrderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE;
                    return true;
                }
            return false;
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
                    List<DELIVERY> tempDelivery = _selectedOrder.DELIVERY.ToList();
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
            get { return _Client.CLIENT_SURNAME+" "+_Client.CLIENT_NAME +" "+_Client.CLIENT_MIDDLE_NAME+" email:  "+_Client.EMAIL +" Всього замовлень : "+_saleOrders.Count; }
        }

        public SALE_ORDER NewOrder
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
                OnPropertyChanged("PackagesProducts");
            }
        }

        public decimal TotalPrice
        {
            get { return _newOrderTotal; }
            set
            {
                _newOrderTotal = value; 
                OnPropertyChanged("NewOrderTotal");
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
    }
}