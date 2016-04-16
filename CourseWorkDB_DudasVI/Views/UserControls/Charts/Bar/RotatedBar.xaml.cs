﻿using System.Windows;
using System.Windows.Media;
using LiveCharts;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Bar
{
    /// <summary>
    ///     Логика взаимодействия для RotatedBar.xaml
    /// </summary>
    public partial class RotatedBar
    {
        public RotatedBar()
        {
            InitializeComponent();

            var config = new SeriesConfiguration<double>().X(value => value);

            SeriesCollection =
                new SeriesCollection(config)
                {
                    new BarSeries
                    {
                        Title = "inverted series",
                        Values = new double[] {10, 15, 18, 20, 15, 13}.AsChartValues(),
                        DataLabels = true
                    },
                    new BarSeries
                    {
                        Title = "inverted series 2",
                        Values = new double[] {4, 8, 19, 19, 16, 12}.AsChartValues(),
                        DataLabels = true
                    },
                    new LineSeries
                    {
                        Title = "inverted line series",
                        Values = new double[] {10, 15, 18, 20, 15, 13}.AsChartValues(),
                        Fill = Brushes.Transparent
                    }
                };

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //this is just to force redraw everytime this view loads
            Chart.Update();
        }
    }
}