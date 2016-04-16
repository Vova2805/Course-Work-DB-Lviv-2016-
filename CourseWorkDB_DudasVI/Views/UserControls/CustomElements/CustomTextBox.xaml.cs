using System.Windows;
using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        public CustomTextBox()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            Box.Clear();
        }
    }
}