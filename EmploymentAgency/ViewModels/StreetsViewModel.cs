using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class StreetsViewModel : BindableBase
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

                    Streets = null;
                }
                else
                {
                    Cities = null;
                    DisplayedCities = null;
                }
            }
        }

        public int? SelectedIdCity { get; set; }

        public int? SelectedIdStreet { get; set; }

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

        public ObservableCollection<object> Streets { get; set; }

        public StreetsViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Streets = new ObservableCollection<object>(CollectionConverter<v_street>.ConvertToObjectList(_executor.GetStreets()));
            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddStreet());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeStreetViewModel.SelectedIdStreet = (int)SelectedIdStreet;

            WindowService.ShowWindow(new ChangeStreet());
        }, () => SelectedIdStreet != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveStreet((int)SelectedIdStreet);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdStreet != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdCountry != null || SelectedIdCity != null || (StreetName != null ? StreetName.Length > 0 : false));

        private void ResetToDefault()
        {
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            CountryName = "";
            CityName = "";
            StreetName = "";

            Find();
        }

        private void Find()
        {
            Streets = new ObservableCollection<object>(CollectionConverter<v_street>.ConvertToObjectList(_executor.GetStreets(
                (SelectedIdCountry != null ? (int)SelectedIdCountry : -1),
                (SelectedIdCity != null ? (int)SelectedIdCity : -1),
                StreetName)));
        }

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => c.CountryName.ToLower().StartsWith(CountryName.ToLower())).Take(100).ToList());
        }

        private void UpdateDisplayedCities()
        {
            DisplayedCities = new ObservableCollection<City>(Cities.Where(c => c.CityName.ToLower().StartsWith(CityName.ToLower())).Take(100).ToList());
        }
    }
}
