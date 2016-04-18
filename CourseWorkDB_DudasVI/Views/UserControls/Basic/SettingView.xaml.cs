using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class SettingView : UserControl
    {
        public SettingView()
        {
            InitializeComponent();
            this.DataContext = App.settingsViewModel;
        }
    }
}