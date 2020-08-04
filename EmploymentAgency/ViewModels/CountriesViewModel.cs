﻿using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class CountriesViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdCountry { get; set; }

        public string CountryName { get; set; }

        public ObservableCollection<object> Countries { get; set; }

        public CountriesViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Countries = new ObservableCollection<object>(CollectionConverter<Country>.ConvertToObjectList(_executor.GetCountries()));

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