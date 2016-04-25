using System;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SalerViewModel : CommonViewModel
    {
        public SalerViewModel()
        {
            AddPermition = true;
        }

        public override void CurrentWarehouseChanged()
        {   
        }

        //TabPages

        #region First

        private ObservableCollection<ClientListItem> _Clients;
        private ClientListItem _SelectedClient;
        private ObservableCollection<string> _ClientsTitle;
        private string _SelectedClientTitle;

        //#endregion

        //#region Second

        private ObservableCollection<ProductListElement> _ProductsList;
        private ProductListElement _selectedProduct;

        //#endregion

        //#region Third

        private ObservableCollection<WAREHOUSE> _WarehousesList;
        private WAREHOUSE _SelectedWarehouse;
        private ObservableCollection<string> _WarehousesListTitles;
        private string _SelectedWarehouseTitle;
        private decimal _Distance;
        private decimal _CostPerKM;

        #endregion

        #region Properties

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
                var titles = _Clients.ToList().Select(c => c.GeneralInfo).ToList();
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

        public ClientListItem SelectedClient
        {
            get { return _SelectedClient; }
            set
            {
                _SelectedClient = value;
                if (SelectedClient != null)
                    SelectedClientTitle = _SelectedClient.GeneralInfo;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ProductListElement SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }


        public ObservableCollection<string> ClientsTitle
        {
            get { return _ClientsTitle; }
            set
            {
                _ClientsTitle = value;
                OnPropertyChanged("ClientsTitle");
            }
        }

        public string SelectedClientTitle
        {
            get { return _SelectedClientTitle; }
            set
            {
                _SelectedClientTitle = value;
                if (SelectedClient == null || !SelectedClient.GeneralInfo.Equals(SelectedClientTitle))
                {
                    SelectedClient =
                        Clients.ToList().FindAll(c => c.GeneralInfo.Equals(SelectedClientTitle)).FirstOrDefault();
                }
                OnPropertyChanged("SelectedClientTitle");
            }
        }

        public WAREHOUSE SelectedWarehouse
        {
            get { return _SelectedWarehouse; }
            set
            {
                _SelectedWarehouse = value;
                OnPropertyChanged("SelectedWarehouse");
            }
        }

        public string SelectedWarehouseTitle
        {
            get { return _SelectedWarehouseTitle; }
            set
            {
                _SelectedWarehouseTitle = value;
                OnPropertyChanged("SelectedWarehouseTitle");
            }
        }

        public ObservableCollection<WAREHOUSE> WarehousesList
        {
            get { return _WarehousesList; }
            set
            {
                _WarehousesList = value;
                OnPropertyChanged("WarehousesList");
            }
        }

        public ObservableCollection<string> WarehousesListTitles
        {
            get { return _WarehousesListTitles; }
            set
            {
                _WarehousesListTitles = value;
                OnPropertyChanged("WarehousesListTitles");
            }
        }

        public decimal CostPerKm
        {
            get { return _CostPerKM; }
            set
            {
                _CostPerKM = value;
                OnPropertyChanged("CostPerKm");
            }
        }

        public decimal Distance
        {
            get { return _Distance; }
            set
            {
                _Distance = value;
                OnPropertyChanged("Distance");
            }
        }

        #endregion

        #region Commands

        #endregion
    }
}