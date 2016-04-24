using System;
using System.Collections.ObjectModel;
using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class DirectorViewModel : ChartViewModel
    {
        private ObservableCollection<STAFF> _EmployeeList;
        private STAFF _SelectedEmployee;

        public DirectorViewModel() : base()
        {
        }

        public override void UpdateBarSeries()
        {
            throw new NotImplementedException();
        }

        public override void UpdateLineSeries()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePieSeries()
        {
            throw new NotImplementedException();
        }

        public override void UpdateSeries()
        {
            throw new NotImplementedException();
        }

        #region Second

        #endregion

        #region Properties

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

        #endregion

        public override void CurrentWarehouseChanged()
        {
            throw new System.NotImplementedException();
        }
    }
}