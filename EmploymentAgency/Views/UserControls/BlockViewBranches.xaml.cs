﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewBranches.xaml
    /// </summary>
    public partial class BlockViewBranches : UserControl
    {
        public int RatioTriggeringScroll { get; set; }

        public int StartupToOutput { get; set; }

        public int MuchToOutput { get; set; }

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(BlockViewBranches), new PropertyMetadata(""));

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(BlockViewBranches), new PropertyMetadata(null));

        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(BlockViewBranches), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewBranches;

            if (current != null)
            {
                if (current.Data != null)
                {
                    current.RemoveContorls();
                    current.GenerateItems();
                    current.RenderContorls();
                }
            }
        }

        public List<BlockViewBranch> Items
        {
            get { return (List<BlockViewBranch>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<BlockViewBranch>), typeof(BlockViewBranches), new PropertyMetadata(null));

        public BlockViewBranches()
        {
            InitializeComponent();

            RatioTriggeringScroll = 80;
            StartupToOutput = 15;
            MuchToOutput = 5;

            Items = new List<BlockViewBranch>();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < (Data.Count > StartupToOutput ? StartupToOutput : Data.Count); i++)
            {
                var item = new BlockViewBranch();
                item.VerticalAlignment = VerticalAlignment.Top;
                item.Margin = new Thickness(10, 10 + (i * (item.Height + 10)), 10, 0);
                item.Branch = Data[i];
                item.SelectedValueChanged += Item_SelectedValueChanged;
                Items.Add(item);
            }
        }

        private void Item_SelectedValueChanged(object sender, EventArgs e)
        {
            var item = (sender as BlockViewBranch);

            SelectedValue = item.Branch.GetType().GetProperty(SelectedValuePath).GetValue(item.Branch);

            DeselectItems();
            SelectItem(item);
        }

        private void RemoveContorls()
        {
            while (grid.Children.Count > 0)
                grid.Children.Clear();
        }

        private void RenderContorl(UIElement control)
        {
            grid.Children.Add(control);
        }

        private void RenderContorls()
        {
            foreach (var item in Items)
                grid.Children.Add(item);
        }

        private void SelectItem(BlockViewBranch item)
        {
            item.Background = new SolidColorBrush(Colors.Yellow);
        }

        private void DeselectItems()
        {
            foreach (var item in Items)
                item.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if ((int)(e.VerticalOffset / (e.ExtentHeight - e.ViewportHeight) * 100) >= RatioTriggeringScroll)
            {
                if (Items.Count < Data.Count)
                {
                    int itemsCount = Items.Count;

                    for (int i = itemsCount; i < (itemsCount + MuchToOutput < Data.Count ? itemsCount + MuchToOutput : Data.Count); i++)
                    {
                        var item = new BlockViewBranch();
                        item.VerticalAlignment = VerticalAlignment.Top;
                        item.Margin = new Thickness(10, 10 + (i * (item.Height + 10)), 10, 0);
                        item.Branch = Data[i];
                        item.SelectedValueChanged += Item_SelectedValueChanged;
                        Items.Add(item);

                        RenderContorl(item);
                    }
                }
            }
        }
    }
}
