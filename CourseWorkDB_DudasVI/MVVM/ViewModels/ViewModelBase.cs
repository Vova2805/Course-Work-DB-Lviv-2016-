using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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