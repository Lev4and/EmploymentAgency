using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для BlockViewRequest.xaml
    /// </summary>
    public partial class BlockViewRequest : BlockViewItem
    {
        public BlockViewRequest()
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
