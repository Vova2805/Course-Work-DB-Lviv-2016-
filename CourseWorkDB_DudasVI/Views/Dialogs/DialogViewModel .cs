using GalaSoft.MvvmLight;

using GalaSoft.MvvmLight.CommandWpf;

using GalaSoft.MvvmLight.Messaging;


namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public class DialogViewModel : ViewModelBase
    {
        public DialogViewModel()

        {



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

        private RelayCommand _sendMessageCommand;

        public RelayCommand SendMessageCommand

        {

            get

            {

                return _sendMessageCommand

                       ?? (_sendMessageCommand = new RelayCommand(SendMessage));

            }

        }

        private void SendMessage()

        {

            Messenger.Default.Send(DialogMessage);

        }


    }

}