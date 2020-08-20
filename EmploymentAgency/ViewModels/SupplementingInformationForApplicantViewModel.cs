using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SupplementingInformationForApplicantViewModel : BindableBase
    {
        private int? _selectedIdCountry;
        private int? _selectedIdCity;

        private string _countryName;
        private string _cityName;
        private string _streetName;

        private readonly PageService _pageService;
        private ConfigurationUser _config;
        private QueryExecutor _executor;

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

        public DateTime MinValueDateOfBirth { get; set; }

        public DateTime MaxValueDateOfBirth { get; set; }

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

        public SupplementingInformationForApplicantViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationUser.GetConfiguration();
            _executor = new QueryExecutor();

            SelectedIdGender = null;
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            Name = "";
            Surname = "";
            Patronymic = "";
            PhoneNumber = "";
            CityName = "";
            NameHouse = "";
            Apartment = "";

            Photo = null;

            MinValueDateOfBirth = new DateTime(1900, 1, 1);
            MaxValueDateOfBirth = DateTime.Now.Date.AddYears(-18);

            DateOfBirth = MaxValueDateOfBirth;

            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            SelectedSkills = new ObservableCollection<object>();
            SelectedDrivingLicenseCategories = new ObservableCollection<object>();
            SelectedEducationActivities = new ObservableCollection<EducationalActivity>();
            SelectedKnowledgeLanguages = new ObservableCollection<KnowledgeLanguage>();
            SelectedLaborActivities = new ObservableCollection<LaborActivity>();

            Genders = new ObservableCollection<Gender>(_executor.GetGenders());
            Skills = new ObservableCollection<object>(_executor.GetSkills());
            DrivingLicenseCategories = new ObservableCollection<object>(_executor.GetDrivingLicenseCategories());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            CountryName = "";
        });

        public ICommand AddInformation => new DelegateCommand(() =>
        {
            if (_executor.AddApplicant(_config.IdUser, Name, Surname, Patronymic, (int)SelectedIdGender, Photo, DateOfBirth, PhoneNumber, (int)SelectedIdStreet, NameHouse, Apartment))
            {
                AddPossessionSkill();
                AddPossessionDrivingLicenseCategory();
                AddEducationalActivity();
                AddKnowledgeLanguage();
                AddLaborActivity();

                MessageBox.Show("Успешное добавление информации");

                _pageService.ChangePage(new Menu());
            }
            else
            {
                MessageBox.Show("Информация о данном пользователе уже существует");
            }
        }, () => SelectedIdGender != null &&
                 SelectedIdStreet != null &&
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

        private void AddPossessionSkill()
        {
            for(int i = 0; i < SelectedSkills.Count; i++)
            {
                _executor.AddPossessionSkill(_config.IdUser, Convert.ToInt32(SelectedSkills[i]));
            }
        }

        private void AddPossessionDrivingLicenseCategory()
        {
            for(int i = 0; i < SelectedDrivingLicenseCategories.Count; i++)
            {
                _executor.AddPossessionDrivingLicenseCategory(_config.IdUser, Convert.ToInt32(SelectedDrivingLicenseCategories[i]));
            }
        }

        private void AddEducationalActivity()
        {
            for(int i = 0; i < SelectedEducationActivities.Count; i++)
            {
                _executor.AddEducationalActivity(_config.IdUser, SelectedEducationActivities[i].IdEducation, SelectedEducationActivities[i].NameEducationalnstitution, SelectedEducationActivities[i].Address, SelectedEducationActivities[i].StartDate, SelectedEducationActivities[i].EndDate);
            }
        }

        private void AddKnowledgeLanguage()
        {
            for(int i = 0; i < SelectedKnowledgeLanguages.Count; i++)
            {
                _executor.AddKnowledgeLanguage(_config.IdUser, SelectedKnowledgeLanguages[i].IdLanguage, SelectedKnowledgeLanguages[i].IdLanguageProficiency);
            }
        }

        private void AddLaborActivity()
        {
            for(int i = 0; i < SelectedLaborActivities.Count; i++)
            {
                _executor.AddLaborActivity(_config.IdUser, SelectedLaborActivities[i].OrganizationName, SelectedLaborActivities[i].OrganizationAddress, SelectedLaborActivities[i].ProfessionName, SelectedLaborActivities[i].Activity, SelectedLaborActivities[i].StartDate, SelectedLaborActivities[i].EndDate);
            }
        }
    }
}
