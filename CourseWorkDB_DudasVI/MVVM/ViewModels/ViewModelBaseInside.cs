﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ourseWorkDB_DudasVI.MVVM.ViewModels
{
    public abstract class ViewModelBaseInside : INotifyPropertyChanged
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