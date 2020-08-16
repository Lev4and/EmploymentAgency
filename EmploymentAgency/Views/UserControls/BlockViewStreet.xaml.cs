using DevExpress.Mvvm;
using System.Windows;
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
                BackgroundBrush = Application.Current.FindResource("SelectedBlockViewItemBackgroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);


            Deselect = new DelegateCommand(() =>
            {
                BackgroundBrush = Application.Current.FindResource("DeselectedBlockViewItemBackgroundBrush") as SolidColorBrush;
            }, () => IsCanSelect);
        }
    }
}
