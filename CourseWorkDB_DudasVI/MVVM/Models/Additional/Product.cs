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
    public class Product : ViewModelBase
    {
       
        private PRODUCT_INFO _ProductInfo;
        private decimal _productPrice;
        private string _productTitle;
        private string _categoryTitle;


        public Product(PRODUCT_INFO productInfo)
        {
            this._ProductInfo = productInfo;
            this._productPrice = API.getlastPrice(ProductInfo.PRODUCT_PRICE).PRICE_VALUE;
            this._productTitle = productInfo.PRODUCT_TITLE;
            this._categoryTitle = productInfo.CATEGORY.CATEGORY_TITLE;
        }


        public PRODUCT_INFO ProductInfo
        {
            get { return _ProductInfo; }
            set
            {
                _ProductInfo = value;
                OnPropertyChanged("ProductInfo");
            }
        }

        public decimal ProductPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value;
                OnPropertyChanged("ProductPrice");
            }
        }

        public string CategoryTitle
        {
            get { return _categoryTitle; }
            set
            {
                _categoryTitle = value; 
                OnPropertyChanged("CategoryTitle");
            }
        }

        public string ProductTitle
        {
            get { return _productTitle; }
            set
            {
                _productTitle = value; 
                OnPropertyChanged("ProductTitle");
            }
        }
    }
}