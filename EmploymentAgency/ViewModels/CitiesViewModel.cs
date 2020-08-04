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
    public class CitiesViewModel : BindableBase
    {
        private string _countryName;

        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdCountry { get; set; }

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
                    }
                }

                if (Countries != null)
                {
                    UpdateDisplayedCountries();
                }
            }
        }

        public string CityName { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<object> Cities { get; set; }

        public CitiesViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Countries = new ObservableCollection<Country>(_executor.GetCountries());
            Cities = new ObservableCollection<object>(CollectionConverter<v_city>.ConvertToObjectList(_executor.GetCities()));

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

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdCountry != null || (CityName != null ? CityName.Length > 0 : false));

        private void ResetToDefault()
        {
            SelectedIdCountry = null;
            SelectedIdCity = null;

            CountryName = "";
            CityName = "";

            Find();
        }

        private void Find()
        {
            Cities = new ObservableCollection<object>(CollectionConverter<v_city>.ConvertToObjectList(_executor.GetCities(
                (SelectedIdCountry != null ? (int)SelectedIdCountry : -1), 
                CityName)));
        }

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => c.CountryName.ToLower().StartsWith(CountryName.ToLower())).Take(100).ToList());
        }
    }
}
