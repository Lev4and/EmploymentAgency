using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewItems.xaml
    /// </summary>
    public partial class BlockViewItems : UserControl
    {
        private int _ratioTriggeringScroll;
        private int _startupToOutput;
        private int _muchToOutput;

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(BlockViewItems), new PropertyMetadata(""));

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(BlockViewItems), new PropertyMetadata(null));

        public TypeBlockViewItem TypeItem
        {
            get { return (TypeBlockViewItem)GetValue(TypeItemProperty); }
            set { SetValue(TypeItemProperty, value); }
        }

        public static readonly DependencyProperty TypeItemProperty =
            DependencyProperty.Register("TypeItem", typeof(TypeBlockViewItem), typeof(BlockViewItems), new PropertyMetadata(null));

        public enum TypeBlockViewItem { BlockViewBranch, BlockViewCity, BlockViewCountry, BlockViewOrganization, BlockViewSkill, BlockViewStreet, BlockViewVacancy, BlockViewRequest }

        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(BlockViewItems), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewItems;

            if (current != null)
            {
                if (current.Data != null)
                {
                    current.SelectedValue = null;

                    current.RemoveContorls();
                    current.GenerateItems();
                    current.RenderContorls();
                }
            }
        }

        public List<Type> TypesBlockViewItem = new List<Type>()
        {
                typeof(BlockViewBranch),
                typeof(BlockViewCity),
                typeof(BlockViewCountry),
                typeof(BlockViewOrganization),
                typeof(BlockViewSkill),
                typeof(BlockViewStreet),
                typeof(BlockViewVacancy),
                typeof(BlockViewRequest)
        };

        public List<BlockViewItem> Items { get; set; }

        public BlockViewItems()
        {
            InitializeComponent();

            _ratioTriggeringScroll = 80;
            _startupToOutput = 15;
            _muchToOutput = 5;

            Items = new List<BlockViewItem>();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < (Data.Count > _startupToOutput ? _startupToOutput : Data.Count); i++)
            {
                BlockViewItem item = Activator.CreateInstance(TypesBlockViewItem.Find(t => t.Name == TypeItem.ToString())) as BlockViewItem;
                item.VerticalAlignment = VerticalAlignment.Top;
                item.HorizontalAlignment = HorizontalAlignment.Stretch;
                item.Margin = new Thickness(0, 10 + (i * (item.Height + 10)), 10, 0);
                item.Data = Data[i];
                item.Click += Item_Click;
                Items.Add(item);
            }
        }

        private void RemoveContorls()
        {
            while (Grid.Children.Count > 0)
                Grid.Children.Clear();
        }

        private void RenderContorl(UIElement control)
        {
            Grid.Children.Add(control);
        }

        private void RenderContorls()
        {
            foreach (var item in Items)
                Grid.Children.Add(item);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = sender as BlockViewItem;

            SelectedValue = item.Data.GetType().GetProperty(SelectedValuePath).GetValue(item.Data);

            DeselectItems();
            SelectItem(item);
        }

        private void DeselectItems()
        {
            foreach (var item in Items)
                item.Deselect.Execute(item);
        }

        private void SelectItem(BlockViewItem item)
        {
            item.Select.Execute(item);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if ((int)(e.VerticalOffset / (e.ExtentHeight - e.ViewportHeight) * 100) >= _ratioTriggeringScroll)
            {
                if (Items.Count < Data.Count)
                {
                    int itemsCount = Items.Count;

                    for (int i = itemsCount; i < (itemsCount + _muchToOutput < Data.Count ? itemsCount + _muchToOutput : Data.Count); i++)
                    {
                        var item = Activator.CreateInstance(TypesBlockViewItem.Find(t => t.Name == TypeItem.ToString())) as BlockViewItem;
                        item.VerticalAlignment = VerticalAlignment.Top;
                        item.HorizontalAlignment = HorizontalAlignment.Stretch;
                        item.Margin = new Thickness(0, 10 + (i * (item.Height + 10)), 10, 0);
                        item.Data = Data[i];
                        item.Click += Item_Click;
                        Items.Add(item);

                        RenderContorl(item);
                    }
                }
            }
        }
    }
}
