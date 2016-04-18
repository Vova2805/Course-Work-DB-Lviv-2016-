using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
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

        private void GoodsFilterOpen(object sender, RoutedEventArgs e)
        {
            GoodsFilterFlyout.IsOpen = true;
        }
    }
}