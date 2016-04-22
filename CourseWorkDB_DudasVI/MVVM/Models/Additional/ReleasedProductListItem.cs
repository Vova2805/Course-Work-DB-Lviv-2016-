using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ReleasedProductListItem:ViewModelBase
    {
        private RELEASED_PRODUCT _releasedProduct;
        private int _QuantityNeeded;

        public ReleasedProductListItem(RELEASED_PRODUCT releasedProduct)
        {
            _releasedProduct = releasedProduct;
            this.QuantityNeeded = releasedProduct.QUANTITY;
        }

        public int QuantityNeeded
        {
            get { return _QuantityNeeded; }
            set
            {
                _QuantityNeeded = value;
                if (_QuantityNeeded < _releasedProduct.QUANTITY)
                    QuantityNeeded = ReleasedProduct.QUANTITY;
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

    }
}