using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly MenuPageService _menuPageService;
        private ConfigurationUser _config;

        public bool IsLeftDrawerOpen { get; set; }

        public Page PageSource { get; set; }

        public MenuViewModel(MenuPageService menuPageService)
        {
            _menuPageService = menuPageService;
            _menuPageService.OnPageChanged += (page) => { IsLeftDrawerOpen = false; PageSource = page; };

            _config = ConfigurationUser.GetConfiguration();
        }

        public ICommand OnUnchecked => new DelegateCommand(() =>
        {
            IsLeftDrawerOpen = false;
        });

        public ICommand Countries => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Countries());
        }, () => _config.RoleName == "Администратор");

        public ICommand Cities => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Cities());
        }, () => _config.RoleName == "Администратор");

        public ICommand Streets => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Streets());
        }, () => _config.RoleName == "Администратор");

        public ICommand Skills => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Skills());
        }, () => _config.RoleName == "Администратор");

        public ICommand DrivingLicenseCategories => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new DrivingLicenseCategories());
        }, () => _config.RoleName == "Администратор");

        public ICommand Educations => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Educations());
        }, () => _config.RoleName == "Администратор");

        public ICommand EmploymentTypes => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new EmploymentTypes());
        }, () => _config.RoleName == "Администратор");

        public ICommand Experiences => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Experiences());
        }, () => _config.RoleName == "Администратор");

        public ICommand Languages => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Languages());
        }, () => _config.RoleName == "Администратор");

        public ICommand LanguageProficiencies => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new LanguageProficiencies());
        }, () => _config.RoleName == "Администратор");

        public ICommand Genders => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Genders());
        }, () => _config.RoleName == "Администратор");

        public ICommand Industries => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Industries());
        }, () => _config.RoleName == "Администратор");

        public ICommand SubIndustries => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new SubIndustries());
        }, () => _config.RoleName == "Администратор");

        public ICommand ProfessionCategories => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new ProfessionCategories());
        }, () => _config.RoleName == "Администратор");

        public ICommand Professions => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Professions());
        }, () => _config.RoleName == "Администратор");

        public ICommand RequestStatuses => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new RequestStatuses());
        }, () => _config.RoleName == "Администратор");

        public ICommand Organizations => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Organizations());
        }, () => _config.RoleName == "Администратор");

        public ICommand Branches => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Branches());
        }, () => _config.RoleName == "Администратор");

        public ICommand Roles => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Roles());
        }, () => _config.RoleName == "Администратор");

        public ICommand Schedules => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Schedules());
        }, () => _config.RoleName == "Администратор");

        public ICommand Users => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Users());
        }, () => _config.RoleName == "Администратор");

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
