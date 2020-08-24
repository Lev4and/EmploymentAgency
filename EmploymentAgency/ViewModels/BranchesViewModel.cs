using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class BranchesViewModel : BindableBase
    {
        private int? _selectedIdIndustry;
        private int? _selectedIdSubIndustry;
        private int? _selectedIdCountry;
        private int? _selectedIdCity;

        private string _industryName;
        private string _nameSubIndustry;
        private string _countryName;
        private string _cityName;
        private string _streetName;
        private string _organizationName;

        private QueryExecutor _executor;

        public int? SelectedIdBranch { get; set; }

        public int? SelectedIdIndustry
        {
            get { return _selectedIdIndustry; }
            set
            {
                _selectedIdIndustry = value;

                if (_selectedIdIndustry != null)
                {
                    UpdateSubIndustries();
                    UpdateDisplayedSubIndustries();
                }
                else
                    SubIndustries = null;

                UpdateDisplayedOrganizations();
            }
        }

        public int? SelectedIdSubIndustry
        {
            get { return _selectedIdSubIndustry; }
            set
            {
                _selectedIdSubIndustry = value;

                UpdateDisplayedOrganizations();
            }
        }

        public int? SelectedIdCountry
        {
            get { return _selectedIdCountry; }
            set
            {
                _selectedIdCountry = value;

                if (_selectedIdCountry != null)
                {
                    UpdateCities();
                    UpdateDisplayedCities();

                    Streets = null;
                }
                else
                {
                    Cities = null;
                    DisplayedCities = null;
                }
            }
        }

        public int? SelectedIdCity
        {
            get { return _selectedIdCity; }
            set
            {
                _selectedIdCity = value;

                if (_selectedIdCity != null)
                {
                    UpdateStreets();
                    UpdateDisplayedStreets();
                }
                else
                {
                    Streets = null;
                    DisplayedStreets = null;
                }
            }
        }

        public int? SelectedIdStreet { get; set; }

        public int? SelectedIdOrganization { get; set; }

        public string IndustryName
        {
            get { return _industryName; }
            set
            {
                _industryName = value;

                if (_industryName != null)
                {
                    if (_industryName.Length == 0)
                    {
                        SelectedIdIndustry = null;
                        SelectedIdSubIndustry = null;
                        SelectedIdOrganization = null;

                        IndustryName = "";
                        NameSubIndustry = "";
                        OrganizationName = "";
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

                if (_nameSubIndustry != null)
                {
                    if (_nameSubIndustry.Length == 0)
                    {
                        SelectedIdSubIndustry = null;
                        SelectedIdOrganization = null;

                        OrganizationName = "";
                    }
                }

                if(SubIndustries != null)
                {
                    UpdateDisplayedSubIndustries();
                }
            }
        }

        public string CountryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;

                if (_countryName != null)
                {
                    if (_countryName.Length == 0)
                    {
                        SelectedIdCountry = null;
                        SelectedIdCity = null;
                        SelectedIdStreet = null;

                        CityName = "";
                        StreetName = "";
                    }
                }

                if (Countries != null)
                {
                    UpdateDisplayedCountries();
                }
            }
        }

        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;

                if (_cityName != null)
                {
                    if (_cityName.Length == 0)
                    {
                        SelectedIdCity = null;
                        SelectedIdStreet = null;

                        StreetName = "";
                    }
                }

                if (Cities != null)
                {
                    UpdateDisplayedCities();
                }
            }
        }

        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;

                if (_streetName != null)
                {
                    if (_streetName.Length == 0)
                    {
                        SelectedIdStreet = null;

                        StreetName = "";
                    }
                }

                if (Streets != null)
                {
                    UpdateDisplayedStreets();
                }
            }
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;

                if (Organizations != null)
                    UpdateDisplayedOrganizations();
            }
        }

        public ObservableCollection<object> Branches { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<Industry> DisplayedIndustries { get; set; }

        public ObservableCollection<SubIndustry> SubIndustries { get; set; }

        public ObservableCollection<SubIndustry> DisplayedSubIndustries { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddBranch());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeBranchViewModel.SelectedIdBranch = (int)SelectedIdBranch;

            WindowService.ShowWindow(new ChangeBranch());
        }, () => SelectedIdBranch != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveBranch((int)SelectedIdBranch);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdBranch != null ? true : false);

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
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            IndustryName = "";
            NameSubIndustry = "";
            OrganizationName = "";
            CityName = "";

            SubIndustries = new ObservableCollection<SubIndustry>();
            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            CountryName = "";

            UpdateOrganizations();
            UpdateDisplayedOrganizations();

            Find();
        }

        private void Find()
        {
            Branches = new ObservableCollection<object>(CollectionConverter<v_branch>.ConvertToObjectList(_executor.GetBranches(
                (SelectedIdIndustry != null ? (int)SelectedIdIndustry : -1),
                (SelectedIdSubIndustry != null ? (int)SelectedIdSubIndustry : -1),
                (SelectedIdCountry != null ? (int)SelectedIdCountry : -1),
                (SelectedIdCity != null ? (int)SelectedIdCity : -1),
                (SelectedIdStreet != null ? (int)SelectedIdStreet : -1),
                (SelectedIdOrganization != null ? (int)SelectedIdOrganization : -1), 
                OrganizationName)));
        }

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateStreets()
        {
            Streets = new ObservableCollection<Street>(_executor.GetStreets((int)SelectedIdCity));
        }

        private void UpdateSubIndustries()
        {
            SubIndustries = new ObservableCollection<SubIndustry>(_executor.GetSubIndustries((int)SelectedIdIndustry));
        }

        private void UpdateOrganizations()
        {
            Organizations = new ObservableCollection<v_organizationWithoutPhoto>(_executor.GetOrganizationsWithoutPhoto());
        }

        private void UpdateDisplayedIndustries()
        {
            DisplayedIndustries = new ObservableCollection<Industry>(Industries.Where(i => SearchAssistant.GetSearchAction(i.IndustryName, IndustryName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedSubIndustries()
        {
            DisplayedSubIndustries = new ObservableCollection<SubIndustry>(SubIndustries.Where(s => SearchAssistant.GetSearchAction(s.NameSubIndustry, NameSubIndustry, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => SearchAssistant.GetSearchAction(c.CountryName, CountryName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedCities()
        {
            DisplayedCities = new ObservableCollection<City>(Cities.Where(c => SearchAssistant.GetSearchAction(c.CityName, CityName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedStreets()
        {
            DisplayedStreets = new ObservableCollection<Street>(Streets.Where(s => SearchAssistant.GetSearchAction(s.StreetName, StreetName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedOrganizations()
        {
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o =>
                                                                                                             SearchAssistant.GetSearchAction(o.OrganizationName, OrganizationName, SearchAssistant.SearchType.AllCriteria).Invoke() &&
                                                                                                             (SelectedIdIndustry != null ? o.IdIndustry == (int)SelectedIdIndustry : true) &&
                                                                                                             (SelectedIdSubIndustry != null ? o.IdSubIndustry == (int)SelectedIdSubIndustry : true)).Take(15).ToList());
        }
    }
}
