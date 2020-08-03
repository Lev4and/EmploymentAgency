using DevExpress.Mvvm;
using EmploymentAgency.Commands;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic.Managers;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using EmploymentAgency.Views.Windows;
using System;
using System.Data.Entity.Core;
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

                        if (user.RoleName != "Администратор" && user.RoleName != "Владелец")
                        {
                            if (_executor.NecessaryToSupplementTheInformation(user.IdUser))
                            {
                                var pageManager = PageManager.GetPageManager(_executor.GetIdRole(user.RoleName));

                                Invoke(() => _pageService.ChangePage(pageManager.GetPage()));
                            }
                            else
                            {
                                Invoke(() => _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu()));
                            }
                        }
                        else
                        {
                            Invoke(() => _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu()));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка");
                    }
                }
                catch (EntityException)
                {
                    MessageBox.Show("Сервер базы данных отключен или указан неверный адрес сервера", "Ошибка");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка");
                }

                IsBackgroundTaskRunning = false;
            });
        }

        private void Invoke(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
