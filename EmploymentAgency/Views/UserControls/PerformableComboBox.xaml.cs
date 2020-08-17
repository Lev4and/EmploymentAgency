using Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PerformableComboBox.xaml
    /// </summary>
    public partial class PerformableComboBox : UserControl
    {
        private int _ratioTriggeringScroll;
        private int _startupToOutput;
        private int _muchToOutput;

        public bool IsEditable
        {
            get { return (bool)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable", typeof(bool), typeof(PerformableComboBox), new PropertyMetadata(true));

        public bool IsDrawerOpen
        {
            get { return (bool)GetValue(IsDrawerOpenProperty); }
            set { SetValue(IsDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty IsDrawerOpenProperty =
            DependencyProperty.Register("IsDrawerOpen", typeof(bool), typeof(PerformableComboBox), new PropertyMetadata(false));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(PerformableComboBox), new PropertyMetadata(""));

        public string SearchLine
        {
            get { return (string)GetValue(SearchLineProperty); }
            set { SetValue(SearchLineProperty, value); }
        }

        public static readonly DependencyProperty SearchLineProperty =
            DependencyProperty.Register("SearchLine", typeof(string), typeof(PerformableComboBox), new PropertyMetadata("", SearchLine_Changed));

        private static void SearchLine_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as PerformableComboBox;

            if(current != null)
            {
                if(current.DataObjects != null)
                {
                    if (current.SearchLine.Length > 0)
                    {
                        current.IsDrawerOpen = false;

                        current.Search();

                        current.RemoveContorls();
                        current.GenerateItems();
                        current.RenderContorls();

                        current.IsDrawerOpen = current.Items.Count > 0;
                    }
                    else
                    {
                        current.IsDrawerOpen = false;

                        current.SelectedValue = null;
                    }
                }
            }
        }

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(PerformableComboBox), new PropertyMetadata(""));

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(PerformableComboBox), new PropertyMetadata("Строка поиска"));

        public string HelperText
        {
            get { return (string)GetValue(HelperTextProperty); }
            set { SetValue(HelperTextProperty, value); }
        }

        public static readonly DependencyProperty HelperTextProperty =
            DependencyProperty.Register("HelperText", typeof(string), typeof(PerformableComboBox), new PropertyMetadata("Укажите или выберите данные"));

        public object SelectedValue
        {
            get { return (object)GetValue(SelectedValueProperty); }
            set { SetValue(SelectedValueProperty, value); }
        }

        public static readonly DependencyProperty SelectedValueProperty =
            DependencyProperty.Register("SelectedValue", typeof(object), typeof(PerformableComboBox), new PropertyMetadata(null));

        public List<PerformableComboBoxItem> Items
        {
            get { return (List<PerformableComboBoxItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<PerformableComboBoxItem>), typeof(PerformableComboBox), new PropertyMetadata(null));

        public ObservableCollection<Type> Data
        {
            get { return (ObservableCollection<Type>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<Type>), typeof(PerformableComboBox), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as PerformableComboBox;

            if(current != null)
            {
                current.DataObjects = CollectionConverter<object>.ConvertToObservableCollection(CollectionConverter<Type>.ConvertToObjectList(current.Data.ToList()));
            }
        }

        public ObservableCollection<object> DataObjects
        {
            get { return (ObservableCollection<object>)GetValue(DataObjectsProperty); }
            set { SetValue(DataObjectsProperty, value); }
        }

        public static readonly DependencyProperty DataObjectsProperty =
            DependencyProperty.Register("DataObjects", typeof(ObservableCollection<object>), typeof(PerformableComboBox), new PropertyMetadata(null, DataObjects_Changed));

        private static void DataObjects_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as PerformableComboBox;

            if(current != null)
            {
                current.IsDrawerOpen = false;

                current.Search();

                current.RemoveContorls();
                current.GenerateItems();
                current.RenderContorls();

                if (current.SelectedValue != null)
                    current.SetSearchLine(current.SelectedValue);
                else
                    current.SelectedValue = null;
            }
        }

        public ObservableCollection<object> DisplayedData
        {
            get { return (ObservableCollection<object>)GetValue(DisplayedDataProperty); }
            set { SetValue(DisplayedDataProperty, value); }
        }

        public static readonly DependencyProperty DisplayedDataProperty =
            DependencyProperty.Register("DisplayedData", typeof(ObservableCollection<object>), typeof(PerformableComboBox), new PropertyMetadata(null));

        public PerformableComboBox()
        {
            InitializeComponent();

            _ratioTriggeringScroll = 80;
            _startupToOutput = 15;
            _muchToOutput = 5;

            Items = new List<PerformableComboBoxItem>();
        }

        private bool IsInformationCorrect(object information)
        {
            return SearchLine.Length > 0 ? GetTextInformation(information).ToLower().StartsWith(SearchLine.ToLower()) : true;
        }

        private string GetTextInformation(object information)
        {
            return information.GetType().GetProperty(DisplayMemberPath).GetValue(information).ToString();
        }

        private object GetSelectedValue(PerformableComboBoxItem item)
        {
            return item.Data.GetType().GetProperty(SelectedValuePath).GetValue(item.Data);
        }

        private PerformableComboBoxItem GetItem(int index)
        {
            return Items[index];
        }

        private void Search()
        {
            DisplayedData = new ObservableCollection<object>();

            foreach(var info in DataObjects)
            {
                if (IsInformationCorrect(info))
                {
                    DisplayedData.Add(info);
                }
            }
        }

        private void SetSearchLine(object selectedValue)
        {
            var info = DataObjects.SingleOrDefault(d => d.GetType().GetProperty(SelectedValuePath).GetValue(d) == selectedValue);

            SearchLine = $"{info.GetType().GetProperty(DisplayMemberPath).GetValue(info)}";
        }

        private void RemoveContorls()
        {
            while (Grid.Children.Count > 0)
                Grid.Children.Clear();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < (DisplayedData.Count > _startupToOutput ? _startupToOutput : DisplayedData.Count); i++)
            {
                var item = new PerformableComboBoxItem(DisplayedData[i], DisplayMemberPath);
                item.VerticalAlignment = VerticalAlignment.Top;
                item.Margin = new Thickness(0, (i * item.ActualHeight), 0, 0);
                item.Click += Item_Click;
                item.MouseMove += Item_MouseMove;
                item.MouseLeave += Item_MouseLeave;
                Items.Add(item);
            }
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

        private void DeselectItems()
        {
            foreach (var item in Items)
                item.Deselect.Execute(item);
        }

        private void SelectItem(PerformableComboBoxItem item)
        {
            item.Select.Execute(item);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var item = sender as PerformableComboBoxItem;

            DeselectItems();

            SearchLine = item.ResultSearch;
            SelectedValue = GetSelectedValue(item);

            IsDrawerOpen = false;
        }

        private void Item_MouseMove(object sender, EventArgs e)
        {
            DeselectItems();

            var item = sender as PerformableComboBoxItem;

            item.Select.Execute(item);
        }

        private void Item_MouseLeave(object sender, EventArgs e)
        {
            DeselectItems();

            var item = sender as PerformableComboBoxItem;

            item.Deselect.Execute(item);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if ((int)(e.VerticalOffset / (e.ExtentHeight - e.ViewportHeight) * 100) >= _ratioTriggeringScroll)
            {
                if (Items.Count < DisplayedData.Count)
                {
                    int itemsCount = Items.Count;

                    for (int i = itemsCount; i < (itemsCount + _muchToOutput < DisplayedData.Count ? itemsCount + _muchToOutput : DisplayedData.Count); i++)
                    {
                        var item = new PerformableComboBoxItem(DisplayedData[i], DisplayMemberPath);
                        item.VerticalAlignment = VerticalAlignment.Top;
                        item.Margin = new Thickness(0, (i * item.ActualHeight), 0, 0);
                        item.Click += Item_Click;
                        item.MouseMove += Item_MouseMove;
                        item.MouseLeave += Item_MouseLeave;
                        Items.Add(item);

                        RenderContorl(item);
                    }
                }
            }
        }

        private void FieldTextBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Items.Count > 0)
            {
                int index = Items.FindIndex(i => i.IsSelect);

                switch (e.Key)
                {
                    case Key.Up:
                        {
                            DeselectItems();

                            index = index > 0 ? index -= 1 : index = Items.Count - 1;

                            var item = GetItem(index);

                            item.Select.Execute(item);
                        }
                        break;
                    case Key.Down:
                        {
                            DeselectItems();

                            index = index < Items.Count - 1 ? index += 1 : index = 0;

                            var item = GetItem(index);

                            item.Select.Execute(item);
                        }
                        break;
                    case Key.Enter:
                        {
                            DeselectItems();

                            var item = GetItem(index);

                            SearchLine = item.ResultSearch;
                            SelectedValue = GetSelectedValue(item);

                            item.Select.Execute(item);

                            IsDrawerOpen = false;
                        }
                        break;
                    case Key.Escape:
                        {
                            DeselectItems();

                            IsDrawerOpen = false;
                        }
                        break;
                }
            }
        }
    }
}
