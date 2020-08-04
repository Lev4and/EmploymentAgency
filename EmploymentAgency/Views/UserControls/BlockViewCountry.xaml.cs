using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewCountry.xaml
    /// </summary>
    public partial class BlockViewCountry : UserControl
    {
        public object Country
        {
            get { return (object)GetValue(CountryProperty); }
            set { SetValue(CountryProperty, value); }
        }

        public static readonly DependencyProperty CountryProperty =
            DependencyProperty.Register("Country", typeof(object), typeof(BlockViewCountry), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewCountry), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandler SelectedValueChanged;

        public delegate void SelectedValueHandler(object sender, EventArgs e);

        public BlockViewCountry()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
