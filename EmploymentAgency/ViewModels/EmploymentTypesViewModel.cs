using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class EmploymentTypesViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdEmploymentType { get; set; }

        public string EmploymentTypeName { get; set; }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public EmploymentTypesViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu());
        });

        private void ResetToDefault()
        {
            SelectedIdEmploymentType = null;

            EmploymentTypeName = "";

            Find();
        }

        private void Find()
        {
            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes(EmploymentTypeName)));
        }
    }
}
