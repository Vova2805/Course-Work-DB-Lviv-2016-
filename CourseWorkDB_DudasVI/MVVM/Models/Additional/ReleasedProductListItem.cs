using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ReleasedProductListItem:ViewModelBase
    {
        private RELEASED_PRODUCT _releasedProduct;
        private int _Quantity;
        private int _QuantityNeeded;
        private bool _isBooked;
        private bool _isExpiring;

        public ReleasedProductListItem(RELEASED_PRODUCT releasedProduct)
        {
            _releasedProduct = releasedProduct;
            this.Quantity = 0;
        }

        public int QuantityNeeded
        {
            get { return _QuantityNeeded; }
            set
            {
                _QuantityNeeded = value;
                if (_QuantityNeeded == _Quantity) IsBooked = false;
                else
                if (_QuantityNeeded < _Quantity)
                {
                    QuantityNeeded = _Quantity;
                    IsBooked = false;
                }
                else IsBooked = true;
                
                OnPropertyChanged("QuantityNeeded");
            }
        }

        public RELEASED_PRODUCT ReleasedProduct
        {
            get { return _releasedProduct; }
            set
            {
                _releasedProduct = value;
                OnPropertyChanged("ReleasedProduct");
            }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                this.QuantityNeeded = _Quantity;
                OnPropertyChanged("Quantity");
            }
        }

        public bool IsBooked
        {
            get { return _isBooked; }
            set
            {
                _isBooked = value;
                OnPropertyChanged("IsBooked");
            }
        }

        public bool IsExpiring
        {
            get { return _isExpiring; }
            set
            {
                _isExpiring = value;
                OnPropertyChanged("IsExpiring");
            }
        }
    }
}