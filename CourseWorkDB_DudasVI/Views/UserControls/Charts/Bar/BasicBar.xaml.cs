using System;
using System.Windows;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    public partial class BasicBar
    {
        public BasicBar(ViewModelBaseInside dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        public SeriesCollection Series { get; set; }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            //this is just to see animation everytime you click next
            Chart.Update();
        }

        private void RemovePointsOnClick(object sender, RoutedEventArgs e)
        {
            //Remove any point from any series and chart will update
            foreach (var series in Series)
            {
                if (series.Values.Count > 0) series.Values.RemoveAt(0);
            }
        }

        private void AddPointsOnClick(object sender, RoutedEventArgs e)
        {
            //Add a point to any series, and chart will update
            var r = new Random();

            foreach (var series in Series)
            {
                series.Values.Add((double) r.Next(0, 15));
            }
        }

        private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            //Remove any series
            if (Series.Count > 0) Series.RemoveAt(0);
        }

        private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            //Ad any series to your chart
            var someRandomValues = new ChartValues<double>();

            var r = new Random();
            var count = Series.Count > 0 ? Series[0].Values.Count : 5;

            for (var i = 0; i < count; i++)
            {
                someRandomValues.Add(r.Next(0, 15));
            }

            var someNewSeries = new BarSeries
            {
                Title = "Some Random Series",
                Values = someRandomValues
            };

            Series.Add(someNewSeries);
        }
    }
}