﻿#pragma checksum "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "49A03C8CEA41D0AB4880DF4233F01DED"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWorkDB_DudasVI.Converters;
using CourseWorkDB_DudasVI.Views.Rules;
using CourseWorkDB_DudasVI.Views.UserControls;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CourseWorkDB_DudasVI.Views.UserControls {
    
    
    /// <summary>
    /// CurrentPlanFilter
    /// </summary>
    public partial class CurrentPlanFilter : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 73 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.NumericUpDown fromPrice;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.NumericUpDown toPrice;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker FromDatePicker;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker ToDatePicker;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CourseWorkDB_DudasVI;component/views/usercontrols/filters/specialist/currentplan" +
                    "filter%20-%20%d0%9a%d0%be%d0%bf%d0%b8%d1%80%d0%be%d0%b2%d0%b0%d1%82%d1%8c.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 63 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ProductSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 71 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            ((MahApps.Metro.Controls.ToggleSwitch)(target)).IsCheckedChanged += new System.EventHandler(this.CheckedChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.fromPrice = ((MahApps.Metro.Controls.NumericUpDown)(target));
            return;
            case 4:
            this.toPrice = ((MahApps.Metro.Controls.NumericUpDown)(target));
            return;
            case 5:
            
            #line 80 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CategorySelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 91 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FilterByPrice);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FromDatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 162 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            this.FromDatePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.FromTimeChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ToDatePicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 176 "..\..\..\..\..\..\Views\UserControls\Filters\Specialist\CurrentPlanFilter - Копировать.xaml"
            this.ToDatePicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.ToTimeChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

