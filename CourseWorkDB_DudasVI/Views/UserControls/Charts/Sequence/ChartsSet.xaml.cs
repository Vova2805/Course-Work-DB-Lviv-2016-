using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CourseWorkDB_DudasVI.Support;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для ChartsSet.xaml
    /// </summary>
    public partial class ChartsSet : UserControl
    {
        public ChartsSet()
        {
            InitializeComponent();
        }

        public void init()
        {
            ExamplesMapper.Initialize(this, DataContext as ViewModelBase);
        }

        public double[] TestPrimaryValues { get; set; }
        public ObservableCollection<string> TestProperty { get; set; }

        #region NavigationButtons

        private void LineNext(object sender, MouseButtonEventArgs e)
        {
            LineControl.Next(ExamplesMapper.LineAndAreaAexamples);
        }

        private void LinePrevious(object sender, MouseButtonEventArgs e)
        {
            LineControl.Previous(ExamplesMapper.LineAndAreaAexamples);
        }

        private void BarPrevious(object sender, MouseButtonEventArgs e)
        {
            BarControl.Previous(ExamplesMapper.BarExamples);
        }

        private void BarNext(object sender, MouseButtonEventArgs e)
        {
            BarControl.Next(ExamplesMapper.BarExamples);
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