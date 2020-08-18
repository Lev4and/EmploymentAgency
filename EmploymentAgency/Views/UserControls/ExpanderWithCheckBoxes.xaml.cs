using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ExpanderWithCheckBoxes.xaml
    /// </summary>
    public partial class ExpanderWithCheckBoxes : UserControl
    {
        public int RatioTriggeringScroll { get; set; }

        public int StartupToOutput { get; set; }

        public int MuchToOutput { get; set; }

        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as ExpanderWithCheckBoxes;

            if(current != null)
            {
                if (current.Data != null)
                {
                    if (current.SelectedValues != null)
                        current.FormattingSelectedValues();

                    current.RemoveContorls();
                    current.GenerateItems();
                    current.RenderContorls();
                }
            }
        }

        public List<CheckBoxForExpander> Items
        {
            get { return (List<CheckBoxForExpander>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<CheckBoxForExpander>), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(null));

        public ObservableCollection<object> SelectedValues
        {
            get { return (ObservableCollection<object>)GetValue(SelectedValuesProperty); }
            set { SetValue(SelectedValuesProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuesProperty =
            DependencyProperty.Register("SelectedValues", typeof(ObservableCollection<object>), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(null));

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(""));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(""));

        public string ElementHeader
        {
            get { return (string)GetValue(ElementHeaderProperty); }
            set { SetValue(ElementHeaderProperty, value); }
        }

        public static readonly DependencyProperty ElementHeaderProperty =
            DependencyProperty.Register("ElementHeader", typeof(string), typeof(ExpanderWithCheckBoxes), new PropertyMetadata("Header"));

        public ExpanderWithCheckBoxes()
        {
            InitializeComponent();

            RatioTriggeringScroll = 80;
            StartupToOutput = 15;
            MuchToOutput = 5;

            Items = new List<CheckBoxForExpander>();

            SelectedValues = new ObservableCollection<object>();
        }

        private bool ContainsValue(object obj)
        {
            return SelectedValues.SingleOrDefault(o =>
            o.ToString() == obj.GetType().GetProperty(SelectedValuePath).GetValue(obj).ToString()) != null;
        }

        private void FormattingSelectedValues()
        {
            for(int i = 0; i < SelectedValues.Count; i++)
            {
                if (SelectedValuePath != null ? SelectedValuePath.Length > 0 : false)
                    SelectedValues[i] = SelectedValues[i].GetType().GetProperty(SelectedValuePath).GetValue(SelectedValues[i]);
            }
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < (Data.Count > StartupToOutput ? StartupToOutput : Data.Count); i++)
            {
                var checkBox = new CheckBoxForExpander();
                checkBox.VerticalAlignment = VerticalAlignment.Top;
                checkBox.HorizontalAlignment = HorizontalAlignment.Left;
                checkBox.Margin = new Thickness(10, 10 + (i * 30), 0, 0);
                checkBox.IsChecked = ContainsValue(Data[i]);
                checkBox.Data = Data[i];
                checkBox.DisplayMemberPath = DisplayMemberPath;
                checkBox.SelectedValuePath = SelectedValuePath;
                checkBox.CheckedChanged += CheckBox_CheckedChanged;
                Items.Add(checkBox);
            }
        }

        private void CheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            var item = sender as CheckBoxForExpander;

            if(item.IsChecked)
                AddSelectedValue(item.SelectedValue);
            else
                RemoveSelectedValue(item.SelectedValue);
        }

        private void AddSelectedValue(object selectedValue)
        {
            if (SelectedValues != null)
            {
                if (!SelectedValues.Contains(selectedValue))
                    SelectedValues.Add(selectedValue);
            }
            else
            {
                SelectedValues = new ObservableCollection<object>();

                if (!SelectedValues.Contains(selectedValue))
                    SelectedValues.Add(selectedValue);
            }
        }

        private void RemoveSelectedValue(object selectedValue)
        {
            SelectedValues.Remove(selectedValue);
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

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if ((int)(e.VerticalOffset / (e.ExtentHeight - e.ViewportHeight) * 100) >= RatioTriggeringScroll)
            {
                if (Items.Count < Data.Count)
                {
                    int itemsCount = Items.Count;

                    for (int i = itemsCount; i < (itemsCount + MuchToOutput < Data.Count ? itemsCount + MuchToOutput : Data.Count); i++)
                    {
                        var checkBox = new CheckBoxForExpander();
                        checkBox.VerticalAlignment = VerticalAlignment.Top;
                        checkBox.HorizontalAlignment = HorizontalAlignment.Left;
                        checkBox.Margin = new Thickness(10, 10 + (i * 30), 0, 0);
                        checkBox.IsChecked = ContainsValue(Data[i]);
                        checkBox.Data = Data[i];
                        checkBox.DisplayMemberPath = DisplayMemberPath;
                        checkBox.SelectedValuePath = SelectedValuePath;
                        checkBox.CheckedChanged += CheckBox_CheckedChanged;
                        Items.Add(checkBox);

                        RenderContorl(checkBox);
                    }
                }
            }
        }
    }
}
