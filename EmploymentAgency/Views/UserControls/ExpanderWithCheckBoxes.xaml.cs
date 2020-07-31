using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ExpanderWithCheckBoxes.xaml
    /// </summary>
    public partial class ExpanderWithCheckBoxes : UserControl
    {
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
                    current.RemoveContorls();
                    current.GenerateItems();
                    current.RenderContorls();
                }
            }
        }

        public List<CheckBox> Items
        {
            get { return (List<CheckBox>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<CheckBox>), typeof(ExpanderWithCheckBoxes), new PropertyMetadata(null));

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

            Items = new List<CheckBox>();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < Data.Count; i++)
            {
                var checkBox = new CheckBox();
                checkBox.VerticalAlignment = VerticalAlignment.Top;
                checkBox.HorizontalAlignment = HorizontalAlignment.Left;
                checkBox.Margin = new Thickness(10, 10 + (i * 30), 0, 0);
                checkBox.IsChecked = false;
                checkBox.Name = $"_{Data[i].GetType().GetProperty(SelectedValuePath).GetValue(Data[i])}";
                checkBox.Content = Data[i].GetType().GetProperty(DisplayMemberPath).GetValue(Data[i]);
                checkBox.Checked += Item_Checked;
                checkBox.Unchecked += Item_Unchecked;
                Items.Add(checkBox);
            }
        }

        private void RemoveContorls()
        {
            while (grid.Children.Count > 0)
                grid.Children.Clear();
        }

        private void RenderContorls()
        {
            foreach (var item in Items)
                grid.Children.Add(item);
        }

        private void Item_Checked(object sender, RoutedEventArgs e)
        {
            if (SelectedValues != null)
            {
                if (!SelectedValues.Contains((sender as CheckBox).Name.ToString().Substring(1)))
                    SelectedValues.Add((sender as CheckBox).Name.ToString().Substring(1));
            }
            else
            {
                SelectedValues = new ObservableCollection<object>();

                if (!SelectedValues.Contains((sender as CheckBox).Name.ToString().Substring(1)))
                    SelectedValues.Add((sender as CheckBox).Name.ToString().Substring(1));
            }
        }

        private void Item_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectedValues.Remove((sender as CheckBox).Name.ToString().Substring(1));
        }
    }
}
