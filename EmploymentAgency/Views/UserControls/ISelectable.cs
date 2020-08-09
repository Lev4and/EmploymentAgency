using DevExpress.Mvvm;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    public interface ISelectable
    {
        bool IsCanSelect { get; set; }

        SolidColorBrush BackgroundBrush { get; set; }

        SolidColorBrush ForegroundBrush { get; set; }

        DelegateCommand Select { get; set; }

        DelegateCommand Deselect { get; set; }
    }
}
