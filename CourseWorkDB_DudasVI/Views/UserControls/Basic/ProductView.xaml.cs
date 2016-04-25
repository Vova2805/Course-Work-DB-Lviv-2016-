using System.Windows.Controls;
using System.Windows.Input;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }

        private void PriceChanged(object sender, MouseButtonEventArgs e)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                model.ChangeProductPriceValue = true;
                model.ChangeProductPricePersentage = true;
            }
        }
    }
}