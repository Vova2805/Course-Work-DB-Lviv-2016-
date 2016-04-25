using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ReleasedProductListItem : ViewModelBaseInside
    {
        private readonly WarehouseListItem CurrentWarehouse;
        private bool _isBooked;
        private bool _isExpiring;
        private int _Quantity;
        private int _QuantityNeeded;
        private RELEASED_PRODUCT _releasedProduct;

        public ReleasedProductListItem(RELEASED_PRODUCT releasedProduct, WarehouseListItem currentWarehouseListItem)
        {
            _releasedProduct = releasedProduct;
            CurrentWarehouse = currentWarehouseListItem;
            Quantity = 0;
        }

        public int QuantityNeeded
        {
            get { return _QuantityNeeded; }
            set
            {
                _QuantityNeeded = value;
                if (_QuantityNeeded == _Quantity) IsBooked = false;
                else if (_QuantityNeeded < _Quantity)
                {
                    QuantityNeeded = _Quantity;
                    CurrentWarehouse.removeScheduleProduct(ReleasedProduct.PRODUCT_INFO);
                    IsBooked = false;
                }
                else
                {
                    IsBooked = true;
                    CurrentWarehouse.addScheduleProduct(ReleasedProduct.PRODUCT_INFO, -_Quantity + _QuantityNeeded);
                }
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
                QuantityNeeded = _Quantity;
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