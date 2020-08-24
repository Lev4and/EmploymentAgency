using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddBranchViewModel : BindableBase
    {
        private int? _selectedIdCountry;
        private int? _selectedIdCity;

        private string _countryName;
        private string _cityName;
        private string _streetName;
        private string _organizationName;

        private QueryExecutor _executor;

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

        public string NameHouse { get; set; }

        public string PhoneNumber { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SelectedIdOrganization = null;
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            OrganizationName = "";
            CityName = "";
            NameHouse = "";
            PhoneNumber = "";

            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            CountryName = "";

            UpdateOrganizations();
            UpdateDisplayedOrganizations();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddBranch((int)SelectedIdOrganization, (int)SelectedIdStreet, NameHouse, PhoneNumber))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Филиал организации с такими данными уже существует");
            }
        }, () => SelectedIdStreet != null &&
                 SelectedIdOrganization != null &&
                 (NameHouse != null ? NameHouse.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateStreets()
        {
            Streets = new ObservableCollection<Street>(_executor.GetStreets((int)SelectedIdCity));
        }

        private void UpdateOrganizations()
        {
            Organizations = new ObservableCollection<v_organizationWithoutPhoto>(_executor.GetOrganizationsWithoutPhoto());
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
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o => SearchAssistant.GetSearchAction(o.OrganizationName, OrganizationName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }
    }
}
