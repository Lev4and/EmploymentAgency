using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class OrganizationsViewModel : BindableBase
    {
        private int? _selectedIdIndustry;

        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdOrganization { get; set; }

        public int? SelectedIdIndustry
        {
            get { return _selectedIdIndustry; }
            set
            {
                _selectedIdIndustry = value;

                if(_selectedIdIndustry == null)
                {
                    SubIndustries = null;
                }
                else
                {
                    UpdateSubIndustries();
                }
            }
        }

        public int? SelectedIdSubIndustry { get; set; }

        public string OrganizationName { get; set; }

        public ObservableCollection<object> Industries { get; set; }

        public ObservableCollection<object> SubIndustries { get; set; }

        public ObservableCollection<object> Organizations { get; set; }

        public OrganizationsViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();

            Industries = new ObservableCollection<object>(CollectionConverter<Industry>.ConvertToObjectList(_executor.GetIndustries()));
        });

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdIndustry != null || SelectedIdSubIndustry != null || (OrganizationName != null ? OrganizationName.Length > 0 : false));

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu());
        });

        private void ResetToDefault()
        {
            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;
            SelectedIdOrganization = null;

            OrganizationName = "";

            Find();
        }

        private void Find()
        {
            Organizations = new ObservableCollection<object>(CollectionConverter<v_organization>.ConvertToObjectList(_executor.GetOrganizations(
                (SelectedIdIndustry != null ? (int)SelectedIdIndustry : -1), 
                (SelectedIdSubIndustry != null ? (int)SelectedIdSubIndustry : -1),
                OrganizationName)));
        }

        private void UpdateSubIndustries()
        {
            SubIndustries = new ObservableCollection<object>(CollectionConverter<SubIndustry>.ConvertToObjectList(_executor.GetSubIndustries((int)SelectedIdIndustry)));
        }
    }
}
