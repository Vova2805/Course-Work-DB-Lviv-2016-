﻿#pragma checksum "..\..\..\Views\HomeWindowSale.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "532DD611F8D595DED21AB8B26C3F0F68"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWorkDB_DudasVI;
using CourseWorkDB_DudasVI.Views.UserControls;
using CourseWorkDB_DudasVI.Views.UserControls.ListItem;
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
    /// HomeWindowSale
    /// </summary>
    public partial class HomeWindowSale : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 117 "..\..\..\Views\HomeWindowSale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout AdminFlyout;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\Views\HomeWindowSale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout UserFilterFlyout;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\Views\HomeWindowSale.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Flyout CustomerFilterFlyout;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWorkDB_DudasVI;component/views/homewindowsale.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\HomeWindowSale.xaml"
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
            
            #line 30 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SettingsClick);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 58 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LogoutClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 83 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Help);
            
            #line default
            #line hidden
            return;
            case 4:
            this.AdminFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 5:
            this.UserFilterFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 6:
            this.CustomerFilterFlyout = ((MahApps.Metro.Controls.Flyout)(target));
            return;
            case 7:
            
            #line 129 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.TabControl)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 165 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CustomerFilter);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 303 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CustomerFilter);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 420 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CustomerFilter);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 537 "..\..\..\Views\HomeWindowSale.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CustomerFilter);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

