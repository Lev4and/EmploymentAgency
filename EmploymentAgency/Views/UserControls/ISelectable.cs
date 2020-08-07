using DevExpress.Mvvm;
using EmploymentAgency.Commands;
using System;
using System.Windows.Media;

namespace EmploymentAgency.Views.UserControls
{
    public interface ISelectable
    {
        bool IsCanSelect { get; set; }

        SolidColorBrush MonochromeBrush { get; set; }

        DelegateCommand Select { get; set; }

        DelegateCommand Deselect { get; set; }
    }
}
