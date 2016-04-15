using MahApps.Metro.Controls;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindowAdmin : MetroWindow
    {
        private SWEET_FACTORYEntities sweetFactory  = new SWEET_FACTORYEntities();
        public HomeWindowAdmin()
        {
            InitializeComponent();
            DirectorModel directorModel = new DirectorModel(sweetFactory);
            DirectorViewModel directorViewModel = Mapper.Map<DirectorModel,DirectorViewModel>(directorModel);
            this.DataContext = directorViewModel;
        }

        private static bool isopenFlyout = false;
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

        private void MenuCall(object sender, RoutedEventArgs e)
        {
            MenuFlyout.IsOpen = true;
        }
    }
}
