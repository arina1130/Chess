﻿#pragma checksum "..\..\PawnChange.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6BD78A760545E96CF1FCCC49060C0E7C38DF15D0428A23DC5F361ADA1A7120F6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Chess;
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


namespace Chess {
    
    
    /// <summary>
    /// PawnChange
    /// </summary>
    public partial class PawnChange : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 39 "..\..\PawnChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonQueen;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\PawnChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonRook;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\PawnChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonBishop;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\PawnChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonKnight;
        
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
            System.Uri resourceLocater = new System.Uri("/Chess;component/pawnchange.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PawnChange.xaml"
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
            this.buttonQueen = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\PawnChange.xaml"
            this.buttonQueen.Click += new System.Windows.RoutedEventHandler(this.buttonQueen_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.buttonRook = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\PawnChange.xaml"
            this.buttonRook.Click += new System.Windows.RoutedEventHandler(this.buttonRook_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonBishop = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\PawnChange.xaml"
            this.buttonBishop.Click += new System.Windows.RoutedEventHandler(this.buttonBishop_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonKnight = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\PawnChange.xaml"
            this.buttonKnight.Click += new System.Windows.RoutedEventHandler(this.buttonKnight_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

