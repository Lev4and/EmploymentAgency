using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewOrganization.xaml
    /// </summary>
    public partial class BlockViewOrganization : BlockViewItem
    {
        public BlockViewOrganization()
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
