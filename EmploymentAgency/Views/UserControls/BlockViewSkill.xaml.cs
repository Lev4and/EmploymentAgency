using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewSkill.xaml
    /// </summary>
    public partial class BlockViewSkill : UserControl
    {
        public object Skill
        {
            get { return (object)GetValue(SkillProperty); }
            set { SetValue(SkillProperty, value); }
        }

        public static readonly DependencyProperty SkillProperty =
            DependencyProperty.Register("Skill", typeof(object), typeof(BlockViewSkill), new PropertyMetadata(null));

        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(SolidColorBrush), typeof(BlockViewSkill), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public event SelectedValueHandler SelectedValueChanged;

        public delegate void SelectedValueHandler(object sender, EventArgs e);

        public BlockViewSkill()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
