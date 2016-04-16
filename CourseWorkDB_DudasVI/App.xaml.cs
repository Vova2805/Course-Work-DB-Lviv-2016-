using System.Windows;
using CourseWorkDB_DudasVI.MVVM.Mapping;

namespace CourseWorkDB_DudasVI
{
    public partial class App : Application
    {
        public App()
        {
            Mapping.Create();
        }
    }
}