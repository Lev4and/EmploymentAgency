using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewStreet.xaml
    /// </summary>
    public partial class BlockViewStreet : BlockViewItem
    {
        public BlockViewStreet()
        {
            InitializeComponent();

            Select = new DelegateCommand(() =>
            {
                MonochromeBrush = new SolidColorBrush(Colors.Yellow);
            }, () => IsCanSelect);


            Deselect = new DelegateCommand(() =>
            {
                MonochromeBrush = new SolidColorBrush(Colors.Transparent);
            }, () => IsCanSelect);
        }
    }
}
