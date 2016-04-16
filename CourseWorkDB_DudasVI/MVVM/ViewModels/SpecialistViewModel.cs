using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels.Additional;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SpecialistViewModel : ViewModelBase
    {
        private WAREHOUSE _CurrentWarehouse;
        private DateTime _FromTime;
        private Dictionary<string, int> _options;
        private ObservableCollection<OrderProductTransactionVm> _productPackagesList;
        private DateTime _ToTime;
        public List<string> _OptionsList;
        public string _selectedOption;


        public SpecialistViewModel() : base()
        {
            ChangeCommand = new Command(DoChange);
        }
        #region Properties

        public WAREHOUSE CurrentWarehouse
        {
            get { return _CurrentWarehouse; }
            set
            {
                _CurrentWarehouse = value;
                OnPropertyChanged("CurrentWarehouse");
            }
        }

        public ObservableCollection<OrderProductTransactionVm> productPackagesList
        {
            get { return _productPackagesList; }
            set
            {
                _productPackagesList = value;
                OnPropertyChanged("productPackagesList");
            }
        }

        public DateTime FromTime
        {
            get { return _FromTime; }
            set
            {
                _FromTime = value;
                OnPropertyChanged("FromTime");
            }
        }

        public DateTime ToTime
        {
            get { return _ToTime; }
            set
            {
                _ToTime = value;
                OnPropertyChanged("ToTime");
            }
        }

        public Dictionary<string, int> options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged("options");
            }
        }

        public string selectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged("selectedOption");
            }
        }

        public List<string> OptionsList
        {
            get { return _OptionsList; }
            set
            {
                _OptionsList = value;
                OnPropertyChanged("OptionsList");
            }
        }

        #endregion

        #region Commands

        public Command ChangeCommand { get; set; }


        private void DoChange()
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();
            if(window!=null)
            window.ShowMessageAsync("Помилка",
                "Не правильний пароль чи логін. Перевірте введені дані і спробуйте знову");
        }

        #endregion
    }
}