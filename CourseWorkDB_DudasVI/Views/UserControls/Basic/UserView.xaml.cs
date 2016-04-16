using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    /// <summary>
    ///     Логика взаимодействия для UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void NumericUpDown_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            throw new NotImplementedException();
        }
    }
}