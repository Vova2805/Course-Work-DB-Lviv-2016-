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
    public class ProductListElement : ViewModelBase
    {
        private readonly SpecialistModel DataContextMSpecialist;
        private readonly SpecialistViewModel DataContextVMSpecialist;
        private readonly bool WorkWithOrders;
        private string _categoryTitle;
        private bool _isAdded;
        private PRODUCT_INFO _ProductInfo;
        private double _productPrice;
        private string _title;

        private DirectorModel DataContextMDirector;
        private SalerModel DataContextMSaler;
        private DirectorViewModel DataContextVMDirector;
        private SalerViewModel DataContextVMSaler;


        private ProductListElement(PRODUCT_INFO productInfo)
        {
            ProductInfo = productInfo;
            isAdded = false;
            _title = _ProductInfo.PRODUCT_TITLE;
            _categoryTitle = _ProductInfo.CATEGORY.CATEGORY_TITLE;
            _productPrice = (double) API.getlastPrice(_ProductInfo.PRODUCT_PRICE).PRICE_VALUE;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, DirectorViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            DataContextVMDirector = dataContextViewModel;
            WorkWithOrders = false;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, DirectorModel dataContextModel) : this(ProductInfo)
        {
            DataContextMDirector = dataContextModel;
            WorkWithOrders = false;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SpecialistViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            DataContextVMSpecialist = dataContextViewModel;
            WorkWithOrders = false;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SpecialistModel dataContextModel) : this(ProductInfo)
        {
            DataContextMSpecialist = dataContextModel;
            WorkWithOrders = false;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SalerViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            DataContextVMSaler = dataContextViewModel;
            WorkWithOrders = true;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SalerModel dataContextModel) : this(ProductInfo)
        {
            DataContextMSaler = dataContextModel;
            WorkWithOrders = true;
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

        public bool isAdded
        {
            get { return _isAdded; }
            set
            {
                _isAdded = value;
                OnPropertyChanged("isAdded");
                var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
                var saleWindow = window as HomeWindowSale;
                if (saleWindow != null && _isAdded)
                {
                    DataContextVMSaler = saleWindow.DataContext as SalerViewModel;
                    if (DataContextVMSaler != null)
                    {
                        DataContextVMSaler.ProductsList.Remove(this);
                        DataContextVMSaler.ProductsList = DataContextVMSaler.ProductsList;
                        DataContextVMSaler.SelectedProduct = DataContextVMSaler.ProductsList.First();
                    }
                }
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
                        if (DataContextVMSpecialist != null)
                            DataContextVMSpecialist.CurrentWarehouse.addScheduleProduct(ProductInfo,1);
                        else DataContextMSpecialist.CurrentWarehouse.addScheduleProduct(ProductInfo,1);
                        isAdded = true;
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
                    DataContextVMSaler = saleWindow.DataContext as SalerViewModel;
                    if (DataContextVMSaler != null)
                        DataContextVMSaler.SelectedClient.addOrderProduct(orderProduct);
                    isAdded = true;
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
                        if (DataContextVMSpecialist != null)
                            DataContextVMSpecialist.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                        else DataContextMSpecialist.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                        isAdded = false;
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
                        isAdded = false;
                    }
                }
            }
        }
    }
}