using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.Views;
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
        private ObservableCollection<OrderProductTransaction> _productPackagesList;
        private DateTime _ToTime;
        public List<string> _OptionsList;
        public string _selectedOption;

        private void Update()
        {
            options = new Dictionary<string, int>();
            OptionsList = new List<string>();
            int i = 0;
            foreach (var quantity in productPackagesList.First().QuantityInOrders)
            {
                options.Add(quantity.From.ToLongDateString() + " - " + quantity.To.ToLongDateString(), i++);
            }
            OptionsList = options.Keys.ToList();
            if(OptionsList.Count>0)
            selectedOption = OptionsList.First();
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

        public ObservableCollection<OrderProductTransaction> productPackagesList
        {
            get { return _productPackagesList; }
            set
            {
                _productPackagesList = value;
                Update();
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

        public ICommand ChangeCommand
        {
            get { return new RelayCommand<string>(DoChange); }
        }


        public void DoChange(string parameter)
        {
            var window = Application.Current.Windows.OfType<MetroWindow>().FirstOrDefault();

            switch (parameter)
            {
                case "Add":
                    {
                        foreach (var pr in productPackagesList)
                        {
                            pr.QuantityInOrders.Add(new OrderProductTransaction.QuantityInOrder(FromTime, ToTime, pr));
                        }
                        if (window != null)
                        {
                            var specialistWindow = (window as HomeWindowSpecialist);
                            if (specialistWindow != null)
                            {
                                specialistWindow.addColumns();
                                Update();
                            }
                        }
                    }
                    break;
                case "Remove":
                    {
                        int index = options[selectedOption];
                        foreach (var pr in productPackagesList)
                        {
                            pr.QuantityInOrders.RemoveAt(index);
                        }
                        if (window != null)
                        {
                            var specialistWindow = (window as HomeWindowSpecialist);
                            if (specialistWindow != null)
                            {
                                specialistWindow.addColumns();
                                Update();
                            }   
                        }
                    }
                    break;
            }
        }

        #endregion
    }
}