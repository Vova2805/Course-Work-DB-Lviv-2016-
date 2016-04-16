﻿using System.Windows;
using LiveCharts.CoreComponents;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    /// <summary>
    ///     Логика взаимодействия для CustomLine.xaml
    /// </summary>
    public partial class CustomLine
    {
        public CustomLine()
        {
            InitializeComponent();
        }

        private void CustomLine_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this is just to see animation everytime you click next
            Chart.Update();
        }

        private void Chart_OnDataClick(ChartPoint point)
        {
            MessageBox.Show("you clicked (" + point.X + "," + point.Y + ")");

            // point.Instance contains the value as object, in case you passed a class, or any other object
        }
    }
}