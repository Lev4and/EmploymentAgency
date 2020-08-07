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
        });

        public ICommand Cities => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Cities());
        });

        public ICommand Streets => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Streets());
        });

        public ICommand Skills => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Skills());
        });

        public ICommand DrivingLicenseCategories => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new DrivingLicenseCategories());
        });

        public ICommand Educations => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Educations());
        });

        public ICommand EmploymentTypes => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new EmploymentTypes());
        });

        public ICommand Experiences => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Experiences());
        });

        public ICommand Languages => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Languages());
        });

        public ICommand LanguageProficiencies => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new LanguageProficiencies());
        });

        public ICommand Genders => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Genders());
        });

        public ICommand Industries => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Industries());
        });

        public ICommand SubIndustries => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new SubIndustries());
        });

        public ICommand ProfessionCategories => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new ProfessionCategories());
        });

        public ICommand Professions => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Professions());
        });

        public ICommand RequestStatuses => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new RequestStatuses());
        });

        public ICommand Organizations => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Organizations());
        });

        public ICommand Branches => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Branches());
        });

        public ICommand Roles => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Roles());
        });

        public ICommand Schedules => new DelegateCommand(() =>
        {
            _menuPageService.ChangePage(new Schedules());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
