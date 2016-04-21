using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutoMapper;
using CourseWorkDB_DudasVI.General;
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
            addHotKey();
            RequiredDatePicker.DisplayDateStart = API.getTodayDate();
        }


        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            AdminFlyout.IsOpen = !AdminFlyout.IsOpen;
        }

        private void UserFilter(object sender, RoutedEventArgs e)
        {
            UserFilterFlyout.IsOpen = !UserFilterFlyout.IsOpen;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void CustomerFilter(object sender, RoutedEventArgs e)
        {
            CustomerFilterFlyout.IsOpen = !CustomerFilterFlyout.IsOpen;
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
            //firstSettings.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            //CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, CustomerFilter));

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
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            //searchTxt.Text = "";
        }
    }
}