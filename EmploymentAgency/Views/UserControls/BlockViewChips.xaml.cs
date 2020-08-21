using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewChips.xaml
    /// </summary>
    public partial class BlockViewChips : UserControl
    {
        public string PhotoPath
        {
            get { return (string)GetValue(PhotoPathProperty); }
            set { SetValue(PhotoPathProperty, value); }
        }

        public static readonly DependencyProperty PhotoPathProperty =
            DependencyProperty.Register("PhotoPath", typeof(string), typeof(BlockViewChips), new PropertyMetadata(""));

        public string TextPath
        {
            get { return (string)GetValue(TextPathProperty); }
            set { SetValue(TextPathProperty, value); }
        }

        public static readonly DependencyProperty TextPathProperty =
            DependencyProperty.Register("TextPath", typeof(string), typeof(BlockViewChips), new PropertyMetadata(""));

        public ObservableCollection<object> Data
        {
            get { return (ObservableCollection<object>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(ObservableCollection<object>), typeof(BlockViewChips), new PropertyMetadata(null, Data_Changed));

        private static void Data_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as BlockViewChips;

            if(current != null)
            {
                if(current.Data != null)
                {
                    current.RemoveContorls();
                    current.GenerateItems();
                    current.RenderContorls();
                }
            }
        }

        public List<Chip> Items { get; set; }

        public BlockViewChips()
        {
            InitializeComponent();

            Items = new List<Chip>();
        }

        private void RemoveContorls()
        {
            while (WrapPanel.Children.Count > 0)
                WrapPanel.Children.Clear();
        }

        private void GenerateItems()
        {
            Items.Clear();

            for (int i = 0; i < Data.Count; i++)
            {
                Chip item = new Chip(TextPath, PhotoPath, Data[i]);

                Items.Add(item);
            }
        }

        private void RenderContorls()
        {
            foreach (var item in Items)
                WrapPanel.Children.Add(item);
        }
    }
}
