﻿using CourseWorkDB_DudasVI.Views.Dialogs;
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
        private DialogResponse _dialogResult;
        private string _dialogTitle;
        private BaseMetroDialog _dialogView = new MessageDialog();

        private bool _ForAll;
        private RelayCommand<string> _hideDialogCommand;

        private RelayCommand _sendMessageCommand;
        private RelayCommand _showDialogCommand;

        public DialogViewModel(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            //Messenger.Default.Register<object>(this, HideDialog);
            _dialogView.DataContext = this;
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

        public string DialogTitle
        {
            get { return _dialogTitle; }
            set
            {
                _dialogTitle = value;
                RaisePropertyChanged();
            }
        }

        public DialogResponse DialogResult
        {
            get { return _dialogResult; }
            set
            {
                _dialogResult = value;
                RaisePropertyChanged();
            }
        }

        public bool ForAll
        {
            get { return _ForAll; }
            set
            {
                _ForAll = value;
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

        public RelayCommand<string> HideDialogCommand
        {
            get { return _hideDialogCommand ?? (_hideDialogCommand = new RelayCommand<string>(HideDialog)); }
        }

        public void Initialize(string dialogTitle, string dialogMessage)
        {
            _dialogTitle = dialogTitle;
            _dialogMessage = dialogMessage;

            _dialogView.DataContext = this;
        }

        public void ChangeDialog(BaseMetroDialog dialog)
        {
            _dialogView = dialog;
        }

        private void SendMessage()
        {
            Messenger.Default.Send(DialogMessage);
        }

        public async void ShowDialog()
        {
            await _dialogCoordinator.ShowMetroDialogAsync(this, _dialogView);
        }

        public async void HideDialog(string Parameter)
        {
            switch (Parameter)
            {
                case "Yes":
                    DialogResult = DialogResponse.Yes;
                    break;
                case "No":
                    DialogResult = DialogResponse.No;
                    break;
                case "Cancel":
                    DialogResult = DialogResponse.Cancel;
                    break;
            }
            await _dialogCoordinator.HideMetroDialogAsync(this, _dialogView);
        }
    }
}