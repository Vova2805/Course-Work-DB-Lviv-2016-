﻿#pragma checksum "..\..\..\Views\HomeWindowSpecialist.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "788FA4E30BDEB185EF0F70EBB76EC312"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWorkDB_DudasVI.MVVM.ViewModels;
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


namespace CourseWorkDB_DudasVI.Views {
    
    
    /// <summary>
    /// HomeWindowSpecialist
    /// </summary>
    public partial class HomeWindowSpecialist : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 55 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout AdminFlyout;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout CostFilterFlyout;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout GoodsFilterFlyout;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout SpecialistEditOrders;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid OrdersGrid;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\Views\HomeWindowSpecialist.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CourseWorkDB_DudasVI.Views.UserControls.ChartsSet ChartsSetView;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWorkDB_DudasVI;component/views/homewindowspecialist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\HomeWindowSpecialist.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 24 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SettingsClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AdminFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 3:
            this.CostFilterFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 4:
            this.GoodsFilterFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 5:
            this.SpecialistEditOrders = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 6:
            
            #line 71 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.TabControl)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.OrdersGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            
            #line 112 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditOrdersOpen);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 125 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCheckAll);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 137 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnUncheckAll);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 150 "..\..\..\Views\HomeWindowSpecialist.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RefreshDiagram);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ChartsSetView = ((CourseWorkDB_DudasVI.Views.UserControls.ChartsSet)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

