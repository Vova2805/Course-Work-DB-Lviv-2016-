using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CourseWorkDB_DudasVI.General;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Resources;
using CourseWorkDB_DudasVI.Views.UserControls;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public static class ExtensionMethod
    {
        public static bool Contains(this string source, string cont, StringComparison compare)
        {
            return source.IndexOf(cont, compare) >= 0;
        }
    }

    public partial class HomeWindowSpecialist : MetroWindow
    {
        private readonly List<ChartsSet> chartsSets;
        private readonly List<Flyout> flyouts;
        public SpecialistViewModel _specialistViewModel;

        public HomeWindowSpecialist()
        {
            var specialistModel = new SpecialistModel();
            _specialistViewModel = Mapper.Map<SpecialistModel, SpecialistViewModel>(specialistModel);
            DataContext = _specialistViewModel;

            InitializeComponent();
           
            ProductsCatalog.DataContext = _specialistViewModel;
            ProductsCatalog.init();
            chartsSets = new List<ChartsSet> {ChartsSet2};
            foreach (var chart in chartsSets)
            {
                chart.DataContext = _specialistViewModel;
                chart.init();
            }
            flyouts = new List<Flyout> {AdminFlyout};
            addHotKey();

            RequiredDatePicker.DisplayDateStart = API.getTodayDate();
        }

        private void addHotKey()
        {
            var firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.E, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, EditOrdersOpen));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, SettingsClick));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OnCheckAll));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.U, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, OnUncheckAll));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, RefreshDiagram));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, Help));
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            var Home = new Authorization();
            Home.Show();
            Session.User = null;
            Close();
        }

        private void ChartsSet_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #region Func

        private void Help(object sender, RoutedEventArgs e)
        {
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
                case 1:
                {
                    ProductsCatalog.ClearText();
                }
                    break;
                case 4:
                {
                    ProductsScheduleCatalog.ClearText();
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
                    
                }
                    break;
                case 1:
                {
                    SpecialistProductFilter.IsOpen = !SpecialistProductFilter.IsOpen;
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
                model.UpdateSeries();
                //ProductsCatalog.ClearText();
                if (SpecialistControl.SelectedIndex == 1 && InnerControl.SelectedIndex == 1 && model.CurrentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                    InnerTabControlSelectionChanged(sender, e);
            }
            MouseClick(sender, null);
           
        }


        private void MouseClick(object sender, MouseButtonEventArgs e)
        {
            if (flyouts != null)
                foreach (var flyout in flyouts)
                {
                    flyout.IsOpen = false;
                }
        }

        #endregion

        private void InnerTabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InnerControl.SelectedIndex == 1 && SpecialistControl.SelectedIndex == 1 )
            {
                var dataContext = this.DataContext as CommonViewModel;
                if (dataContext != null && dataContext.CurrentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                {
                        dataContext.CurrentWarehouseString = dataContext.WarehousesStringsWithoutAll.First();
                }
            }
        }
    }
}