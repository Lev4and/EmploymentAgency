using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddCityViewModel : BindableBase
    {
        private string _countryName;

        private QueryExecutor _executor;

        public int? SelectedIdCountry { get; set; }

        public string CityName { get; set; }

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

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            CountryName = "";

            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            UpdateDisplayedCountries();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(_executor.AddCity((int)SelectedIdCountry, CityName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Город с такими данными уже существует");
            }
        }, () => SelectedIdCountry != null &&
                 (CityName != null ? CityName.Length > 0 : false));

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => c.CountryName.ToLower().StartsWith(CountryName.ToLower())).Take(15).ToList());
        }
    }
}
