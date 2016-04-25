using CourseWorkDB_DudasVI.Views.Dialogs;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls.Dialogs;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public class DialogViewModel : GalaSoft.MvvmLight.ViewModelBase
    {
        private readonly IDialogCoordinator _dialogCoordinator;
        private BaseMetroDialog _dialogView = new DialogView();
        public DialogViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            Messenger.Default.Register<string>(this, ProcessMessage);
        }

        public void changeDialog(BaseMetroDialog dialog)
        {
            this._dialogView = dialog;
        }

        private string _dialogMessage;
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

        private string _dialogResult;
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

        private RelayCommand _sendMessageCommand;
        public RelayCommand SendMessageCommand
        {
            get
            {
                return _sendMessageCommand ?? (_sendMessageCommand = new RelayCommand(SendMessage));
            }
        }

        private void SendMessage()
        {
            Messenger.Default.Send(DialogMessage);
        }
        
        private RelayCommand _showDialogCommand;
        public RelayCommand ShowDialogCommand
        {
            get
            {
                return _showDialogCommand ?? (_showDialogCommand = new RelayCommand(ShowDialog));
            }
        }

        private async void ShowDialog()
        {
            await _dialogCoordinator.ShowMetroDialogAsync(this, _dialogView);
        }
        
        private async void ProcessMessage(string messageContents)
        {
            DialogResult = messageContents;
            await _dialogCoordinator.HideMetroDialogAsync(this, _dialogView);
        }

       
    }
}