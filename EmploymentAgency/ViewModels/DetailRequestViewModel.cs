using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Model.Configurations;
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
    public class DetailRequestViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_request _request;

        public static int SelectedIdRequest { get; set; }


        public bool IsRequestConsidered { get; set; }

        public int? Age { get; set; }

        public int? Salary { get; set; }

        public int? SelectedIdVacancy { get; set; }

        public int? SelectedIdEmploymentRequest { get; set; }

        public string ApplicantFullName { get; set; }

        public string AddressApplicant { get; set; }

        public string GenderName { get; set; }

        public string PhoneNumber { get; set; }

        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public string ExperienceName { get; set; }

        public string AboutMe { get; set; }

        public string RequestStatusName { get; set; }

        public byte[] Photo { get; set; }

        public ObservableCollection<object> PreferredEmploymentTypes { get; set; }

        public ObservableCollection<object> PreferredSchedules { get; set; }

        public ObservableCollection<object> PossessionSkills { get; set; }

        public ObservableCollection<object> PossessionDrivingLicenseCategories { get; set; }

        public ObservableCollection<object> Vacancies { get; set; }

        public ObservableCollection<object> Collection { get; set; }

        public ObservableCollection<object> SentEmploymentRequests { get; set; }

        public ObservableCollection<EmploymentType> EmploymentTypes { get; set; }

        public ObservableCollection<Schedule> Schedules { get; set; }

        public ObservableCollection<KnowledgeLanguage> KnowledgeLanguages { get; set; }

        public ObservableCollection<EducationalActivity> EducationActivities { get; set; }

        public ObservableCollection<LaborActivity> LaborActivities { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _request = _executor.GetRequestExtendedInformation(SelectedIdRequest);

            IsRequestConsidered = _executor.IsRequestConsidered(_request.IdRequest);

            Age = _request.Age;
            Salary = _request.Salary;
            ApplicantFullName = _request.ApplicantFullName;
            AddressApplicant = _request.AddressApplicant;
            GenderName = _request.GenderName;
            PhoneNumber = _request.PhoneNumber;
            NameProfessionCategory = _request.NameProfessionCategory;
            ProfessionName = _request.ProfessionName;
            ExperienceName = _request.ExperienceName;
            AboutMe = _request.AboutMe;
            RequestStatusName = _request.RequestStatusName;
            Photo = _request.Photo;

            Vacancies = new ObservableCollection<object>();

            if (IsRequestConsidered)
                Collection = new ObservableCollection<object>(GetSuitableVacancies());

            SentEmploymentRequests = UpdateSentEmploymentRequests();

            EmploymentTypes = new ObservableCollection<EmploymentType>(_executor.GetEmploymentTypes());
            Schedules = new ObservableCollection<Schedule>(_executor.GetSchedules());

            PreferredEmploymentTypes = GetPreferredEmploymentTypes();
            PreferredSchedules = GetPreferredSchedules();

            PossessionSkills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills(_request.IdApplicant)));
            PossessionDrivingLicenseCategories = new ObservableCollection<object>(CollectionConverter<DrivingLicenseCategory>.ConvertToObjectList(_executor.GetDrivingLicenseCategories(_request.IdApplicant)));
            EducationActivities = new ObservableCollection<EducationalActivity>(_executor.GetEducationActivities(_request.IdApplicant));
            KnowledgeLanguages = new ObservableCollection<KnowledgeLanguage>(_executor.GetKnowledgeLanguages(_request.IdApplicant));
            LaborActivities = new ObservableCollection<LaborActivity>(_executor.GetLaborActivities(_request.IdApplicant));
        });

        public ICommand Show => new DelegateCommand(() =>
        {
            ShowVacancyViewModel.SelectedIdVacancy = (int)SelectedIdVacancy;

            WindowService.ShowWindow(new ShowVacancy());
        }, () => SelectedIdVacancy != null);

        public ICommand EmploymentRequest => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите отправить запрос на трудоустройство?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(_executor.AddEmploymentRequest(GetIdSuitableVacancy()))
                {
                    MessageBox.Show("Успешная отправка заявки");

                    SentEmploymentRequests = UpdateSentEmploymentRequests();
                }
                else
                {
                    MessageBox.Show("Вы уже отправляли запрос с такими данными");
                }
            }
        }, () => SelectedIdVacancy != null);

        public ICommand ShowDetailVacancy => new DelegateCommand(() =>
        {
            ShowVacancyViewModel.SelectedIdVacancy = _executor.GetEmploymentRequest((int)SelectedIdEmploymentRequest).SuitableVacancy.Vacancy.IdVacancy;

            WindowService.ShowWindow(new ShowVacancy());
        }, () => SelectedIdEmploymentRequest != null);

        public ICommand SignContract => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите заключить договор?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(_executor.GetEmploymentRequest((int)SelectedIdEmploymentRequest).IsSuitable == true)
                {
                    if (_executor.AddContract((int)SelectedIdEmploymentRequest))
                    {
                        MessageBox.Show("Успешное заключение договора");
                    }
                    else
                    {
                        MessageBox.Show("Договор с такими данными уже был заключен");
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя заключить договор так как работодатель отклонил ваш запрос или он не был рассмотрен");
                }
            }    
        }, () => SelectedIdEmploymentRequest != null);

        private int GetIdSuitableVacancy()
        {
            var suitableVacancies = _executor.GetSuitableVacancies(SelectedIdRequest);

            return suitableVacancies.SingleOrDefault(s => s.IdVacancy == (int)SelectedIdVacancy).IdSuitableVacancy;
        }

        private ObservableCollection<object> UpdateSentEmploymentRequests()
        {
            return new ObservableCollection<object>(_executor.GetSentEmploymentRequests(SelectedIdRequest));
        }

        private ObservableCollection<object> GetSuitableVacancies()
        {
            Vacancies = new ObservableCollection<object>();

            var suitableVacancies = _executor.GetSuitableVacancies(SelectedIdRequest);

            foreach (var suitableVacancy in suitableVacancies)
            {
                Vacancies.Add(((object)_executor.GetVacancyExtendedInformation(suitableVacancy.IdVacancy)));
            }

            return Vacancies;
        }

        private ObservableCollection<object> GetPreferredEmploymentTypes()
        {
            var result = new ObservableCollection<object>();
            var preferredEmploymentTypes = _executor.GetPreferredEmploymentTypes(SelectedIdRequest);

            EmploymentTypes.ForEach(e =>
            {
                if (preferredEmploymentTypes.Any(p => p.IdEmploymentType == e.IdEmploymentType))
                    result.Add(e);
            });

            return result;
        }

        private ObservableCollection<object> GetPreferredSchedules()
        {
            var result = new ObservableCollection<object>();
            var preferredSchedules = _executor.GetPreferredSchedules(SelectedIdRequest);

            Schedules.ForEach(s =>
            {
                if (preferredSchedules.Any(p => p.IdSchedule == s.IdSchedule))
                    result.Add(s);
            });

            return result;
        }
    }
}
