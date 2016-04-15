using System;
using System.Collections.Generic;
using System.Linq;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels.Additional
{
    public class OrderProductTransactionVm:ViewModelBase
    {
        private bool _isChecked;
        private ORDER_PRODUCT _packagesTotal;
        public double _productPrice;
        public List<OrderProductTransaction.QuantityInOrder> _QuantityInOrders;
        public List<ORDER_PRODUCT> _packages;
        private DateTime _FromTime;
        private DateTime _ToTime;
        private Dictionary<string, int> _options;
        private KeyValuePair<string, int> _selectedOption;

#region Properties
        public bool isChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged("isChecked");
            }
        }

        public ORDER_PRODUCT packagesTotal
        {
            get { return _packagesTotal; }
            set
            {
                _packagesTotal = value;
                OnPropertyChanged("packagesTotal");
            }
        }

        public double productPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value;
                OnPropertyChanged("productPrice");
            }
        }

        public List<OrderProductTransaction.QuantityInOrder> QuantityInOrders
        {
            get { return _QuantityInOrders; }
            set
            {
                _QuantityInOrders = value;
                OnPropertyChanged("QuantityInOrders");
            }
        }

        public List<ORDER_PRODUCT> packages
        {
            get { return _packages; }
            set
            {
                _packages = value;
                OnPropertyChanged("packages");
            }
        }
     

    public DateTime FromTime
    {
        get { return _FromTime; }
        set
        {
            _FromTime = value;
            OnPropertyChanged("FromTime");
        }
    }

    public DateTime ToTime
    {
        get { return _ToTime; }
        set
        {
            _ToTime = value;
            OnPropertyChanged("ToTime");
        }
    }

    public Dictionary<string, int> options
    {
        get { return _options; }
        set
        {
            _options = value;
            OnPropertyChanged("ToTime");
        }
    }

    public KeyValuePair<string, int> selectedOption
    {
        get { return _selectedOption; }
        set
        {
            _selectedOption = value;
            OnPropertyChanged("ToTime");
        }
    }
        #endregion

    }
}
