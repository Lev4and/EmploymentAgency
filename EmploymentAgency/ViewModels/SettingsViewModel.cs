using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private ConfigurationDatabase _config;

        public int CommandTimeout { get; set; }

        public string ServerAddress { get; set; }

        public string DatabaseName { get; set; }

        public SettingsViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationDatabase.GetConfiguration();

            CommandTimeout = _config.CommandTimeout;
            ServerAddress = _config.ServerAddress;
            DatabaseName = _config.DatabaseName;
        });

        public ICommand SaveChanges => new DelegateCommand(() =>
        {
            RewriteConfiguration();

            MessageBox.Show("Успешное сохранение");
        }, () => CommandTimeout >= 0 && (ServerAddress != null ? ServerAddress.Length > 0 : false) && (DatabaseName != null ? DatabaseName.Length > 0 : false));

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Main());
        });

        private void RewriteConfiguration()
        {
            var config = new ConfigurationDatabase(CommandTimeout, ServerAddress, DatabaseName);
            config.Save();
        }
    }
}
