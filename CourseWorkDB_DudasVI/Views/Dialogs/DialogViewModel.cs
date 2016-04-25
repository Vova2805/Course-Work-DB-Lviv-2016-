using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;
using MessageDialog = CourseWorkDB_DudasVI.Views.Dialogs.MessageDialog;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public class DialogViewModel : ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        private string _dialogMessage;

        private string _dialogResult;
        private BaseMetroDialog _dialogView = new MessageDialog();

        private RelayCommand _sendMessageCommand;

        private RelayCommand _showDialogCommand;

        public DialogViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            Messenger.Default.Register<string>(this, HideDialog);
        }

        public string DialogMessage
        {
            get { return _dialogMessage; }
            set
            {
                if (_dialogMessage == value)
                {
                    return;
                }
                _dialogMessage = value;
                RaisePropertyChanged();
            }
        }

        public string DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (_dialogResult == value)
                {
                    return;
                }
                _dialogResult = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand SendMessageCommand
        {
            get { return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(SendMessage)); }
        }

        public RelayCommand ShowDialogCommand
        {
            get { return _showDialogCommand ?? (_showDialogCommand = new RelayCommand(ShowDialog)); }
        }

        public void ChangeDialog(BaseMetroDialog dialog)
        {
            _dialogView = dialog;
        }

        private void SendMessage()
        {
            Messenger.Default.Send(DialogMessage);
        }

        private async void ShowDialog()
        {
            await _dialogCoordinator.ShowMetroDialogAsync(this, _dialogView);
        }

        private async void HideDialog(string messageContents)
        {
            DialogResult = messageContents;
            await _dialogCoordinator.HideMetroDialogAsync(this, _dialogView);
        }
    }
}