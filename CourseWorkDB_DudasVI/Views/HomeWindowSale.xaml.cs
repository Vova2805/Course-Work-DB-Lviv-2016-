using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSale : MetroWindow
    {
        private static bool isopenFlyout;

        public HomeWindowSale()
        {
            var salerModel = new SalerModel();
            var salerViewModel = Mapper.Map<SalerModel, SalerViewModel>(salerModel);
            DataContext = salerViewModel;
            InitializeComponent();
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
    }
}