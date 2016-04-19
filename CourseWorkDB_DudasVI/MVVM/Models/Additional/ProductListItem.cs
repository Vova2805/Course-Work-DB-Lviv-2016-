using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.Models.Additional
{
    public class ProductListElement:ViewModelBase
    {
        public ProductListElement(PRODUCT_INFO ProductInfo)
        {
            this.ProductInfo = ProductInfo;
            isAdded = false;
            _title = ProductInfo.PRODUCT_TITLE;
            _categoryTitle = ProductInfo.CATEGORY.CATEGORY_TITLE;
        }

        public PRODUCT_INFO _ProductInfo;
        public bool _isAdded;
        private string _text;
        private string _title;
        private string _categoryTitle;

        public PRODUCT_INFO ProductInfo {
            get { return _ProductInfo; }
            set
            {
                _ProductInfo = value;
                OnPropertyChanged("ProductInfo");
            }
        }
        public bool isAdded {
            get { return _isAdded; }
            set
            {
                _isAdded = value;
                OnPropertyChanged("isAdded");
            }
        }
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
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
                var result = await specialistWindow.ShowMessageAsync("Попередження",
                            "Ви дійсно хочете підтвердити зміни?\nСкасувати цю дію буде не можливо.",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {

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
                        specialistWindow.ShowMessageAsync("Попередження",
                            "Ви дійсно хочете підтвердити зміни?\nСкасувати цю дію буде не можливо.",
                            MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                   
                }
            }
        }
    }
}