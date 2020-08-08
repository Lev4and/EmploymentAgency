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
    public class CountriesViewModel : BindableBase
    {
        private int? _selectedIdCountry;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdCountry
        {
            get { return _selectedIdCountry; }
            set
            {
                _selectedIdCountry = value;

                IsCanChange = _selectedIdCountry != null ? true : false;
                IsCanRemove = _selectedIdCountry != null ? true : false;
            }
        }

        public string CountryName { get; set; }

        public ObservableCollection<object> Countries { get; set; }

        public CountriesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Countries = new ObservableCollection<object>(CollectionConverter<Country>.ConvertToObjectList(_executor.GetCountries()));

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddCountry());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeCountryViewModel.SelectedIdCountry = (int)SelectedIdCountry;

            WindowService.ShowWindow(new ChangeCountry());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveCountry((int)SelectedIdCountry);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdCountry = null;

            CountryName = "";

            Find();
        }

        private void Find()
        {
            Countries = new ObservableCollection<object>(CollectionConverter<Country>.ConvertToObjectList(_executor.GetCountries(CountryName)));
        }
    }
}
