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
    public class ProductListElement : ViewModelBaseInside
    {
        private readonly bool WorkWithOrders;
        private string _categoryTitle;
        private bool _isBooked;
        private bool _isNumbersVisible;
        private PRODUCT_INFO _ProductInfo;
        private double _productPrice;
        private int _Quantity;
        private int _QuantityNeeded;
        private string _title;
        private bool isSaler;
        private GeneralModel dataContextM;

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

        public ProductListElement(PRODUCT_INFO ProductInfo, GeneralModel dataContextModel)
            : this(ProductInfo)
        {
            dataContextM = dataContextModel;
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
                if (dataContextVM != null && dataContextVM.CurrentWarehouse!=null)
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

        public bool IsSaler
        {
            get { return isSaler; }
            set
            {
                isSaler = value;
                OnPropertyChanged("IsSaler");
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

        public int QuantityNeeded
        {
            get { return _QuantityNeeded; }
            set
            {
                _QuantityNeeded = value;
                if (_QuantityNeeded == _Quantity)
                {
                    IsBooked = false;
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (dataContextVM is SpecialistViewModel)
                    {
                        dataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    }
                    else if (dataContextVM is SalerViewModel)
                    {
                        
                    }
                }
                else if (_QuantityNeeded < _Quantity)
                {
                    _QuantityNeeded = _Quantity;
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (dataContextVM is SpecialistViewModel)
                    {
                        dataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    }
                    else if (dataContextVM is SalerViewModel)
                    {

                    }
                    IsBooked = false;
                }
                else
                {
                    IsBooked = true;
                    if (dataContextVM == null) initialiseDataContextVM();
                    if (dataContextVM is SpecialistViewModel)
                    {
                        dataContextVM.CurrentWarehouse.addScheduleProduct(ProductInfo, -_Quantity + _QuantityNeeded);
                    }
                    else if (dataContextVM is SalerViewModel)
                    {

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

        public ICommand AddProduct
        {
            get { return new RelayCommand<object>(AddProductFunc); }
        }

        public ICommand RemoveProduct
        {
            get { return new RelayCommand<object>(RemoveProductFunc); }
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
                dataContextVM = salerWindow.DataContext as SalerViewModel;
            }
        }

        public async void AddProductFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            if (!WorkWithOrders)
            {
                var specialistWindow = window as HomeWindowSpecialist;
                if (specialistWindow != null)
                {
                    //TODO додати кількість
                    var result = await specialistWindow.ShowMessageAsync("Додати до плану",
                        "Додати?",
                        MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Affirmative)
                    {
                        if (dataContextVM != null)
                            dataContextVM.CurrentWarehouse.addScheduleProduct(ProductInfo, 1);
                        //isAdded = true;
                    }
                }
            }
            else
            {
                var saleWindow = window as HomeWindowSale;
                if (saleWindow != null)
                {
                    var orderProduct = new ORDER_PRODUCT();
                    orderProduct.PRODUCT_INFO_ID = ProductInfo.PRODUCT_INFO_ID;
                    orderProduct.PRODUCT_INFO = ProductInfo;
                    orderProduct.QUANTITY_IN_ORDER = 1;
                    dataContextVM = saleWindow.DataContext as SalerViewModel;
                    if (dataContextVM != null)
                        dataContextVM.SelectedClient.addOrderProduct(orderProduct);
                    //isAdded = true;
                }
            }
        }

        public async void RemoveProductFunc(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            if (!WorkWithOrders)
            {
                var specialistWindow = window as HomeWindowSpecialist;
                if (specialistWindow != null)
                {
                    var result =
                        await
                            specialistWindow.ShowMessageAsync("Видалити із плану",
                                "Видалити?",
                                MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Affirmative)
                    {
                        if (dataContextVM != null)
                            dataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                        //isAdded = false;
                    }
                }
            }
            else
            {
                var saleWindow = window as HomeWindowSale;
                if (saleWindow != null)
                {
                    var result =
                        await
                            saleWindow.ShowMessageAsync("Видалити із плану",
                                "Видалити?",
                                MessageDialogStyle.AffirmativeAndNegative);
                    if (result == MessageDialogResult.Affirmative)
                    {
                        //isAdded = false;
                    }
                }
            }
        }
    }
}