using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeEmploymentTypeViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private EmploymentType _employmentType;

        public static int SelectedIdEmploymentType { get; set; }


        public string EmploymentTypeName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _employmentType = _executor.GetEmploymentType(SelectedIdEmploymentType);

            EmploymentTypeName = _employmentType.EmploymentTypeName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateEmploymentType(SelectedIdEmploymentType, EmploymentTypeName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Тип занятости с такими данными уже существует");
            }
        }, () => (EmploymentTypeName != null ? EmploymentTypeName.Length > 0 : false));
    }
}
