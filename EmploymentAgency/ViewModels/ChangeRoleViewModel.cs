using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeRoleViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Role _role;

        public static int SelectedIdRole { get; set; }


        public string RoleName { get; set; }

        public ChangeRoleViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _role = _executor.GetRole(SelectedIdRole);

            RoleName = _role.RoleName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateRole(SelectedIdRole, RoleName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Должность с такими данными уже существует");
            }
        }, () => (RoleName != null ? RoleName.Length > 0 : false));
    }
}
