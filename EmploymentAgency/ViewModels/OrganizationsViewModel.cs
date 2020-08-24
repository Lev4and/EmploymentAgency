using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class OrganizationsViewModel : BindableBase
    {
        private int? _selectedIdIndustry;

        private string _industryName;
        private string _nameSubIndustry;

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
                    UpdateDisplayedSubIndustries();
                }
            }
        }

        public int? SelectedIdSubIndustry { get; set; }

        public string IndustryName
        {
            get { return _industryName; }
            set
            {
                _industryName = value;

                if(_industryName != null)
                {
                    if(_industryName.Length == 0)
                    {
                        SelectedIdIndustry = null;
                        SelectedIdSubIndustry = null;

                        NameSubIndustry = "";
                    }
                }

                if(Industries != null)
                {
                    UpdateDisplayedIndustries();
                }
            }
        }

        public string NameSubIndustry
        {
            get { return _nameSubIndustry; }
            set
            {
                _nameSubIndustry = value;

                if(_nameSubIndustry != null)
                {
                    if(_nameSubIndustry.Length == 0)
                    {
                        SelectedIdSubIndustry = null;
                    }
                }

                if(SubIndustries != null)
                {
                    UpdateDisplayedSubIndustries();
                }
            }
        }

        public string OrganizationName { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<Industry> DisplayedIndustries { get; set; }

        public ObservableCollection<SubIndustry> SubIndustries { get; set; }

        public ObservableCollection<SubIndustry> DisplayedSubIndustries { get; set; }

        public ObservableCollection<object> Organizations { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            IndustryName = "";
            NameSubIndustry = "";

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

            UpdateDisplayedIndustries();
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
        });

        private void ResetToDefault()
        {
            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;
            SelectedIdOrganization = null;

            IndustryName = "";
            NameSubIndustry = "";
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
            SubIndustries = new ObservableCollection<SubIndustry>(_executor.GetSubIndustries((int)SelectedIdIndustry));
        }

        private void UpdateDisplayedIndustries()
        {
            DisplayedIndustries = new ObservableCollection<Industry>(Industries.Where(i => i.IndustryName.ToLower().StartsWith(IndustryName.ToLower())).Take(20).ToList());
        }

        private void UpdateDisplayedSubIndustries()
        {
            DisplayedSubIndustries = new ObservableCollection<SubIndustry>(SubIndustries.Where(s => s.NameSubIndustry.ToLower().StartsWith(NameSubIndustry.ToLower())).Take(20).ToList());
        }
    }
}
