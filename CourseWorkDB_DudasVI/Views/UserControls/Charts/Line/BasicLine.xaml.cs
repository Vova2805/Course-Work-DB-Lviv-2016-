using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls.Charts.Line
{
    public partial class BasicLine
    {
        public BasicLine(ViewModelBase dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}