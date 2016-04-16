using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;
using CourseWorkDB_DudasVI.Views.UserControls;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Bar;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Line;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Pie;
using MahApps.Metro.Controls;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSpecialist : MetroWindow
    {
        private readonly SWEET_FACTORYEntities _sweetFactoryEntities = new SWEET_FACTORYEntities();
        private readonly List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        public SpecialistViewModel _specialistViewModel;

        public HomeWindowSpecialist()
        {
            var specialistModel = new SpecialistModel(_sweetFactoryEntities);
            _specialistViewModel = Mapper.Map<SpecialistModel, SpecialistViewModel>(specialistModel);
            DataContext = _specialistViewModel;
            InitializeComponent();
            addColumns();
            InitializeChart();
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
        }
        #endregion
        #region Charts
        public static List<UserControl> LineCharts { get; set; }
        public static List<UserControl> BarCharts { get; set; }
        public static List<UserControl> PieCharts { get; set; }

        public void InitializeChart()
        {
            LineCharts = new List<UserControl>
            {
                new BasicLine(),
                new CustomLine()
            };
            BarCharts = new List<UserControl>
            {
                new MultiAxesBarChart(),
                new RotatedBar(),
                new MvvmBar(),
                new PointPropertyChangedBar(),
                new BasicBar()
            };
            PieCharts = new List<UserControl>
            {
                new BasicPie()
            };

            this.LineControl.Content = LineCharts != null && LineCharts.Count > 0 ? LineCharts[0]: null;
            this.BarControl.Content = BarCharts != null && BarCharts.Count > 0 ? BarCharts[0] : null;
            this.PieControl.Content = PieCharts != null && PieCharts.Count > 0 ? PieCharts[0] : null;
        }

        public static void Next(ContentControl control, List<UserControl> list)
        {
            var c = control.Content as UserControl;
            if (c == null) return;
            var i = list.IndexOf(c);
            i++;
            if (i > list.Count - 1) i = 0;
            control.Content = list[i];
        }

        public static void Previous(ContentControl control, List<UserControl> list)
        {
            var c = control.Content as UserControl;
            if (c == null) return;
            var i = list.IndexOf(c);
            i--;
            if (i < 0) i = list.Count - 1;
            control.Content = list[i];
        }
        #endregion
    }
}