using System;
using System.Windows;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
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