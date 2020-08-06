using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private ConfigurationUser _config;

        public MenuViewModel(PageService pageService)
        {
            _pageService = pageService;
            _config = ConfigurationUser.GetConfiguration();
        }

        public ICommand Countries => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Countries());
        });

        public ICommand Cities => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Cities());
        });

        public ICommand Streets => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Streets());
        });

        public ICommand Skills => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Skills());
        });

        public ICommand DrivingLicenseCategories => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new DrivingLicenseCategories());
        });

        public ICommand Educations => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Educations());
        });

        public ICommand EmploymentTypes => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new EmploymentTypes());
        });

        public ICommand Experiences => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Experiences());
        });

        public ICommand Organizations => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Organizations());
        });

        public ICommand Branches => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Branches());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
