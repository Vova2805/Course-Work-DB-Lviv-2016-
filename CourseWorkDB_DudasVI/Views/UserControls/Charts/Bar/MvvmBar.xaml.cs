﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Bar
{
    /// <summary>
    /// Логика взаимодействия для MvvmBar.xaml
    /// </summary>
    public partial class MvvmBar
    {
        public MvvmBar()
        {
            InitializeComponent();
            Sales = new SalesViewModel();
            DataContext = this;
            YFormatter = x => x + ".00k items";
        }

        public SalesViewModel Sales { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void AddSalesmanOnClick(object sender, RoutedEventArgs e)
        {
            Sales.AddRandomSeries();
        }

        private void RemoveSalesmanOnClick(object sender, RoutedEventArgs e)
        {
            Sales.RemoveLastSalesData();
        }

        private void AddMonthOnClick(object sender, RoutedEventArgs e)
        {
            Sales.AddOneMonth();
        }
        private void RemoveMonthOnClick(object sender, RoutedEventArgs e)
        {
            Sales.RemoveLastMonth();
        }

        private void ClearSeriesOnClick(object sender, RoutedEventArgs e)
        {
            Sales.SalesmenSeries.Clear();
        }

        private void MvvmExample_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this is just to see animation everytime you click next
            Chart.Update();
        }

        private void InsertOnClick(object sender, RoutedEventArgs e)
        {
            Sales.InstertAMiddleVal();
        }
    }

    public class SalesData
    {
        public int ItemsSold { get; set; }
        public decimal ItemsAverageSellPrice { get; set; }
        public double Rentability { get; set; }
    }

    public class AverageSalesData
    {
        public int AverageItemsSold { get; set; }
    }

    public class SalesViewModel
    {
        private readonly string[] _names =
        {
            "Charles", "Susan", "Edit", "Roger", "Peter", "James", "Ana", "Alice", "Maria",
            "Jesus", "Jose", "Miriam", "Aristoteles", "Socrates", "Isaac", "Thomas", "Nicholas"
        };

        private readonly string[] _months =
        {
            "Jan 65", "Feb 65", "Mar 65", "Apr 65", "May 65", "Jun 65", "Jul 65", "Ago 65", "Sep 65", "Oct 65", "Nov 65", "Dec 65",
            "Jan 66", "Feb 66", "Mar 66", "Apr 66", "May 66", "Jun 66", "Jul 66", "Ago 66", "Sep 66", "Oct 66", "Nov 66", "Dec 66",
            "Jan 67", "Feb 67", "Mar 67", "Apr 67", "May 67", "Jun 67", "Jul 67", "Ago 67", "Sep 67", "Oct 67", "Nov 67", "Dec 67"
        };

        public SalesViewModel()
        {
            AvailableMonths = _months;
            SalesmenSeries = new SeriesCollection
            {
                //will use SeriesCollection Setup
                new BarSeries
                {
                    Title = "Charles",
                    Values = new ChartValues<SalesData>
                    {
                        new SalesData {ItemsSold = 15, Rentability = .15, ItemsAverageSellPrice = 5000},
                        new SalesData {ItemsSold = 16, Rentability = .12, ItemsAverageSellPrice = 5200},
                        new SalesData {ItemsSold = 22, Rentability = .11, ItemsAverageSellPrice = 5100},
                        new SalesData {ItemsSold = 25, Rentability = .13, ItemsAverageSellPrice = 5400},
                        new SalesData {ItemsSold = 20, Rentability = .12, ItemsAverageSellPrice = 5100},
                        new SalesData {ItemsSold = 10, Rentability = .11, ItemsAverageSellPrice = 5200},
                        new SalesData {ItemsSold = 12, Rentability = .13, ItemsAverageSellPrice = 5400}
                    }
                },
                //Will use series collection Setup too
                new BarSeries
                {
                    Title = "Frida",
                    Values = new ChartValues<SalesData>
                    {
                        new SalesData {ItemsSold = 25, Rentability = .12, ItemsAverageSellPrice = 5200},
                        new SalesData {ItemsSold = 12, Rentability = .19, ItemsAverageSellPrice = 5100},
                        new SalesData {ItemsSold = 24, Rentability = .12, ItemsAverageSellPrice = 5400},
                        new SalesData {ItemsSold = 15, Rentability = .13, ItemsAverageSellPrice = 5200},
                        new SalesData {ItemsSold = 14, Rentability = .14, ItemsAverageSellPrice = 5100},
                        new SalesData {ItemsSold = 15, Rentability = .13, ItemsAverageSellPrice = 5600},
                        new SalesData {ItemsSold = 14, Rentability = .11, ItemsAverageSellPrice = 4900}
                    }
                },
                //Override Setup for this series to plot another property or even another type
                new BarSeries
                {
                    Title = "Average Series",
                    Values = new ChartValues<AverageSalesData>
                    {
                        new AverageSalesData {AverageItemsSold = 22},
                        new AverageSalesData {AverageItemsSold = 23},
                        new AverageSalesData {AverageItemsSold = 21},
                        new AverageSalesData {AverageItemsSold = 22},
                        new AverageSalesData {AverageItemsSold = 23},
                        new AverageSalesData {AverageItemsSold = 24},
                        new AverageSalesData {AverageItemsSold = 22}
                    }
                }.Setup(new SeriesConfiguration<AverageSalesData>().Y(data => data.AverageItemsSold)) // this is the line that overrides SeriesCollection Setup

            }.Setup(new SeriesConfiguration<SalesData>().Y(data => data.ItemsSold)); // Setup a default configuration for all series in this collection.
        }

        public SeriesCollection SalesmenSeries { get; set; }
        public string[] AvailableMonths { get; set; }

        public void AddRandomSeries()
        {
            var r = new Random();

            var values = new ChartValues<SalesData>();
            var numberOfVals = SalesmenSeries.Count == 0 ? 5 : SalesmenSeries[0].Values.Count;
            for (var i = 0; i < numberOfVals; i++) values.Add(new SalesData
            {
                ItemsSold = r.Next(5, 30),
                Rentability = r.NextDouble() * .2,
                ItemsAverageSellPrice = 5000
            });

            SalesmenSeries.Add(new BarSeries
            {
                Title = _names[r.Next(0, _names.Count() - 1)],
                Values = values
            });
        }

        public void RemoveLastSalesData()
        {
            if (SalesmenSeries.Count <= 1) return;
            SalesmenSeries.RemoveAt(SalesmenSeries.Count - 1);
        }

        public void AddOneMonth()
        {
            if (SalesmenSeries.Count == 0) return;

            var r = new Random();
            if (SalesmenSeries[0].Values.Count >= _months.Count()) return;
            foreach (var salesman in SalesmenSeries.Where(x => x.Title != "Average Series"))
            {
                salesman.Values.Add(new SalesData
                {
                    ItemsSold = r.Next(5, 30),
                    Rentability = r.NextDouble() * .2,
                    ItemsAverageSellPrice = 5000
                });
            }
            var averageSeries = SalesmenSeries.FirstOrDefault(x => x.Title == "Average Series");
            if (averageSeries != null)
            {
                averageSeries.Values.Add(new AverageSalesData { AverageItemsSold = r.Next(20, 25) });
            }
        }

        public void InstertAMiddleVal()
        {
            var r = new Random();
            foreach (var salesman in SalesmenSeries.Where(x => x.Title != "Average Series"))
            {
                if (salesman.Values.Count > 2)
                    salesman.Values.Insert(2, new SalesData
                    {
                        ItemsSold = r.Next(5, 30),
                        Rentability = r.NextDouble() * .2,
                        ItemsAverageSellPrice = 5000m + ((decimal)r.NextDouble() * 1000)
                    });
            }
            var averageSeries = SalesmenSeries.FirstOrDefault(x => x.Title == "Average Series");
            if (averageSeries != null)
            {
                averageSeries.Values.Insert(2, new AverageSalesData { AverageItemsSold = r.Next(20, 25) });
            }
        }

        public void RemoveLastMonth()
        {
            if (SalesmenSeries.Count == 0) return;
            if (SalesmenSeries[0].Values.Count == 1) return;
            foreach (var salesman in SalesmenSeries)
            {
                salesman.Values.RemoveAt(salesman.Values.Count - 1);
            }
        }
    }
}
