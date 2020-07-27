using DevExpress.Mvvm;
using EmploymentAgency.Services;
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

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
