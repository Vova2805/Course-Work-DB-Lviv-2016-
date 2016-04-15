using LiveCharts.CoreComponents;
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

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    /// <summary>
    /// Логика взаимодействия для UiLine.xaml
    /// </summary>
    public partial class UiLine
    {
        private readonly Image _sun = new Image
        {
            //Source = new BitmapImage(new Uri("pack://application:,,,/Resources/images/sun.png")),
            Height = 40,
            Width = 40
        };
        private readonly Image _snow = new Image
        {
            //Source = new BitmapImage(new Uri("pack://application:,,,/Resources/images/snow.png")),
            Height = 40,
            Width = 40
        };
        private readonly TextBlock _note = new TextBlock
        {
            Text = "This is a test note, you can place any UIElement in a chart, " +
                   "and use the Plot event to move them to a certain location",
            MaxWidth = 120,
            TextWrapping = TextWrapping.Wrap
        };

        public UiLine()
        {
            InitializeComponent();
            Chart.Canvas.Children.Add(_sun);
            Chart.Canvas.Children.Add(_snow);
            Chart.Canvas.Children.Add(_note);
            TemperatureAxis.LabelFormatter = x => x + "°";
            Panel.SetZIndex(_note, int.MaxValue);
            Panel.SetZIndex(_sun, int.MaxValue);
            Panel.SetZIndex(_snow, int.MaxValue);
        }

        private void Chart_OnPlot(Chart chart)
        {
            var sunPoint = chart.ToPlotArea(new Point(4, 35));
            var snowPoint = chart.ToPlotArea(new Point(8, -3));
            var notePoint = chart.ToPlotArea(new Point(1, 35));

            Canvas.SetLeft(_sun, sunPoint.X - 20);
            Canvas.SetLeft(_snow, snowPoint.X - 20);
            Canvas.SetLeft(_note, notePoint.X);

            Canvas.SetTop(_sun, sunPoint.Y - 20);
            Canvas.SetTop(_snow, snowPoint.Y - 20);
            Canvas.SetTop(_note, notePoint.Y);
        }

        private void UiElementsLine_OnLoaded(object sender, RoutedEventArgs e)
        {
            //this is only to force animation everytime you change view.
            Chart.Update();
        }
    }
}
