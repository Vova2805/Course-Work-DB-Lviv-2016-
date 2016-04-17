using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSpecialist : MetroWindow
    {
        private readonly SWEET_FACTORYEntities _sweetFactoryEntities = new SWEET_FACTORYEntities();
        private readonly List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        public SpecialistViewModel _specialistViewModel;
        private List<Flyout> flyouts; 

        public HomeWindowSpecialist()
        {
            var specialistModel = new SpecialistModel(_sweetFactoryEntities);
            _specialistViewModel = Mapper.Map<SpecialistModel, SpecialistViewModel>(specialistModel);
            DataContext = _specialistViewModel;

            InitializeComponent();
            ChartsSetView.DataContext = _specialistViewModel;
            ChartsSetView.init();
            addColumns();
            flyouts = new List<Flyout>() {AdminFlyout, SpecialistEditOrders};
            addHotKey();
        }

        private void addHotKey()
        {
            try
            {
                RoutedCommand firstSettings = new RoutedCommand();
                firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.A, ModifierKeys.Alt));
                CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));
            }
            catch (Exception e)
            {
                
            }
        }
        public void addColumns()
        {
            foreach (var col in columns)
            {
                OrdersGrid.Columns.Remove(col);
            }
            columns.Clear();
            var i = 0;
            foreach (var quantity in _specialistViewModel.productPackagesList.First().QuantityInOrders)
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

        private void RefreshDiagram(object sender, RoutedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                model.UpdateSeries();
            }
        }

        private void OnCheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                foreach (var product in model.productPackagesList)
                {
                    product.isChecked = true;
                }
            }
        }

        private void OnUncheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                foreach (var product in model.productPackagesList)
                {
                    product.isChecked = false;
                }
            }
        }

        #region Func

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            AdminFlyout.IsOpen = !AdminFlyout.IsOpen;
        }

        private void EditOrdersOpen(object sender, RoutedEventArgs e)
        {
            SpecialistEditOrders.IsOpen = !SpecialistEditOrders.IsOpen;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var flyout in flyouts)
            {
                flyout.IsOpen = false;
            }
        }


        private void MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        #endregion
        #region HotKey
        public HotKey Key = new HotKey(System.Windows.Input.Key.E,ModifierKeys.Alt);
        #endregion
    }
}