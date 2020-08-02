using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SupplementingInformationForApplicantViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private ConfigurationUser _config;
        private QueryExecutor _executor;

        public int? SelectedIdGender { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

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

        public SupplementingInformationForApplicantViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationUser.GetConfiguration();
            _executor = new QueryExecutor();

            SelectedIdGender = null;

            Name = "";
            Surname = "";
            Patronymic = "";
            PhoneNumber = "";

            Photo = null;

            MinValueDateOfBirth = new DateTime(1900, 1, 1);
            MaxValueDateOfBirth = DateTime.Now.Date.AddYears(-18);

            DateOfBirth = MaxValueDateOfBirth;

            SelectedSkills = new ObservableCollection<object>();
            SelectedDrivingLicenseCategories = new ObservableCollection<object>();

            Genders = CollectionConverter<Gender>.ConvertToObservableCollection(_executor.GetGenders());
            Skills = CollectionConverter<object>.ConvertToObservableCollection(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));
            DrivingLicenseCategories = CollectionConverter<object>.ConvertToObservableCollection(CollectionConverter<DrivingLicenseCategory>.ConvertToObjectList(_executor.GetDrivingLicenseCategories()));
        });

        public ICommand AddInformation => new DelegateCommand(() =>
        {
            if (_executor.AddApplicant(_config.IdUser, Name, Surname, Patronymic, (int)SelectedIdGender, Photo, DateOfBirth, PhoneNumber))
            {
                AddPossessionSkill();
                AddPossessionDrivingLicenseCategory();
                AddEducationalActivity();
                AddKnowledgeLanguage();
                AddLaborActivity();

                MessageBox.Show("Успешное добавление информации");
            }
            else
            {
                MessageBox.Show("Информация о данном пользователе уже существует");
            }
        }, () => SelectedIdGender != null &&
                 (Name != null ? Name.Length > 0 : false) &&
                 (Surname != null ? Surname.Length > 0 : false) &&
                 (Patronymic != null ? Patronymic.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));

        public ICommand AddPhoto => new DelegateCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.jpeg, *.png, *webp)|*.bmp;*.jpg;*.jpeg;*.png;*.webp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Photo = FileConverter.GetBytesFromFile(openFileDialog.FileName);
            }
        }, () => Photo == null);

        public ICommand RemovePhoto => new DelegateCommand(() =>
        {
            Photo = null;
        }, () => Photo != null);

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
