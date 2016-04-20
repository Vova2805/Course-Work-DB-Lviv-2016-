using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ClientListItem : ViewModelBase
    {
        private CLIENT _Client;
        private ObservableCollection<SALE_ORDER> _saleOrders;
        private SALE_ORDER _selectedOrder;
        private SALE_ORDER _newOrder;


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
            get { return _Client.CLIENT_SURNAME+" "+_Client.CLIENT_NAME +" "+_Client.CLIENT_MIDDLE_NAME+" email:  "+_Client.EMAIL; }
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
    }
}