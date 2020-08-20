using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddUserViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRole { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Roles = new ObservableCollection<Role>(_executor.GetRoles().Where(r =>
            r.RoleName == "Администратор" || r.RoleName == "Менеджер" || r.RoleName == "Владелец").ToList());
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddUser((int)SelectedIdRole, Login, Password))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными уже существует");
            }
        }, () => SelectedIdRole != null &&
                 (Login != null ? Login.Length > 0 : false) &&
                 (Password != null ? Password.Length > 0 : false));
    }
}
