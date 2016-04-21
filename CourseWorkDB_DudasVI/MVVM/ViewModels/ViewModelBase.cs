using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseWorkDB_DudasVI.General;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _AddPermition;

        public bool AddPermition
        {
            get { return _AddPermition; }
            set
            {
                _AddPermition = value;
                OnPropertyChanged("AddPermition");
            }
        }

        public string userNameSurname
        {
            get
            {
                return Session.User.POST.POST_NAME + " " + Session.User.STAFF_NAME + " " + Session.User.STAFF_SURNAME;
            }
        }

        #region Body

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}