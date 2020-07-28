using DevExpress.Mvvm;
using EmploymentAgency.Commands;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public bool IsBackgroundTaskRunning { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public AuthorizationViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            IsBackgroundTaskRunning = false;
        });

        public ICommand PasswordChanged => new Command((obj) =>
        {
            if (obj != null)
                Password = (obj as PasswordBox).Password;
        });

        public ICommand Authorization => new DelegateCommand(() =>
        {
            AuthorizationAsync();
        }, () => (Login != null ? Login.Length > 0 : false) && (Password != null ? Password.Length > 0 : false) && IsBackgroundTaskRunning == false);

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Main());
        });

        private void RewriteConfiguration(v_user user)
        {
            var config = new ConfigurationUser(user.IdUser, user.Password);
            config.Save();
        }

        private async void AuthorizationAsync()
        {
            await Task.Run(() =>
            {
                IsBackgroundTaskRunning = true;

                try
                {
                    v_user user = null;

                    if (_executor.CorrectDataUser(Login, Password, out user))
                    {
                        RewriteConfiguration(user);

                        MessageBox.Show("Успех");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                catch
                {
                    MessageBox.Show("Сервер базы данных отключен или указан неверный адрес сервера", "Ошибка");
                }

                IsBackgroundTaskRunning = false;
            });
        }
    }
}
