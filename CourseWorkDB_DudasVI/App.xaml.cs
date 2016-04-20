using System.Windows;
using CourseWorkDB_DudasVI.MVVM.Mapping;
using CourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI
{
    public partial class App : Application
    {
        public static SettingsViewModel settingsViewModel;

        public App()
        {
            Mapping.Create();
            settingsViewModel = new SettingsViewModel();
        }
    }
}