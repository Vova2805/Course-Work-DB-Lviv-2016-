using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SalerViewModel : ViewModelBase
    {

        //TabPages

        #region First
        private ObservableCollection<ClientListItem> _Clients;
        private ClientListItem _SelectedClient;
        private ObservableCollection<string> _ClientsTitle;
        private string _SelectedClientTitle;
        #endregion

        #region Second
        private ObservableCollection<ProductListElement> _ProductsList;
        private ProductListElement _selectedProduct;
        #endregion

        #region Third
        
        #endregion

        #region So on

        #endregion

        #region Functions

        #region First

        #endregion

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
                    int name = string.Compare(client1.Client.CLIENT_NAME, client2.Client.CLIENT_NAME,true);
                    int surname = string.Compare(client1.Client.CLIENT_SURNAME,client2.Client.CLIENT_SURNAME, true);
                    int middle_name = string.Compare(client1.Client.CLIENT_MIDDLE_NAME,client2.Client.CLIENT_MIDDLE_NAME);
                    return surname == 0? name == 0?middle_name==0?0:middle_name: name : surname;
                });
                List<string> titles = _Clients.ToList().Select(c => c.GeneralInfo).ToList();
                ClientsTitle = new ObservableCollection<string>();
                foreach (var title in titles)
                {
                    ClientsTitle.Add(title);
                }
                if(SelectedClient!=null)
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
                if(SelectedClient!=null)
                SelectedClientTitle = _SelectedClient.GeneralInfo;
                OnPropertyChanged("SelectedClient");
            }
        }

        public ObservableCollection<ProductListElement> ProductsList
        {
            get { return _ProductsList; }
            set
            {
                _ProductsList = value;
                OnPropertyChanged("ProductsList");
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
                if (SelectedClient==null || !SelectedClient.GeneralInfo.Equals(SelectedClientTitle))
                {
                    SelectedClient = Clients.ToList().FindAll(c => c.GeneralInfo.Equals(SelectedClientTitle)).FirstOrDefault();
                }
                OnPropertyChanged("SelectedClientTitle");
            }
        }
        

        #endregion

        #region Commands

        #endregion
    }
}