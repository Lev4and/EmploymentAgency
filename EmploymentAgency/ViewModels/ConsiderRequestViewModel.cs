using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Model.Configurations;
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
    public class ConsiderRequestViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_request _request;

        public static int SelectedIdRequest { get; set; }


        public bool IsRequestConsidered { get; set; }

        public bool IsConsideration { get; set; }

        public int? Age { get; set; }

        public int? Salary { get; set; }

        public int? SelectedIdVacancy { get; set; }

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
            IsConsideration = false;

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

            if(IsRequestConsidered)
                Collection = new ObservableCollection<object>(GetSuitableVacancies());

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

        public ICommand Paste => new DelegateCommand(() =>
        {
            object idVacancy = Clipboard.GetDataObject().GetData(DataFormats.Text);
            object vacancy = _executor.GetVacancyExtendedInformation(Convert.ToInt32(idVacancy.ToString()));

            if (!Vacancies.Contains(vacancy))
                Vacancies.Add(vacancy);

            Collection = new ObservableCollection<object>(Vacancies);
        }, () => IsConsideration == true);

        public ICommand Show => new DelegateCommand(() =>
        {
            ShowVacancyViewModel.SelectedIdVacancy = (int)SelectedIdVacancy;

            WindowService.ShowWindow(new ShowVacancy());
        }, () => SelectedIdVacancy != null);

        public ICommand Remove => new DelegateCommand(() =>
        {
            object vacancy = _executor.GetVacancyExtendedInformation(Convert.ToInt32(SelectedIdVacancy.ToString()));

            if (Vacancies.Contains(vacancy))
                Vacancies.Remove(vacancy);

            Collection = new ObservableCollection<object>(Vacancies);

        }, () => SelectedIdVacancy != null && IsConsideration == true);

        public ICommand BeginConsider => new DelegateCommand(() =>
        {
            if(!_executor.IsRequestConsidered(SelectedIdRequest))
            {
                IsConsideration = true;

                _executor.UpdateStatusConsiderationRequest(SelectedIdRequest, "На рассмотрении");

                RequestStatusName = "На рассмотрении";
            }
            else
            {
                MessageBox.Show("Нельзя обрабатывать рассмотренную заявку");

                IsRequestConsidered = true;
            }
        }, () => IsRequestConsidered == false && IsConsideration == false);

        public ICommand EndConsider => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите завершить рассмотрение заявки?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                foreach (var item in Collection)
                    _executor.AddSuitableVacancy(SelectedIdRequest, _config.IdUser, Convert.ToInt32(item.GetType().GetProperty("IdVacancy").GetValue(item).ToString()));

                _executor.UpdateStatusConsiderationRequest(SelectedIdRequest, Collection.Count > 0 ? "Найдены подходящие вакансии" : "Подходящие вакансии не найдены");

                MessageBox.Show("Рассмотрение заявки завершена");

                IsConsideration = false;
                IsRequestConsidered = true;

                RequestStatusName = Collection.Count > 0 ? "Найдены подходящие вакансии" : "Подходящие вакансии не найдены";

                Collection = new ObservableCollection<object>(GetSuitableVacancies());
            }
        }, () => IsConsideration == true);

        private ObservableCollection<object> GetSuitableVacancies()
        {
            Vacancies = new ObservableCollection<object>();

            var suitableVacancies = _executor.GetSuitableVacancies(SelectedIdRequest);

            foreach(var suitableVacancy in suitableVacancies)
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
