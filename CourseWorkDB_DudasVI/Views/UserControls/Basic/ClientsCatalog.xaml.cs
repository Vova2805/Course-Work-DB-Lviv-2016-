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
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                view = CollectionViewSource.GetDefaultView(model.Clients);
                view.Filter = FilterClientsRule;
            }
        }

        public void ClearText()
        {
            searchTxt.Text = "";
        }

        private bool FilterClientsRule(object obj)
        {
            var model = DataContext as CommonViewModel;
            if (model != null)
            {
                var clinent = obj as ClientListItem;
                if (clinent.Client.CLIENT_NAME.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    clinent.Client.CLIENT_SURNAME.Contains(model.ChangedText,StringComparison.OrdinalIgnoreCase)
                    ||
                    clinent.Client.CLIENT_MIDDLE_NAME.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    clinent.Client.EMAIL.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    clinent.Client.COMPANY_TITLE.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase)
                    ||
                    clinent.Client.MOBILE_PHONE.Contains(model.ChangedText, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var model = DataContext as CommonViewModel;
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