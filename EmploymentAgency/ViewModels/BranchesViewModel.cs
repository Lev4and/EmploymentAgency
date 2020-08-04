using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Linq;
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

        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdBranch { get; set; }

        public int? SelectedIdIndustry
        {
            get { return _selectedIdIndustry; }
            set
            {
                _selectedIdIndustry = value;

                if (_selectedIdIndustry != null)
                    UpdateSubIndustries();
                else
                    SubIndustries = null;
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

        public ObservableCollection<SubIndustry> SubIndustries { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public BranchesViewModel(PageService pageService)
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

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => (OrganizationName != null ? OrganizationName.Length > 0 : false));

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu());
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

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());

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
                (SelectedIdOrganization != null ? (int)SelectedIdOrganization : -1))));
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

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => c.CountryName.ToLower().StartsWith(CountryName.ToLower())).Take(100).ToList());
        }

        private void UpdateDisplayedCities()
        {
            DisplayedCities = new ObservableCollection<City>(Cities.Where(c => c.CityName.ToLower().StartsWith(CityName.ToLower())).Take(100).ToList());
        }

        private void UpdateDisplayedStreets()
        {
            DisplayedStreets = new ObservableCollection<Street>(Streets.Where(s => s.StreetName.ToLower().StartsWith(StreetName.ToLower())).Take(100).ToList());
        }

        private void UpdateDisplayedOrganizations()
        {
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o =>
                                                                                                             (OrganizationName.Length > 0 ? o.OrganizationName.ToLower().StartsWith(OrganizationName.ToLower()) : true) &&
                                                                                                             (SelectedIdSubIndustry != null ? o.IdSubIndustry == (int)SelectedIdSubIndustry : true)).Take(100).ToList());
        }
    }
}
