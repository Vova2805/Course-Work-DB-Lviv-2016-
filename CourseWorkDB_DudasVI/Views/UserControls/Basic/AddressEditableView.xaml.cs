using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для AddressView.xaml
    /// </summary>
    public partial class AddressEditableView : UserControl
    {
        public AddressEditableView()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}