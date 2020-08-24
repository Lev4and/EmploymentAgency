using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddVacancyViewModel : BindableBase
    {
        private int? _selectedIdProfessionCategory;

        private string _nameProfessionCategory;
        private string _professionName;

        private QueryExecutor _executor;
        private ConfigurationUser _config;

        public int? SelectedIdProfessionCategory
        {
            get { return _selectedIdProfessionCategory; }
            set
            {
                _selectedIdProfessionCategory = value;

                if(_selectedIdProfessionCategory != null)
                {
                    UpdateProfessions();
                    UpdateDisplayedProfessions();
                }
                else
                {
                    Professions = null;
                    DisplayedProfessions = null;
                }
            }
        }

        public int? SelectedIdProfession { get; set; }

        public int? SelectedIdEmploymentType { get; set; }

        public int? SelectedIdSchedule { get; set; }

        public int? SelectedIdExperience { get; set; }

        public int? Salary { get; set; }

        public string NameProfessionCategory
        {
            get { return _nameProfessionCategory; }
            set
            {
                _nameProfessionCategory = value;

                if(_nameProfessionCategory != null)
                {
                    if(_nameProfessionCategory.Length == 0)
                    {
                        SelectedIdProfessionCategory = null;
                        SelectedIdProfession = null;

                        ProfessionName = "";
                    }
                }

                if(ProfessionCategories != null)
                {
                    UpdateDisplayedProfessionCategories();
                }
            }
        }

        public string ProfessionName
        {
            get { return _professionName; }
            set
            {
                _professionName = value;

                if(_professionName != null)
                {
                    if(_professionName.Length == 0)
                    {
                        SelectedIdProfession = null;
                    }
                }

                if(Professions != null)
                {
                    UpdateDisplayedProfessions();
                }
            }
        }

        public string Description { get; set; }

        public string Duties { get; set; }

        public string Requirements { get; set; }

        public string Terms { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ObservableCollection<EmploymentType> EmploymentTypes { get; set; }

        public ObservableCollection<Schedule> Schedules { get; set; }

        public ObservableCollection<Experience> Experiences { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<ProfessionCategory> DisplayedProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ObservableCollection<Profession> DisplayedProfessions { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            NameProfessionCategory = "";
            ProfessionName = "";

            SelectedIdProfessionCategory = null;
            SelectedIdProfession = null;

            SelectedSkills = new ObservableCollection<object>();

            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));
            EmploymentTypes = new ObservableCollection<EmploymentType>(_executor.GetEmploymentTypes());
            Schedules = new ObservableCollection<Schedule>(_executor.GetSchedules());
            Experiences = new ObservableCollection<Experience>(_executor.GetExperiences());
            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            UpdateDisplayedProfessionCategories();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            _executor.AddVacancy(_config.IdUser, (int)SelectedIdProfession, (int)SelectedIdEmploymentType, (int)SelectedIdSchedule, (int)SelectedIdExperience, Description, Duties, Requirements, Terms, Salary, SelectedSkills.ToList());

            MessageBox.Show("Успешное добавление");
        }, () => SelectedIdProfession != null &&
                 SelectedIdEmploymentType != null &&
                 SelectedIdSchedule != null &&
                 SelectedIdExperience != null);

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }

        private void UpdateDisplayedProfessions()
        {
            DisplayedProfessions = new ObservableCollection<Profession>(Professions.Where(p => SearchAssistant.GetSearchAction(p.ProfessionName, ProfessionName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedProfessionCategories()
        {
            DisplayedProfessionCategories = new ObservableCollection<ProfessionCategory>(ProfessionCategories.Where(p => SearchAssistant.GetSearchAction(p.NameProfessionCategory, NameProfessionCategory, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }
    }
}
