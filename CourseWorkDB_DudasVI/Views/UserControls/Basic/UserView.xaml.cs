using System;
using System.Windows;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            //BirthDatePicker.DisplayDateEnd = API.getTodayDate();
        }
        private void SalaryChanged(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                model.ChangeEmployeeSalaryValue = true;
                model.ChangeEmployeeSalaryPersentage = true;
            }
        }
    }
}