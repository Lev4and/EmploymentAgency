using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeUserViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_user _user;

        public static int SelectedIdUser { get; set; }


        public string RoleName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _user = _executor.GetUserExtendedInformation(SelectedIdUser);

            RoleName = _user.RoleName;
            Login = _user.Login;
            Password = _user.Password;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateUser(_user.IdUser, Login, Password))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными уже существует");
            }
        }, () => (Login != null ? Login.Length > 0 : false) &&
                 (Password != null ? Password.Length > 0 : false));
    }
}
