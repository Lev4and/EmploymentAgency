using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSupplementInformationForApplicantViewModel : BindableBase
    {
        private int? _selectedIdCountry;
        private int? _selectedIdCity;

        private string _countryName;
        private string _cityName;
        private string _streetName;

        private ConfigurationUser _config;
        private QueryExecutor _executor;
        private v_applicant _applicant;

        public int? SelectedIdGender { get; set; }

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

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

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

        public string NameHouse { get; set; }

        public string Apartment { get; set; }

        public byte[] Photo { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ObservableCollection<Gender> Genders { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ObservableCollection<object> SelectedDrivingLicenseCategories { get; set; }

        public ObservableCollection<object> DrivingLicenseCategories { get; set; }

        public ObservableCollection<KnowledgeLanguage> SelectedKnowledgeLanguages { get; set; }

        public ObservableCollection<EducationalActivity> SelectedEducationActivities { get; set; }

        public ObservableCollection<LaborActivity> SelectedLaborActivities { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<Country> DisplayedCountries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<City> DisplayedCities { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationUser.GetConfiguration();
            _executor = new QueryExecutor();

            _applicant = _executor.GetApplicantExtendedInformation(_config.IdUser);

            Name = _applicant.Name;
            Surname = _applicant.Surname;
            Patronymic = _applicant.Patronymic;
            PhoneNumber = _applicant.PhoneNumber;
            CountryName = _applicant.CountryName;
            CityName = _applicant.CityName;
            StreetName = _applicant.StreetName;
            NameHouse = _applicant.NameHouse;
            Apartment = _applicant.Apartment;

            SelectedIdGender = _applicant.IdGender;
            SelectedIdCountry = _applicant.IdCountry;
            SelectedIdCity = _applicant.IdCity;
            SelectedIdStreet = _applicant.IdStreet;

            Photo = _applicant.Photo;

            DateOfBirth = _applicant.DateOfBirth;

            SelectedSkills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills(_applicant.IdApplicant)));
            SelectedDrivingLicenseCategories = new ObservableCollection<object>(CollectionConverter<DrivingLicenseCategory>.ConvertToObjectList(_executor.GetDrivingLicenseCategories(_applicant.IdApplicant)));
            SelectedEducationActivities = new ObservableCollection<EducationalActivity>(_executor.GetEducationActivities(_applicant.IdApplicant));
            SelectedKnowledgeLanguages = new ObservableCollection<KnowledgeLanguage>(_executor.GetKnowledgeLanguages(_applicant.IdApplicant));
            SelectedLaborActivities = new ObservableCollection<LaborActivity>(_executor.GetLaborActivities(_applicant.IdApplicant));

            Genders = new ObservableCollection<Gender>(_executor.GetGenders());
            Skills = new ObservableCollection<object>(_executor.GetSkills());
            DrivingLicenseCategories = new ObservableCollection<object>(_executor.GetDrivingLicenseCategories());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());
        });

        public ICommand ChangeInformation => new DelegateCommand(() =>
        {
            _executor.UpdateApplicant(_applicant.IdApplicant, Name, Surname, Patronymic, Photo, PhoneNumber, (int)SelectedIdStreet, NameHouse, Apartment, SelectedSkills.ToList(), SelectedDrivingLicenseCategories.ToList(), SelectedEducationActivities.ToList(), SelectedKnowledgeLanguages.ToList(), SelectedLaborActivities.ToList());

            MessageBox.Show("Успешное изменение информации");
        }, () => SelectedIdStreet != null &&
                 (Name != null ? Name.Length > 0 : false) &&
                 (Surname != null ? Surname.Length > 0 : false) &&
                 (Patronymic != null ? Patronymic.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false) &&
                 (NameHouse != null ? NameHouse.Length > 0 : false));

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
    }
}
