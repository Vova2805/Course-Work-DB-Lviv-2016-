using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    public partial class AnimationLine
    {
        public AnimationLine()
        {
            InitializeComponent();

            Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "",
                    Values = new ChartValues<ViewModel>
                    {
                        new ViewModel {YValue = 0},
                        new ViewModel {YValue = 5},
                        new ViewModel {YValue = 7},
                        new ViewModel {YValue = 10},
                        new ViewModel {YValue = 9}
                    },
                    StrokeDashArray = new DoubleCollection {1}
                }
            }.Setup(new SeriesConfiguration<ViewModel>().Y(vm => vm.YValue));

            DataContext = this;
        }

        public SeriesCollection Series { get; set; }
        
        public void AddSeriesOnClick(object sender, RoutedEventArgs e)
        {
            var vals = new ChartValues<ViewModel>();
            var r = new Random();

            for (var i = 0; i < Series[0].Values.Count; i++)
            {
                vals.Add(new ViewModel {YValue = r.Next(0, 11)});
            }

            Series.Add(new LineSeries
            {
                Values = vals
            });
        }

        public void RemoveSeriesOnClick(object sender, RoutedEventArgs e)
        {
            if (Series.Count == 1) return;
            Series.RemoveAt(0);
        }
    }

    public class ViewModel : IObservableChartPoint
    {
        private double _yValue;
        public double YValue
        {
            get { return _yValue; }
            set
            {
                _yValue = value;
                if (PointChanged != null) PointChanged.Invoke(this);
            }
        }

        public event Action<object> PointChanged;
    }
}