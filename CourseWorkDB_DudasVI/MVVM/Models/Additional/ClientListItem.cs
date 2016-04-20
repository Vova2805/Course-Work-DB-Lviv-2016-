using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private ObservableCollection<ORDER_PRODUCT> _packagesProducts;
        private int _totalQuantity = 0;
        private decimal _newOrderTotal = 0;


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
            _packagesProducts = new ChartValues<ORDER_PRODUCT>();//empty
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

        public ObservableCollection<ORDER_PRODUCT> PackagesProducts
        {
            get { return _packagesProducts; }
            set
            {
                _packagesProducts = value; 
                OnPropertyChanged("PackagesProducts");
            }
        }

        public decimal NewOrderTotal
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
    }
}