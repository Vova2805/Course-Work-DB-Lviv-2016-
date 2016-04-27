using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class OrderProductListItem : ViewModelBaseInside
    {
        private readonly ClientListItem DataContext;
        private ORDER_PRODUCT _orderProduct;
        private decimal _packageTotal;
        private int _QuantityInOrder;

        public OrderProductListItem(ORDER_PRODUCT orderProduct, int quantity, ClientListItem dataContext)
        {
            DataContext = dataContext;
            _orderProduct = orderProduct;
            QuantityInOrder = quantity;
            PackageTotal = API.getlastPrice(_orderProduct.PRODUCT_INFO.PRODUCT_PRICE).PRICE_VALUE*_QuantityInOrder;
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
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
                DataContext.PackagesProducts = DataContext.PackagesProducts;
                if (_QuantityInOrder == 0)
                {
                    DataContext.removeOrderProduct(OrderProduct.PRODUCT_INFO);
                }
                SyncQuantity(OrderProduct.PRODUCT_INFO, _QuantityInOrder);
            }
        }

        private async void SyncQuantity(PRODUCT_INFO product, int quantity)
        {
            //sync quantity
            var ProductListItem =
                 Session.dataContext.ProductsList.ToList()
                    .Find(pr => pr.ProductInfo.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            var ReleasedProductItem =
                Session.dataContext.ProductsOnWarehouse.ToList()
                    .Find(pr => pr.ReleasedProduct.PRODUCT_INFO_ID == product.PRODUCT_INFO_ID);
            if (ProductListItem != null && ProductListItem.QuantityNeeded != quantity)
                ProductListItem.QuantityNeeded = quantity;
            //if ()
            //{
            //    var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            //    var metroWindow = window as MetroWindow;
            //    if (metroWindow != null)
            //    {
            //        await metroWindow.ShowMessageAsync("Попередження", "На складі замало місця щоб вмістити стільки продукції!");
            //    }
            //}

        }

        public ICommand RemoveProductFromOrder
        {
            get { return new RelayCommand<object>(RemoveProductFromOrderFunc); }
        }

        private void RemoveProductFromOrderFunc(object obj)
        {
            QuantityInOrder = 0; //remove product from order
        }
    }
}