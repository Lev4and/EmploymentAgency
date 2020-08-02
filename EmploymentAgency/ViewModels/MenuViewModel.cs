using DevExpress.Mvvm;
using EmploymentAgency.Services;

namespace EmploymentAgency.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private readonly PageService _pageService;

        public MenuViewModel(PageService pageService)
        {
            _pageService = pageService;
        }
    }
}
