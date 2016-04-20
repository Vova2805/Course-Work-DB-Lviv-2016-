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
        #endregion

        #region Second
        private ObservableCollection<ProductListElement> _Products;
        private ProductListElement _selectedProduct;
        #endregion

        #region Third
        private ObservableCollection<string> _ClientsTitle;
        private string _SelectedClientTitle;
        private ObservableCollection<ProductListElement> _ProductsList;
        private ProductListElement _SelectedProduct;
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

        public ObservableCollection<ProductListElement> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged("Products");
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
                OnPropertyChanged("SelectedClientTitle");
            }
        }
        

        #endregion

        #region Commands

        #endregion
    }
}