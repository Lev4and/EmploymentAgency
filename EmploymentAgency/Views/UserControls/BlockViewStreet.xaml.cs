using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewStreet.xaml
    /// </summary>
    public partial class BlockViewStreet : UserControl
    {
        public object Street
        {
            get { return (object)GetValue(StreetProperty); }
            set { SetValue(StreetProperty, value); }
        }

        public static readonly DependencyProperty StreetProperty =
            DependencyProperty.Register("Street", typeof(object), typeof(BlockViewStreet), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewStreet), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandled SelectedValueChanged;

        public delegate void SelectedValueHandled(object sender, EventArgs e);

        public BlockViewStreet()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
