using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    /// <summary>
    ///     Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindowSale : MetroWindow
    {
        private static bool isopenFlyout;

        public HomeWindowSale()
        {
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