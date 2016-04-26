using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class EmployeeCatalog : UserControl
    {
        private ICollectionView view;

        public EmployeeCatalog()
        {
            InitializeComponent();
        }

        public void init()
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                view = CollectionViewSource.GetDefaultView(model.EmployeeList);
                view.Filter = FilterEmployeeRule;
            }
        }

        public void ClearText()
        {
            searchTxt.Text = "";
        }

        private bool FilterEmployeeRule(object obj)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                var employee = obj as STAFF;
                if (employee.STAFF_NAME.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    employee.STAFF_SURNAME.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    employee.EMAIL.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                model.ChangedText = searchTxt.Text;
            }
            if (view != null)
                view.Refresh();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            ClearText();
        }
    }
}