using System;
using LiveCharts;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Bar
{
    public class StoreViewModel : IObservableChartPoint
    {
        private double _income;

        public double Income
        {
            get { return _income; }
            set
            {
                _income = value;
                if (PointChanged != null) PointChanged.Invoke(this);
            }
        }

        public event Action<object> PointChanged;
    }
}