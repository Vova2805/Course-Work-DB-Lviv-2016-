using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSale : MetroWindow
    {
        private static bool isopenFlyout;

        public HomeWindowSale()
        {
            var salerViewModel = new CommonViewModel();
            DataContext = salerViewModel;
            InitializeComponent();
            addHotKey();
            RequiredDatePicker.DisplayDateStart = API.getTodayDate();
            ProductsCatalog.DataContext = salerViewModel;
            ProductsCatalog.init();
            ClientsCatalog.DataContext = salerViewModel;
            ClientsCatalog.init();
        }


        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            AdminFlyout.IsOpen = !AdminFlyout.IsOpen;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CustomerFilter(object sender, RoutedEventArgs e)
        {
          
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

        private void addHotKey()
        {
            var firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OpenFilter));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OpenFilter));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, SettingsClick));

            //firstSettings = new RoutedCommand();
            //firstSettings.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            //CommandBindings.Add(new CommandBinding(firstSettings, OnCheckAll));

            //firstSettings = new RoutedCommand();
            //firstSettings.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Alt));
            //CommandBindings.Add(new CommandBinding(firstSettings, OnUncheckAll));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, Help));
            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(firstSettings, NextTab));
        }


        private void NextTab(object sender, RoutedEventArgs e)
        {
            SalerTabControl.SelectedIndex = SalerTabControl.SelectedIndex == 0 ? 1 : 0;
        }

        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            if (SalerTabControl.SelectedIndex == 0)
            {
                ClientsFilter.IsOpen = !ClientsFilter.IsOpen;
            }
            else
            if (SalerTabControl.SelectedIndex == 1)
            {
                NewOrderFilter.IsOpen = !NewOrderFilter.IsOpen;
            }
        }
    }
}