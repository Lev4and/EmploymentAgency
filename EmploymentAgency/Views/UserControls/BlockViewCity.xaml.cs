using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewCity.xaml
    /// </summary>
    public partial class BlockViewCity : UserControl
    {
        public object City
        {
            get { return (object)GetValue(CityProperty); }
            set { SetValue(CityProperty, value); }
        }

        public static readonly DependencyProperty CityProperty =
            DependencyProperty.Register("City", typeof(object), typeof(BlockViewCity), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static new readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewCity), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandler SelectedValueChanged;

        public delegate void SelectedValueHandler(object sender, EventArgs e);

        public BlockViewCity()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
