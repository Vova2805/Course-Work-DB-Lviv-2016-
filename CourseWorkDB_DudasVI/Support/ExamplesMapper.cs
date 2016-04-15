using CourseWorkDB_DudasVI.Views.UserControls;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Bar;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Line;
using CourseWorkDB_DudasVI.Views.UserControls.Charts.Pie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Support
{
    public static class ExamplesMapper
    {
        public static void Initialize(ChartsSet window)
        {
            LineAndAreaAexamples = new List<UserControl>
            {
                new AnimationLine(),
                new BasicLine(),
                new CustomLine(),
                new UiLine()
            };
            BarExamples = new List<UserControl>
            {
                new MultiAxesBarChart(),
                new RotatedBar(),
                new MvvmBar(),
                new PointPropertyChangedBar(),
                new BasicBar()
            };
            PieExamples = new List<UserControl>
            {
                new BasicPie(),
                new MvvmPie()
            };

            window.LineControl.Content = LineAndAreaAexamples != null && LineAndAreaAexamples.Count > 0 ? LineAndAreaAexamples[0] : null;
            window.BarControl.Content = BarExamples != null && BarExamples.Count > 0 ? BarExamples[0] : null;
           // window.StackedBarControl.Content = StackedBarExamples != null && StackedBarExamples.Count > 0 ? StackedBarExamples[0] : null;
            window.PieControl.Content = PieExamples != null && PieExamples.Count > 0 ? PieExamples[0] : null;
        }
        public static List<UserControl> LineAndAreaAexamples { get; set; }
        public static List<UserControl> BarExamples { get; set; }
        public static List<UserControl> StackedBarExamples { get; set; }
        public static List<UserControl> PieExamples { get; set; }
        public static List<UserControl> ScatterExamples { get; set; }
        public static List<UserControl> MoreExamples { get; set; }

        public static void Next(this ContentControl control, List<UserControl> list)
        {
            var c = control.Content as UserControl;
            if (c == null) return;
            var i = list.IndexOf(c);
            i++;
            if (i > list.Count - 1) i = 0;
            control.Content = list[i];
        }

        public static void Previous(this ContentControl control, List<UserControl> list)
        {
            var c = control.Content as UserControl;
            if (c == null) return;
            var i = list.IndexOf(c);
            i--;
            if (i < 0) i = list.Count - 1;
            control.Content = list[i];
        }
    }
}
