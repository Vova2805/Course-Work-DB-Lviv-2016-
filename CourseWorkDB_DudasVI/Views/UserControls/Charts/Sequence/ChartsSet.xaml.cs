using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWorkDB_DudasVI.Support;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class ChartsSet : UserControl
    {
        public ChartsSet()
        {
            InitializeComponent();
        }

        public double[] TestPrimaryValues { get; set; }
        public ObservableCollection<string> TestProperty { get; set; }

        public void init()
        {
            ExamplesMapper.Initialize(this, DataContext as ViewModelBaseInside);
        }

        #region NavigationButtons

        private void LineNext(object sender, MouseButtonEventArgs e)
        {
            LineControl.Next(ExamplesMapper.LineAndAreaAexamples);
        }

        private void LinePrevious(object sender, MouseButtonEventArgs e)
        {
            LineControl.Previous(ExamplesMapper.LineAndAreaAexamples);
        }

        private void PiePrevious(object sender, MouseButtonEventArgs e)
        {
            PieControl.Previous(ExamplesMapper.PieExamples);
        }

        private void PieNext(object sender, MouseButtonEventArgs e)
        {
            PieControl.Next(ExamplesMapper.PieExamples);
        }

        #endregion
    }
}