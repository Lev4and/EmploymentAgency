using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewBranch.xaml
    /// </summary>
    public partial class BlockViewBranch : BlockViewItem
    {
        public BlockViewBranch()
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
