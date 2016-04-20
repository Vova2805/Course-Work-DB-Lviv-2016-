using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using CourseWorkDB_DudasVI.MVVM.Models.Additional;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class ClientsCatalog : UserControl
    {
        private ICollectionView view;

        public ClientsCatalog()
        {
            InitializeComponent();
        }

        public void init()
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                view = CollectionViewSource.GetDefaultView(model.ProductsList);
                view.Filter = FilterProductsRule;
            }
        }

        public void ClearText()
        {
            searchTxt.Text = "";
        }

        private bool FilterProductsRule(object obj)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                var product = obj as ProductListElement;
                if (product.ProductInfo.PRODUCT_TITLE.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    product.ProductInfo.CATEGORY.CATEGORY_TITLE.Contains(model.ChangedText,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var model = DataContext as SpecialistViewModel;
            if (model != null)
            {
                model.ChangedText = searchTxt.Text;
            }
            if (view != null)
                view.Refresh();
        }

        private void ClearButtonClick(object sender, RoutedEventArgs e)
        {
            ClearText();
        }
    }
}