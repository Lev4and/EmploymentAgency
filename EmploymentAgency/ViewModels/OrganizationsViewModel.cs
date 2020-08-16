using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class OrganizationsViewModel : BindableBase
    {
        private int? _selectedIdIndustry;

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

        public OrganizationsViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<object>(CollectionConverter<Industry>.ConvertToObjectList(_executor.GetIndustries()));

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddOrganization());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeOrganizationViewModel.SelectedIdOrganization = (int)SelectedIdOrganization;

            WindowService.ShowWindow(new ChangeOrganization());
        }, () => SelectedIdOrganization != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveOrganization((int)SelectedIdOrganization);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdOrganization != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdIndustry != null || SelectedIdSubIndustry != null || (OrganizationName != null ? OrganizationName.Length > 0 : false));

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
