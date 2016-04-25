using System.Windows;
using System.Windows.Controls;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class ProductView : UserControl
    {
        public ProductView()
        {
            InitializeComponent();
        }
        
        private void PriceChanged(object sender, System.Windows.Input.MouseButtonEventArgs e)
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