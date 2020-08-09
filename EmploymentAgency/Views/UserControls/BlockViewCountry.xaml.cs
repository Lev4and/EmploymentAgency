using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewCountry.xaml
    /// </summary>
    public partial class BlockViewCountry : BlockViewItem
    {
        public BlockViewCountry()
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
