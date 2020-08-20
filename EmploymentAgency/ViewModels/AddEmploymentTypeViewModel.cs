using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddEmploymentTypeViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string EmploymentTypeName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddEmploymentType(EmploymentTypeName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Тип занятости с такими данными уже существует");
            }
        }, () => (EmploymentTypeName != null ? EmploymentTypeName.Length > 0 : false));
    }
}
