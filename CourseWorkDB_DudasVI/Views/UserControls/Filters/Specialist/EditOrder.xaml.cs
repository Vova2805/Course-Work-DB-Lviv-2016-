using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Views.UserControls
{

    public partial class EditOrder : UserControl
    {
        public static DateTime from = DateTime.Now;
        public static DateTime to = DateTime.Now;
        public EditOrder()
        {
            InitializeComponent();
        }

        private void ToTimeChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                   to = (DateTime)date;
            }
            FromDatePicker.SelectedDate = FromDatePicker.SelectedDate;
        }

        private void FromTimeChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;
            if (date != null)
            {
                from = (DateTime)date;
            }
            ToDatePicker.SelectedDate = ToDatePicker.SelectedDate;
        }
    }
}