using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewBranch.xaml
    /// </summary>
    public partial class BlockViewBranch : UserControl
    {
        public object Branch
        {
            get { return (object)GetValue(BranchProperty); }
            set { SetValue(BranchProperty, value); }
        }

        public static readonly DependencyProperty BranchProperty =
            DependencyProperty.Register("Branch", typeof(object), typeof(BlockViewBranch), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewBranch), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandler SelectedValueChanged;

        public delegate void SelectedValueHandler(object sender, EventArgs e);

        public BlockViewBranch()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
