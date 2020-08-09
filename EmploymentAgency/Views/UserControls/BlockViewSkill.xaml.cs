using DevExpress.Mvvm;
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
                BackgroundBrush = new SolidColorBrush(Colors.Yellow);
            }, () => IsCanSelect);


            Deselect = new DelegateCommand(() =>
            {
                BackgroundBrush = new SolidColorBrush(Colors.Transparent);
            }, () => IsCanSelect);
        }
    }
}
