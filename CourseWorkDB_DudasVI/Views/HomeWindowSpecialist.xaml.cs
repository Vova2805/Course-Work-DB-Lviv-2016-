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
using CourseWorkDB_DudasVI.Views.UserControls;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSpecialist : MetroWindow
    {
        private readonly SWEET_FACTORYEntities _sweetFactoryEntities = new SWEET_FACTORYEntities();
        private readonly List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        public SpecialistViewModel _specialistViewModel;
        private List<Flyout> flyouts;
        private List<UserControls.ChartsSet> chartsSets;

        public HomeWindowSpecialist()
        {
            var specialistModel = new SpecialistModel(_sweetFactoryEntities);
            _specialistViewModel = Mapper.Map<SpecialistModel, SpecialistViewModel>(specialistModel);
            DataContext = _specialistViewModel;

            InitializeComponent();
            chartsSets = new List<ChartsSet>() { ChartsSetView, ChartsSet2 };
            foreach (var chart in chartsSets)
            {
                chart.DataContext = _specialistViewModel;
                chart.init();
            }
            
            addColumns();
            flyouts = new List<Flyout>() {AdminFlyout, SpecialistEditOrders};
            addHotKey();
        }

        private void addHotKey()
        {
            RoutedCommand firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.E, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.F, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.S, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, SettingsClick));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.C, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OnCheckAll));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.U, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OnUncheckAll));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.R, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, RefreshDiagram));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(System.Windows.Input.Key.F1, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, Help));
        }


        #region Func

        private void Help(object sender, RoutedEventArgs e)
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
            foreach (var quantity in _specialistViewModel.productPackagesList.First().QuantityInOrders)
            {
                var column = new DataGridTextColumn();
                column.Header = quantity.From.ToLongDateString() + " \n " + quantity.To.ToLongDateString();
                column.Binding = new Binding("QuantityInOrders[" + i + "].Quantity");
                var style = new Style(typeof(DataGridCell));
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
           
            switch (SpecialistControl.SelectedIndex)
            {
                case 0:
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
                    break;
            }
        }

        private void OnUncheckAll(object sender, RoutedEventArgs e)
        {
            switch (SpecialistControl.SelectedIndex)
            {
                case 0:
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
                    break;
            }
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
         AdminFlyout.IsOpen = !AdminFlyout.IsOpen;
        }

        private void EditOrdersOpen(object sender, RoutedEventArgs e)
        {
            switch (SpecialistControl.SelectedIndex)
            {
                case 0:
                {
                        SpecialistEditOrders.IsOpen = !SpecialistEditOrders.IsOpen;
                }
                    break;
            }
            
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                model.TabIndex = SpecialistControl.SelectedIndex;
            }
            if (flyouts!=null )
            foreach (var flyout in flyouts)
            {
                flyout.IsOpen = false;
            }
        }   


        private void MouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        #endregion
    }
}