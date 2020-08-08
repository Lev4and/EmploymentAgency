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
    /// Логика взаимодействия для SearchByInputField.xaml
    /// </summary>
    public partial class SearchByInputField : UserControl
    {
        private ObservableCollection<object> _savedData;

        public bool IsDrawerOpen
        {
            get { return (bool)GetValue(IsDrawerOpenProperty); }
            set { SetValue(IsDrawerOpenProperty, value); }
        }

        public static readonly DependencyProperty IsDrawerOpenProperty =
            DependencyProperty.Register("IsDrawerOpen", typeof(bool), typeof(SearchByInputField), new PropertyMetadata(false));

        public int MaxToOutput
        {
            get { return (int)GetValue(MaxToOutputProperty); }
            set { SetValue(MaxToOutputProperty, value); }
        }

        public static readonly DependencyProperty MaxToOutputProperty =
            DependencyProperty.Register("MaxToOutput", typeof(int), typeof(SearchByInputField), new PropertyMetadata(5));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(SearchByInputField), new PropertyMetadata(""));

        public string SearchLine
        {
            get { return (string)GetValue(SearchLineProperty); }
            set { SetValue(SearchLineProperty, value); }
        }

        public static readonly DependencyProperty SearchLineProperty =
            DependencyProperty.Register("SearchLine", typeof(string), typeof(SearchByInputField), new PropertyMetadata("", SearchLine_Change));

        private static void SearchLine_Change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as SearchByInputField;

            if(current != null)
            {
                if(current._savedData != null)
                {
                    if(current.SearchLine.Length > 0)
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
                    }
                }
            }
        }

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(SearchByInputField), new PropertyMetadata("Строка поиска"));

        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(SearchByInputField), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as SearchByInputField;

            if (current != null)
            {
                if (current._savedData == null)
                    current._savedData = current.Data;
            }
        }

        public ObservableCollection<object> DisplayedData
        {
            get { return (ObservableCollection<object>)GetValue(DisplayedDataProperty); }
            set { SetValue(DisplayedDataProperty, value); }
        }

        public static readonly DependencyProperty DisplayedDataProperty =
            DependencyProperty.Register("DisplayedData", typeof(ObservableCollection<object>), typeof(SearchByInputField), new PropertyMetadata(null));

        public List<SearchByInputFieldItem> Items
        {
            get { return (List<SearchByInputFieldItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(List<SearchByInputFieldItem>), typeof(SearchByInputField), new PropertyMetadata(null));

        public SearchByInputField()
        {
            InitializeComponent();

            Items = new List<SearchByInputFieldItem>();
        }

        private bool IsInformationCorrect(object information)
        {
            return GetTextInformation(information).ToLower().StartsWith(SearchLine.ToLower());
        }

        private bool ContainsDisplayedData(object information)
        {
            foreach(var info in DisplayedData)
            {
                var infoResutText = info.GetType().GetProperty(DisplayMemberPath).GetValue(info).ToString();
                var informationResutText = information.GetType().GetProperty(DisplayMemberPath).GetValue(information).ToString();

                if (infoResutText == informationResutText)
                    return true;
            }

            return false;
        }

        private string GetTextInformation(object information)
        {
            return information.GetType().GetProperty(DisplayMemberPath).GetValue(information).ToString();
        }

        private SearchByInputFieldItem GetItem(int index)
        {
            return Items[index];
        }

        private void Search()
        {
            DisplayedData = new ObservableCollection<object>();

            foreach(var info in _savedData)
            {
                if (IsInformationCorrect(info))
                {
                    if (DisplayedData.Count < MaxToOutput && !ContainsDisplayedData(info))
                        DisplayedData.Add(info);
                    else
                        break;
                }
            }
        }

        private void DeselectItems()
        {
            foreach (var item in Items)
                item.Deselect.Execute(item);
        }

        private void RemoveContorls()
        {
            while (grid.Children.Count > 0)
                grid.Children.Clear();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < DisplayedData.Count; i++)
            {
                var item = new SearchByInputFieldItem();
                item.VerticalAlignment = VerticalAlignment.Top;
                item.Margin = new Thickness(0, (i * 60), 0, 0);
                item.ResultSearch = GetTextInformation(DisplayedData[i]);
                Items.Add(item);
            }
        }

        private void RenderContorls()
        {
            foreach (var item in Items)
                grid.Children.Add(item);
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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
