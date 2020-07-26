using DevExpress.Mvvm;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public Page PageSource { get; set; }

        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;
            _pageService.ChangePage(new Main());
        }
    }
}
