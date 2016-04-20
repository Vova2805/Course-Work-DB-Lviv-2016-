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

        #endregion

        #region Commands

        #endregion
    }
}