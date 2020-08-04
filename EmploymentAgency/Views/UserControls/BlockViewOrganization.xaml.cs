using EmploymentAgency.Model.Database.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewOrganization.xaml
    /// </summary>
    public partial class BlockViewOrganization : UserControl
    {
        public object Organization
        {
            get { return (object)GetValue(OrganizationProperty); }
            set { SetValue(OrganizationProperty, value); }
        }

        public static readonly DependencyProperty OrganizationProperty =
            DependencyProperty.Register("Organization", typeof(object), typeof(BlockViewOrganization), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewOrganization), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandler SelectedValueChanged;

        public delegate void SelectedValueHandler(object sender, EventArgs e);

        public BlockViewOrganization()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(SelectedValueChanged != null)
            {
                EventArgs eventArgs = new EventArgs();

                SelectedValueChanged(this, eventArgs);
            }
        }
    }
}
