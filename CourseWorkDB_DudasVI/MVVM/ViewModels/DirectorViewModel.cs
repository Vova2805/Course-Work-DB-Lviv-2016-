using System.Collections.Generic;
using System.Collections.ObjectModel;
using CourseWorkDB_DudasVI;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class DirectorViewModel:ViewModelBase
    {
        private ObservableCollection<STAFF> _EmployeeList;
        private STAFF _SelectedEmployee;

        public ObservableCollection<STAFF> EmployeeList
        {
            get { return _EmployeeList; }
            set
            {
                _EmployeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        public STAFF SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }
    }
}