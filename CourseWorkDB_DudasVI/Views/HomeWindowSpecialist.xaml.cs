using System.Collections.Generic;
using System.Linq;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using AutoMapper;
using CourseWorkDB_DudasVI.MVVM.Models;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views
{
    public partial class HomeWindowSpecialist : MetroWindow
    {
        public SpecialistViewModel _specialistViewModel;
        SWEET_FACTORYEntities _sweetFactoryEntities = new SWEET_FACTORYEntities();
        private List<DataGridTextColumn> columns = new List<DataGridTextColumn>();
        public HomeWindowSpecialist()
        {
            SpecialistModel specialistModel = new SpecialistModel(_sweetFactoryEntities);
            _specialistViewModel = Mapper.Map<SpecialistModel, SpecialistViewModel>(specialistModel);
            this.DataContext = _specialistViewModel;
            InitializeComponent();
            addColumns();
        }

        private void addColumns()
        {
            foreach (var col in columns)
            {
                OrdersGrid.Columns.Remove(col);
            }
            columns.Clear();
            int i = 0;
            foreach (var quantity in _specialistViewModel.productPackagesList.First().QuantityInOrders)
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Header = quantity.From.ToLongDateString() +" \n "+quantity.To.ToLongDateString();
                column.Binding = new Binding("QuantityInOrders["+i+"].Quantity");
                Style style = new Style(typeof(DataGridCell));
                style.Setters.Add(new System.Windows.Setter
                {
                    Property = Control.HorizontalAlignmentProperty,
                    Value = HorizontalAlignment.Center
                });
                column.CellStyle = style;
                OrdersGrid.Columns.Add(column);
                columns.Add(column);
                i++;
            }
        }
        
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
    }
}
