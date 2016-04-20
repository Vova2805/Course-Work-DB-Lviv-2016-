using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Product> _Products;
        private Product _selectedProduct;
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

        public ObservableCollection<Product> Products
        {
            get { return _Products; }
            set
            {
                _Products = value;
                OnPropertyChanged("Products");
            }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        #endregion

        #region Commands

        #endregion
    }
}