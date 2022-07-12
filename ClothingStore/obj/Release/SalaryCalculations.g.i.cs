﻿#pragma checksum "..\..\SalaryCalculations.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C3FE3C4C048D6966DD140BF0884226E51EC97207913406898C35FBCA94EF73FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ClothingStore;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace ClothingStore {
    
    
    /// <summary>
    /// SalaryCalculations
    /// </summary>
    public partial class SalaryCalculations : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 17 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid salaryGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox hoursBox;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox scheludeBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox costBox;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addBtn;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox filterBox;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock warningBox;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\SalaryCalculations.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReset;
        
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
            System.Uri resourceLocater = new System.Uri("/ClothingStore;component/salarycalculations.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SalaryCalculations.xaml"
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
            this.salaryGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.hoursBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\SalaryCalculations.xaml"
            this.hoursBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.hoursBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 4:
            this.scheludeBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.costBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 100 "..\..\SalaryCalculations.xaml"
            this.costBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.costBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addBtn = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\SalaryCalculations.xaml"
            this.addBtn.Click += new System.Windows.RoutedEventHandler(this.addBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.filterBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 159 "..\..\SalaryCalculations.xaml"
            this.filterBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.filterBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.warningBox = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.btnReset = ((System.Windows.Controls.Button)(target));
            
            #line 161 "..\..\SalaryCalculations.xaml"
            this.btnReset.Click += new System.Windows.RoutedEventHandler(this.btnReset_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 26 "..\..\SalaryCalculations.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Delete_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
