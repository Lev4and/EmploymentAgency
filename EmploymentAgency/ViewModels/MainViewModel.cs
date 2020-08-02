using DevExpress.Mvvm;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public MainViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Authorization => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Authorization());
        });

        public ICommand Registration => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Registration());
        });

        public ICommand Settings => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Settings());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
