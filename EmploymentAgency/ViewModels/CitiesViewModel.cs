﻿using Converters;
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
    public class CitiesViewModel : BindableBase
    {
        private int? _selectedIdCity;

        private string _countryName;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdCountry { get; set; }

        public int? SelectedIdCity
        {
            get { return _selectedIdCity; }
            set
            {
                _selectedIdCity = value;

                IsCanChange = _selectedIdCity != null ? true : false;
                IsCanRemove = _selectedIdCity != null ? true : false;
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

        public CitiesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Countries = new ObservableCollection<Country>(_executor.GetCountries());
            Cities = new ObservableCollection<object>(CollectionConverter<v_city>.ConvertToObjectList(_executor.GetCities()));

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddCity());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeCityViewModel.SelectedIdCity = (int)SelectedIdCity;

            WindowService.ShowWindow(new ChangeCity());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveCity((int)SelectedIdCity);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
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
