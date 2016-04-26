using System.Linq;
using System.Windows;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ProductListElement : ViewModelBaseInside
    {
        private readonly bool _workWithOrders;
        private string _categoryTitle;
        private bool _isBooked;
        private bool _isntSaler;
        private bool _isNumbersVisible;
        private PRODUCT_INFO _productInfo;
        private double _productPrice;
        private int _quantity;
        private int _quantityNeeded;
        private string _title;

        private CommonViewModel _dataContextVm;


        private ProductListElement(PRODUCT_INFO productInfo)
        {
            ProductInfo = productInfo;
            IsNumbersVisible = Session.userType !=UserType.Director;
            //isAdded = false;
            _title = _productInfo.PRODUCT_TITLE;
            _categoryTitle = _productInfo.CATEGORY.CATEGORY_TITLE;
            _productPrice = (double) API.getlastPrice(_productInfo.PRODUCT_PRICE).PRICE_VALUE;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, CommonViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            _dataContextVm = dataContextViewModel;
            _workWithOrders = false;
        }

        public double ProductPrice
        {
            get { return _productPrice; }
            set
            {
                _productPrice = value;
                OnPropertyChanged("ProductPrice");
            }
        }

        public PRODUCT_INFO ProductInfo
        {
            get { return _productInfo; }
            set
            {
                _productInfo = value;
                OnPropertyChanged("ProductInfo");
            }
        }

        public bool IsNumbersVisible
        {
            get { return _isNumbersVisible; }
            set
            {
                _isNumbersVisible = value;
                if (_dataContextVm == null) initialiseDataContextVM();
                if (_dataContextVm != null && _dataContextVm.CurrentWarehouse != null)
                    IsBooked = _dataContextVm.CurrentWarehouse.Contains(ProductInfo) && _isNumbersVisible;
                else IsBooked = _isBooked && _isNumbersVisible;
                OnPropertyChanged("IsNumbersVisible");
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

        public bool IsntSaler
        {
            get { return _isntSaler; }
            set
            {
                _isntSaler = value;
                OnPropertyChanged("IsntSaler");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                if (Session.userType == UserType.Specialist)
                {
                    QuantityNeeded = _quantity;
                }
                else if (Session.userType == UserType.Saler)
                {
                    QuantityNeeded = 0;
                }
                OnPropertyChanged("Quantity");
            }
        }

        public int QuantityNeeded
        {
            get { return _quantityNeeded; }
            set
            {
                _quantityNeeded = value;
                if (_quantityNeeded == _quantity)
                {
                    if (_dataContextVm == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        IsBooked = false;
                        _dataContextVm.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        //TODO show warning
                        if (Quantity != 0)
                            IsBooked = true;
                    }
                }
                else if (_quantityNeeded < _quantity)
                {
                    if (_dataContextVm == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        _quantityNeeded = _quantity;
                        _dataContextVm.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                        IsBooked = false;
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        _dataContextVm.SelectedClient.addOrderProduct(ProductInfo, _quantityNeeded);
                        IsBooked = true;
                    }
                }
                else //>=
                {
                    if (_dataContextVm == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        IsBooked = true;
                        _dataContextVm.CurrentWarehouse.addScheduleProduct(ProductInfo, -_quantity + _quantityNeeded);
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        QuantityNeeded = _quantity;
                    }
                }

                OnPropertyChanged("QuantityNeeded");
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

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private void initialiseDataContextVM()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                _dataContextVm = specialistWindow.DataContext as CommonViewModel;
            }
            var salerWindow = window as HomeWindowSale;
            if (salerWindow != null)
            {
                _dataContextVm = salerWindow.DataContext as CommonViewModel;
            }
        }
    }
}