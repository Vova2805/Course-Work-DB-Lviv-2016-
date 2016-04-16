using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using LiveCharts;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    public partial class BasicLine
    {
       
        public BasicLine()
        {
            InitializeComponent();
        }
        

        //private void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        //{
        //    if (Series.Count > 0) Series.RemoveAt(0);
        //}

        //private void AddSeriesOnClick(object sender, RoutedEventArgs e)
        //{
        //    var someRandomValues = new ChartValues<double>();

        //    var r = new Random();
        //    var count = Series.Count > 0 ? Series[0].Values.Count : 5;

        //    for (var i = 0; i < count; i++)
        //    {
        //        someRandomValues.Add(r.Next(0, 15));
        //    }

        //    var someNewSeries = new LineSeries
        //    {
        //        Title = "Some Random Series",
        //        Values = someRandomValues
        //    };

        //    Series.Add(someNewSeries);
        //}
        
    }
}