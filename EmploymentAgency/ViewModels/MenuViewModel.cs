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
