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
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using CourseWorkDB_DudasVI.Support;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ChartsSet.xaml
    /// </summary>
    public partial class ChartsSet : UserControl
    {
        public double[] TestPrimaryValues { get; set; }
        public ObservableCollection<string> TestProperty { get; set; }

        public ChartsSet()
        {
            InitializeComponent();
            ExamplesMapper.Initialize(this);

            TestPrimaryValues = new[] { 3d, 2, 4, 6 };
            TestProperty = new ObservableCollection<string> { "bye!" };

            DataContext = this;
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
        private void BarPrevious(object sender, MouseButtonEventArgs e)
        {
            BarControl.Previous(ExamplesMapper.BarExamples);
        }
        private void BarNext(object sender, MouseButtonEventArgs e)
        {
            BarControl.Next(ExamplesMapper.BarExamples);
        }
        //private void StackedBarPrevious(object sender, MouseButtonEventArgs e)
        //{
        //    StackedBarControl.Previous(ExamplesMapper.StackedBarExamples);
        //}
        //private void StackedBarNext(object sender, MouseButtonEventArgs e)
        //{
        //    StackedBarControl.Next(ExamplesMapper.StackedBarExamples);
        //}
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
