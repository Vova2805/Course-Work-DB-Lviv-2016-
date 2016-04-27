using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWorkDB_DudasVI.General;
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
        public CommonViewModel _specialistViewModel;

        public HomeWindowSpecialist()
        {
            _specialistViewModel = new CommonViewModel();
            DataContext = _specialistViewModel;

            InitializeComponent();

            //initialization of filters
            ProductsCatalog.DataContext = _specialistViewModel;
            ProductsCatalog.init();
            ProductsScheduleCatalog.DataContext = _specialistViewModel;
            ProductsScheduleCatalog.init();


            chartsSets = new List<ChartsSet> {ChartsSet2};
            foreach (var chart in chartsSets)
            {
                chart.DataContext = _specialistViewModel;
                chart.init();
            }
            flyouts = new List<Flyout> {AdminFlyout};
            addHotKey();
            RequiredDatePicker.DisplayDateStart = API.getTodayDate().Date;
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

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.P, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, ToPlan));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, ToPlan));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.Tab, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(firstSettings, NextTab));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.D, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, CleadSchedule));

            firstSettings = new RoutedCommand();
            firstSettings.InputGestures.Add(new KeyGesture(Key.X, ModifierKeys.Alt));
            CommandBindings.Add(new CommandBinding(firstSettings, SaveSchedule));
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

        private void InnerTabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InnerControl.SelectedIndex == 1 && SpecialistTabControl.SelectedIndex == 1)
            {
                var dataContext = DataContext as CommonViewModel;
                if (dataContext != null && dataContext.CurrentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                {
                    dataContext.CurrentWarehouseString = dataContext.WarehousesStringsWithoutAll.First();
                }
            }
        }

        #region Func

        private void Help(object sender, RoutedEventArgs e)
        {
        }


        private void RefreshDiagram(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                model.UpdateSeries();
            }
        }

        private void OnCheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                if (SpecialistTabControl.SelectedIndex == 3)
                    foreach (var product in model.InOutComeFlow)
                    {
                        product.IsMarked = true;
                    }
            }
        }

        private void OnUncheckAll(object sender, RoutedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                if (SpecialistTabControl.SelectedIndex == 3)
                    foreach (var product in model.InOutComeFlow)
                    {
                        product.IsMarked = false;
                    }
            }
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            AdminFlyout.IsOpen = !AdminFlyout.IsOpen;
        }

        private void OpenFilter(object sender, RoutedEventArgs e)
        {
            switch (SpecialistTabControl.SelectedIndex)
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
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                model.TabIndex = SpecialistTabControl.SelectedIndex;
                model.UpdateSeries();
                //ProductsCatalog.ClearText();
                if (SpecialistTabControl.SelectedIndex == 1 && InnerControl.SelectedIndex == 1 &&
                    model.CurrentWarehouseString.Equals(ResourceClass.ALL_WAREHOUSES))
                    InnerTabControlSelectionChanged(sender, e);
                var visibility = true;
                if (SpecialistTabControl.SelectedIndex == 2)
                {
                    visibility = false;
                }
                foreach (var product in model.ProductsList)
                {
                    product.IsNumbersVisible = visibility;
                }
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

        private int indexBack = 0;

        private void ToPlan(object sender, RoutedEventArgs e)
        {
            if (SpecialistTabControl.SelectedIndex == 1 && InnerControl.SelectedIndex == 1) //come back
                SpecialistTabControl.SelectedIndex = indexBack;
            else
            {
                indexBack = SpecialistTabControl.SelectedIndex;
                SpecialistTabControl.SelectedIndex = 1;
                InnerControl.SelectedIndex = 1;
            }
        }

        private void NextTab(object sender, RoutedEventArgs e)
        {
            int index = SpecialistTabControl.SelectedIndex;
            if (index == 3)
                SpecialistTabControl.SelectedIndex = 0;
            else SpecialistTabControl.SelectedIndex += 1;
        }

        private void CleadSchedule(object sender, RoutedEventArgs e)
        {
            if (SpecialistTabControl.SelectedIndex == 1 && InnerControl.SelectedIndex == 1)
            {
                var model = DataContext as CommonViewModel;
                if (model != null)
                {
                    model.CurrentWarehouse.ClearScheduleFunc(null);
                }
            }
        }

        private void SaveSchedule(object sender, RoutedEventArgs e)
        {
            if (SpecialistTabControl.SelectedIndex == 1 && InnerControl.SelectedIndex == 1)
            {
                var model = DataContext as CommonViewModel;
                if (model != null)
                {
                    model.CurrentWarehouse.SaveScheduleScheduleFunc(null);
                }
            }
        }
    }
}