﻿using DevExpress.Mvvm;
using EmploymentAgency.Commands;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    public class BlockViewItem : UserControl, ISelectable
    {
        public bool IsCanSelect
        {
            get { return (bool)GetValue(IsCanSelectProperty); }
            set { SetValue(IsCanSelectProperty, value); }
        }

        public static readonly DependencyProperty IsCanSelectProperty =
            DependencyProperty.Register("IsCanSelect", typeof(bool), typeof(BlockViewItem), new PropertyMetadata(true));

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BlockViewItem), new PropertyMetadata(null));

        public SolidColorBrush MonochromeBrush
        {
            get { return (SolidColorBrush)GetValue(MonochromeBrushProperty); }
            set { SetValue(MonochromeBrushProperty, value); }
        }

        public static readonly DependencyProperty MonochromeBrushProperty =
            DependencyProperty.Register("MonochromeBrush", typeof(SolidColorBrush), typeof(BlockViewItem), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public DelegateCommand Select { get; set; }

        public DelegateCommand Deselect { get; set; }

        public event BlockViewHandler Click;

        public delegate void BlockViewHandler(object sender, EventArgs e);

        public ICommand OnClick => new DelegateCommand(() =>
        {
            Click?.Invoke(this, new EventArgs());
        });
    }
}
