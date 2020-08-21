using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class VacanciesViewModel : BindableBase
    {
        private int? _selectedIdProfessionCategory;
        private int? _selectedIdCountry;
        private int? _selectedIdCity;

        private string _countryName;
        private string _cityName;
        private string _streetName;

        private QueryExecutor _executor;

        public int? BeginValueSalary { get; set; }

        public int? EndValueSalary { get; set; }

        public int? MaxValueSalary { get; set; }

        public int? MinValueSalary { get; set; }

        public int? SelectedIdProfessionCategory
        {
            get { return _selectedIdProfessionCategory; }
            set
            {
                _selectedIdProfessionCategory = value;

                if (_selectedIdProfessionCategory != null)
                    UpdateProfessions();
                else
                    Professions = null;
            }
        }

        public int? SelectedIdProfession { get; set; }

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

        public int? SelectedIdVacancy { get; set; }

        public string ProfessionName { get; set; }

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

        public DateTime BeginValueDateOfRegistration { get; set; }

        public DateTime EndValueDateOfRegistration { get; set; }

        public DateTime MaxValueDateOfRegistration { get; set; }

        public DateTime MinValueDateOfRegistration { get; set; }

        public DateTime? BeginValueClosingDate { get; set; }

        public DateTime? EndValueClosingDate { get; set; }

        public DateTime? MaxValueClosingDate { get; set; }

        public DateTime? MinValueClosingDate { get; set; }

        public ObservableCollection<object> Experiences { get; set; }

        public ObservableCollection<object> SelectedExperiences { get; set; }

        public ObservableCollection<object> Schedules { get; set; }

        public ObservableCollection<object> SelectedSchedules { get; set; }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public ObservableCollection<object> SelectedEmploymentTypes { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ObservableCollection<object> Vacancies { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

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
        });

        public ICommand Show => new DelegateCommand(() =>
        {
            ShowVacancyViewModel.SelectedIdVacancy = (int)SelectedIdVacancy;

            WindowService.ShowWindow(new ShowVacancy());
        }, () => SelectedIdVacancy != null);

        public ICommand Copy => new DelegateCommand(() =>
        {
            Clipboard.SetDataObject(Vacancies.First(v => v.GetType().GetProperty("IdVacancy").GetValue(v).ToString() == SelectedIdVacancy.ToString()));
        }, () => SelectedIdVacancy != null);

        private void ResetToDefault()
        {
            SelectedIdVacancy = null;
            SelectedIdProfessionCategory = null;
            SelectedIdProfession = null;
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            MinValueClosingDate = _executor.GetMinClosingDateVacancy();
            MinValueDateOfRegistration = _executor.GetMinDateOfRegistrationVacancy();
            MinValueSalary = _executor.GetMinSalaryVacancy();

            MaxValueClosingDate = _executor.GetMaxClosingDateVacancy();
            MaxValueDateOfRegistration = _executor.GetMaxDateOfRegistrationVacancy();
            MaxValueSalary = _executor.GetMaxSalaryVacancy();

            BeginValueClosingDate = MinValueClosingDate;
            BeginValueDateOfRegistration = MinValueDateOfRegistration;
            BeginValueSalary = MinValueSalary;

            EndValueClosingDate = MaxValueClosingDate;
            EndValueDateOfRegistration = MaxValueDateOfRegistration;
            EndValueSalary = MaxValueSalary;

            ProfessionName = "";
            CityName = "";

            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            SelectedExperiences = new ObservableCollection<object>();
            SelectedSchedules = new ObservableCollection<object>();
            SelectedEmploymentTypes = new ObservableCollection<object>();
            SelectedSkills = new ObservableCollection<object>();

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());
            Experiences = new ObservableCollection<object>(CollectionConverter<Experience>.ConvertToObjectList(_executor.GetExperiences()));
            Schedules = new ObservableCollection<object>(CollectionConverter<Schedule>.ConvertToObjectList(_executor.GetSchedules()));
            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes()));
            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));
            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            CountryName = "";

            Find();
        }

        private void Find()
        {
            Vacancies = new ObservableCollection<object>(CollectionConverter<v_vacancy>.ConvertToObjectList(_executor.GetVacancies(
                (SelectedIdProfessionCategory != null ? (int)SelectedIdProfessionCategory : -1),
                (SelectedIdProfession != null ? (int)SelectedIdProfession : -1),
                (SelectedIdCountry != null ? (int)SelectedIdCountry : -1), 
                (SelectedIdCity != null ? (int)SelectedIdCity : -1), 
                (SelectedIdStreet != null ? (int)SelectedIdStreet : -1), ProfessionName, SelectedExperiences.ToList(),
                SelectedSchedules.ToList(), SelectedEmploymentTypes.ToList(), SelectedSkills.ToList(), new Model.Logic.Range<DateTime>(BeginValueDateOfRegistration, EndValueDateOfRegistration),
                BeginValueClosingDate, EndValueClosingDate, BeginValueSalary, EndValueSalary)));
        }

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateStreets()
        {
            Streets = new ObservableCollection<Street>(_executor.GetStreets((int)SelectedIdCity));
        }

        private void UpdateDisplayedCountries()
        {
            DisplayedCountries = new ObservableCollection<Country>(Countries.Where(c => c.CountryName.ToLower().StartsWith(CountryName.ToLower())).Take(15).ToList());
        }

        private void UpdateDisplayedCities()
        {
            DisplayedCities = new ObservableCollection<City>(Cities.Where(c => c.CityName.ToLower().StartsWith(CityName.ToLower())).Take(15).ToList());
        }

        private void UpdateDisplayedStreets()
        {
            DisplayedStreets = new ObservableCollection<Street>(Streets.Where(s => s.StreetName.ToLower().StartsWith(StreetName.ToLower())).Take(15).ToList());
        }
    }
}
