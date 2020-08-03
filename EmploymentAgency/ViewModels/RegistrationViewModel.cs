using DevExpress.Mvvm;
using EmploymentAgency.Commands;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class RegistrationViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdRole { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public RegistrationViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SelectedIdRole = null;

            Login = "";
            Password = "";
            RepeatPassword = "";

            Roles = new ObservableCollection<Role>(_executor.GetRoles().Where(r => 
            r.RoleName != "Администратор" && r.RoleName != "Менеджер" && r.RoleName != "Владелец"));
        });

        public ICommand PasswordChanged => new Command((obj) =>
        {
            if (obj != null)
                Password = (obj as PasswordBox).Password;
        });

        public ICommand RepeatPasswordChanged => new Command((obj) =>
        {
            if (obj != null)
                RepeatPassword = (obj as PasswordBox).Password;
        });

        public ICommand Registration => new DelegateCommand(() =>
        {
            if(_executor.AddUser((int)SelectedIdRole, Login, Password))
            {
                MessageBox.Show("Успешная регистрация");

                _pageService.ChangePage(new Authorization());
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует");
            }
        }, () => SelectedIdRole != null &&
                 (Login != null ? Login.Length > 0 : false) &&
                 (Password != null ? Password.Length > 0 : false) &&
                 (RepeatPassword != null ? RepeatPassword.Length > 0 && RepeatPassword == Password : false));

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Main());
        });
    }
}
