using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
