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
        private readonly bool WorkWithOrders;
        private string _categoryTitle;
        private bool _isBooked;
        private bool _isntSaler;
        private bool _isNumbersVisible;
        private PRODUCT_INFO _ProductInfo;
        private double _productPrice;
        private int _Quantity;
        private int _QuantityNeeded;
        private string _title;

        private CommonViewModel dataContextVM;


        private ProductListElement(PRODUCT_INFO productInfo)
        {
            ProductInfo = productInfo;
            IsNumbersVisible = true;
            //isAdded = false;
            _title = _ProductInfo.PRODUCT_TITLE;
            _categoryTitle = _ProductInfo.CATEGORY.CATEGORY_TITLE;
            _productPrice = (double) API.getlastPrice(_ProductInfo.PRODUCT_PRICE).PRICE_VALUE;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, CommonViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            dataContextVM = dataContextViewModel;
            WorkWithOrders = false;
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
            get { return _ProductInfo; }
            set
            {
                _ProductInfo = value;
                OnPropertyChanged("ProductInfo");
            }
        }

        public bool IsNumbersVisible
        {
            get { return _isNumbersVisible; }
            set
            {
                _isNumbersVisible = value;
                if (dataContextVM == null) initialiseDataContextVM();
                if (dataContextVM != null && dataContextVM.CurrentWarehouse != null)
                    IsBooked = dataContextVM.CurrentWarehouse.Contains(ProductInfo) && _isNumbersVisible;
                else IsBooked = _isBooked && _isNumbersVisible;
                OnPropertyChanged("IsNumbersVisible");
            }
        }

        //public bool isAdded
        //{
        //    get { return _isAdded; }
        //    set
        //    {
        //        _isAdded = value;
        //        OnPropertyChanged("isAdded");
        //        var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
        //        var saleWindow = window as HomeWindowSale;
        //        if (saleWindow != null && _isAdded)
        //        {
        //            DataContextVMSaler = saleWindow.DataContext as SalerViewModel;
        //            if (DataContextVMSaler != null)
        //            {
        //                DataContextVMSaler.ProductsList.Remove(this);
        //                DataContextVMSaler.ProductsList = DataContextVMSaler.ProductsList;
        //                DataContextVMSaler.SelectedProduct = DataContextVMSaler.ProductsList.First();
        //            }
        //        }
        //    }
        //}
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
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                if (Session.userType == UserType.Specialist)
                {
                    QuantityNeeded = _Quantity;
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
            get { return _QuantityNeeded; }
            set
            {
                _QuantityNeeded = value;
                if (_QuantityNeeded == _Quantity)
                {
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        IsBooked = false;
                        dataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        //TODO show warning
                        if (Quantity != 0)
                            IsBooked = true;
                    }
                }
                else if (_QuantityNeeded < _Quantity)
                {
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        _QuantityNeeded = _Quantity;
                        dataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                        IsBooked = false;
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        dataContextVM.SelectedClient.addOrderProduct(ProductInfo, _QuantityNeeded);
                        IsBooked = true;
                    }
                }
                else //>=
                {
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (Session.userType == UserType.Specialist)
                    {
                        IsBooked = true;
                        dataContextVM.CurrentWarehouse.addScheduleProduct(ProductInfo, -_Quantity + _QuantityNeeded);
                    }
                    else if (Session.userType == UserType.Saler)
                    {
                        QuantityNeeded = _Quantity;
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
                dataContextVM = specialistWindow.DataContext as CommonViewModel;
            }
            var salerWindow = window as HomeWindowSale;
            if (salerWindow != null)
            {
                dataContextVM = salerWindow.DataContext as CommonViewModel;
            }
        }
    }
}