using System;
using System.Windows;
using System.Windows.Controls;

namespace CourseWorkDB_DudasVI.Views.UserControls
{
    public partial class Test 
    {
        public Test()
        {
            InitializeComponent();
            this.DataContext = new ViewModelLocator().Main;
        }
        
    }
}