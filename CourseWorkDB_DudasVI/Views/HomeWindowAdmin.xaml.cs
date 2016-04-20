using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views
{
    /// <summary>
    ///     Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindowAdmin : MetroWindow
    {
        private static bool isopenFlyout;

        public HomeWindowAdmin()
        {
            InitializeComponent();
            var directorModel = new DirectorModel();
            var directorViewModel = Mapper.Map<DirectorModel, DirectorViewModel>(directorModel);
            DataContext = directorViewModel;
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            var Home = new Authorization();
            Home.Show();
            Session.User = null;
            Close();
        }
        private void Help(object sender, RoutedEventArgs e)
        {
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            isopenFlyout = !isopenFlyout;
            AdminFlyout.IsOpen = isopenFlyout;
        }

        private void UserFilter(object sender, RoutedEventArgs e)
        {
            UserFilterFlyout.IsOpen = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserFilterFlyout.IsOpen = false;
        }

        private void CustomerFilter(object sender, RoutedEventArgs e)
        {
            CustomerFilterFlyout.IsOpen = true;
        }

        private void CostFilterOpen(object sender, RoutedEventArgs e)
        {
            CostFilterFlyout.IsOpen = true;
        }

        private void OnCheckAll(object sender, RoutedEventArgs e)
        {
            //switch (SpecialistControl.SelectedIndex)
            //{
            //    case 0:
            //        {
            //            var model = DataContext as SpecialistViewModel;
            //            if (model != null)
            //            {
            //                foreach (var product in model.productPackagesList)
            //                {
            //                    product.isChecked = true;
            //                }
            //            }
            //        }
            //        break;
            //    case 1:
            //        {
            //            ProductsCatalog.ClearText();
            //        }
            //        break;
            //    case 4:
            //        {
            //            ProductsScheduleCatalog.ClearText();
            //        }
            //        break;
            //}
        }

        private void OnUncheckAll(object sender, RoutedEventArgs e)
        {
            //switch (SpecialistControl.SelectedIndex)
            //{
            //    case 0:
            //        {
            //            var model = DataContext as SpecialistViewModel;
            //            if (model != null)
            //            {
            //                foreach (var product in model.productPackagesList)
            //                {
            //                    product.isChecked = false;
            //                }
            //            }
            //        }
            //        break;
            //}
        }

        private void RefreshDiagram(object sender, RoutedEventArgs e)
        {

        }

        private void ProductFilterClick(object sender, RoutedEventArgs e)
        {
            FilterProductFlow.IsOpen = !FilterProductFlow.IsOpen;
        }
    }
}