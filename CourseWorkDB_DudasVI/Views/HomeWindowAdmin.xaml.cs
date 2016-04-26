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
            //var firstSettings = new RoutedCommand();
            //firstSettings.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            //CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));
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

        private void EditOrdersOpen(object sender, RoutedEventArgs e)
        {
            DirectorEditOrders.IsOpen = !DirectorEditOrders.IsOpen;
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
    }
}