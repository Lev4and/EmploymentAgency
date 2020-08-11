using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddRoleViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string RoleName { get; set; }

        public AddRoleViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddRole(RoleName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Должность с такими данными уже существует");
            }
        }, () => (RoleName != null ? RoleName.Length > 0 : false));
    }
}
