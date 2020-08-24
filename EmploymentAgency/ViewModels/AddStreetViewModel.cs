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
    public class AddStreetViewModel : BindableBase
    {
        private int? _selectedIdCountry;

        private string _countryName;
        private string _cityName;

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
                }
                else
                {
                    Cities = null;
                    DisplayedCities = null;
                }
            }
        }

        public int? SelectedIdCity { get; set; }

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

                        CityName = "";
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

                        StreetName = "";
                    }
                }

                if (Cities != null)
                {
                    UpdateDisplayedCities();
                }
            }
        }

        public string StreetName { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SelectedIdCountry = null;
            SelectedIdCity = null;

            CountryName = "";
            CityName = "";

            Cities = new ObservableCollection<City>();

            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            UpdateDisplayedCountries();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddStreet((int)SelectedIdCity, StreetName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Улица с такими данными уже существует");
            }
        }, () => SelectedIdCity != null &&
                 (StreetName != null ? StreetName.Length > 0 : false));

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => SearchAssistant.GetSearchAction(c.CountryName, CountryName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedCities()
        {
            DisplayedCities = new ObservableCollection<City>(Cities.Where(c => SearchAssistant.GetSearchAction(c.CityName, CityName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }
    }
}
