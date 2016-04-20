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
        private string _categoryTitle;
        private bool _isAdded;
        private PRODUCT_INFO _ProductInfo;
        private string _title;
        private readonly SpecialistModel DataContextM;
        private readonly SpecialistViewModel DataContextVM;


        private ProductListElement(PRODUCT_INFO ProductInfo)
        {
            this.ProductInfo = ProductInfo;
            isAdded = false;
            _title = ProductInfo.PRODUCT_TITLE;
            _categoryTitle = ProductInfo.CATEGORY.CATEGORY_TITLE;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SpecialistViewModel dataContextViewModel)
            : this(ProductInfo)
        {
            DataContextVM = dataContextViewModel;
        }

        public ProductListElement(PRODUCT_INFO ProductInfo, SpecialistModel dataContextModel) : this(ProductInfo)
        {
            DataContextM = dataContextModel;
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

        public ICommand AddProductToPlan
        {
            get { return new RelayCommand<object>(AddToSchedule); }
        }

        public ICommand RemoveProductFromPlan
        {
            get { return new RelayCommand<object>(RemoveFromSchedule); }
        }

        public async void AddToSchedule(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            var specialistWindow = window as HomeWindowSpecialist;
            if (specialistWindow != null)
            {
                //TODO додати кількість
                var result = await specialistWindow.ShowMessageAsync("Додати до плану",
                    "Додати?",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    var scheduleProductInfo = new SCHEDULE_PRODUCT_INFO();
                    scheduleProductInfo.PRODUCT_INFO_ID = ProductInfo.PRODUCT_INFO_ID;
                    scheduleProductInfo.QUANTITY_IN_SCHEDULE = 1;
                    scheduleProductInfo.PRODUCT_INFO = ProductInfo;
                    if (DataContextVM != null)
                        DataContextVM.CurrentWarehouse.addScheduleProduct(scheduleProductInfo);
                    else DataContextM.CurrentWarehouse.addScheduleProduct(scheduleProductInfo);
                    isAdded = true;
                }
            }
        }

        public async void RemoveFromSchedule(object obj)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
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
                    if (DataContextVM != null)
                        DataContextVM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    else DataContextM.CurrentWarehouse.removeScheduleProduct(ProductInfo);
                    isAdded = false;
                }
            }
        }
    }
}