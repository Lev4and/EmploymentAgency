using DevExpress.Mvvm;
using System.Windows;
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
                BackgroundBrush = Application.Current.FindResource("SelectedBlockViewItemBackgroundBrush") as SolidColorBrush; 
            }, () => IsCanSelect);


            Deselect = new DelegateCommand(() => 
            { 
                BackgroundBrush = Application.Current.FindResource("DeselectedBlockViewItemBackgroundBrush") as SolidColorBrush; 
            }, () => IsCanSelect);
        }
    }
}
