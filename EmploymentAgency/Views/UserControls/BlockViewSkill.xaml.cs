using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewSkill.xaml
    /// </summary>
    public partial class BlockViewSkill : BlockViewItem
    {
        public BlockViewSkill()
        {
            InitializeComponent();

            Select = new DelegateCommand(() =>
            {
                BackgroundBrush = Application.Current.FindResource("SelectedBlockViewItemBackgroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);


            Deselect = new DelegateCommand(() =>
            {
                BackgroundBrush = Application.Current.FindResource("DeselectedBlockViewItemBackgroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);
        }
    }
}
