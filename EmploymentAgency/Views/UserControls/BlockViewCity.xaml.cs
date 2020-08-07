using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewCity.xaml
    /// </summary>
    public partial class BlockViewCity : BlockViewItem
    {
        public BlockViewCity() : base()
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
