using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowAdmin : MetroWindow
    {
        private static bool isopenFlyout;
        private readonly List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        private readonly List<Flyout> flyouts;

        public HomeWindowAdmin()
        {
            InitializeComponent();
            var directorViewModel = new CommonViewModel();
            DataContext = directorViewModel;

            flyouts = new List<Flyout> {AdminFlyout};
            addHotKey();

            EmployeeCatalog.DataContext = directorViewModel;
            EmployeeCatalog.init();
            ClientsCatalog.DataContext = directorViewModel;
            ClientsCatalog.init();
            ProductsCatalog.DataContext = directorViewModel;
            ProductsCatalog.init();

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
            firstSettings.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(firstSettings, NextTab));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            Session.FactoryEntities = new SWEET_FACTORYEntities();
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
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void OnCheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                if(AdminTabControl.SelectedIndex == 3)
                foreach (var flow in model.InOutComeFlow)
                {
                    flow.IsMarked = true;
                }
                if (AdminTabControl.SelectedIndex == 4)
                    foreach (var package in model.ProductPackagesList)
                {
                    package.isChecked = true;
                }
            }
        }

        private void NextTab(object sender, RoutedEventArgs e)
        {
            int tabIndex = AdminTabControl.SelectedIndex;
            if (tabIndex == 5) AdminTabControl.SelectedIndex = 0;
            else AdminTabControl.SelectedIndex += 1;
        }

        private void OnUncheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                if (AdminTabControl.SelectedIndex == 3)
                    foreach (var flow in model.InOutComeFlow)
                {
                    flow.IsMarked = false;
                }
                if (AdminTabControl.SelectedIndex == 4)
                    foreach (var package in model.ProductPackagesList)
                {
                    package.isChecked = false;
                }
            }      
        }

        private void RefreshDiagram(object sender, RoutedEventArgs e)
        {
        }

        public void addColumns()
        {
            foreach (var col in columns)
            {
                OrdersGrid.Columns.Remove(col);
            }
            columns.Clear();
            var i = 0;
            var dataContext = DataContext as CommonViewModel;
            if (dataContext != null)
                foreach (var quantity in dataContext.ProductPackagesList.First().QuantityInOrders)
                {
                    var column = new DataGridTextColumn();
                    column.Header = quantity.From.ToLongDateString() + " \n " + quantity.To.ToLongDateString();
                    column.Binding = new Binding("QuantityInOrders[" + i + "].Quantity");
                    var style = new Style(typeof (DataGridCell));
                    style.Setters.Add(new Setter
                    {
                        Property = HorizontalAlignmentProperty,
                        Value = HorizontalAlignment.Center
                    });
                    column.CellStyle = style;
                    OrdersGrid.Columns.Add(column);
                    columns.Add(column);
                    i++;
                }
        }

        private void MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (flyouts != null)
                foreach (var flyout in flyouts)
                {
                    flyout.IsOpen = false;
                }
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

        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            if (AdminTabControl.SelectedIndex == 1)
            {
                
            }
            if (AdminTabControl.SelectedIndex == 2)
            {
                
            }
            if (AdminTabControl.SelectedIndex == 3)
            {
                InOutComeFilterName.IsOpen = !InOutComeFilterName.IsOpen;
            }
            if (AdminTabControl.SelectedIndex == 4)
            {
                DirectorEditOrders.IsOpen = !DirectorEditOrders.IsOpen;
            }
            if (AdminTabControl.SelectedIndex == 5)
            {
                
            }
        }
    }
}