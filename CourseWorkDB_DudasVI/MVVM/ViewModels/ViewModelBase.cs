using System.ComponentModel;
using System.Runtime.CompilerServices;
using CourseWorkDB_DudasVI.General;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string userNameSurname
        {
            get { return Session.User.POST.POST_NAME+" "+Session.User.STAFF_NAME + " " + Session.User.STAFF_SURNAME; }
        }
    }
}