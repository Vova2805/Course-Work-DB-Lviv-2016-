using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductListItem : ViewModelBaseInside
    {
        private readonly ClientListItem DataContext;
        private ORDER_PRODUCT _orderProduct;
        private decimal _packageTotal;
        private int _QuantityInOrder = 0;

        public OrderProductListItem(ORDER_PRODUCT orderProduct, int quantity, ClientListItem dataContext)
        {
            DataContext = dataContext;
            _orderProduct = orderProduct;
            QuantityInOrder = quantity;
            PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE * _QuantityInOrder;
        }

        public decimal PackageTotal
        {
            get { return _packageTotal; }
            set
            {
                _packageTotal = value;
                OnPropertyChanged("PackageTotal");
            }
        }

        public ORDER_PRODUCT OrderProduct
        {
            get { return _orderProduct; }
            set
            {
                _orderProduct = value;
                OnPropertyChanged("OrderProduct");
            }
        }

        public int QuantityInOrder
        {
            get { return _QuantityInOrder; }
            set
            {
                _QuantityInOrder = value;
                PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE *
                               _QuantityInOrder;
                _orderProduct.QUANTITY_IN_ORDER = _QuantityInOrder;
                OnPropertyChanged("QuantityInOrder");
                DataContext.PackagesProducts =
                    DataContext.PackagesProducts;
            }
        }

        public ICommand RemoveProductFromOrder
        {
            get { return new RelayCommand<object>(RemoveProductFromOrderFunc); }
        }

        private void RemoveProductFromOrderFunc(object obj)
        {
            _QuantityInOrder = 0; //remove product from order
        }
    }
}