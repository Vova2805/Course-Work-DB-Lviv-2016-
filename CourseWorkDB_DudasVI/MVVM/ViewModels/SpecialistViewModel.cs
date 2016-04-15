using System.Collections.ObjectModel;
using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.MVVM.ViewModels.Additional;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SpecialistViewModel:ViewModelBase
    {
        private ObservableCollection<OrderProductTransactionVm> _productPackagesList;
        private WAREHOUSE _CurrentWarehouse;

        public WAREHOUSE CurrentWarehouse
        {
            get { return _CurrentWarehouse; }
            set
            {
                _CurrentWarehouse = value;
                OnPropertyChanged("CurrentWarehouse");
            }
        }

        public ObservableCollection<OrderProductTransactionVm> productPackagesList
        {
            get { return _productPackagesList; }
            set
            {
                _productPackagesList = value;
                OnPropertyChanged("productPackagesList");
            }
        }
    }
}