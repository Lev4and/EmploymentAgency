using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeVacancyViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_vacancy _vacancy;

        public static int SelectedIdVacancy { get; set; }

        public int? Salary { get; set; }

        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public string EmploymentTypeName { get; set; }

        public string ScheduleName { get; set; }

        public string ExperienceName { get; set; }

        public string Description { get; set; }

        public string Duties { get; set; }

        public string Requirements { get; set; }

        public string Terms { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ObservableCollection<object> SelectedSkills { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _vacancy = _executor.GetVacancyExtendedInformation(SelectedIdVacancy);

            Salary = _vacancy.Salary;

            NameProfessionCategory = _vacancy.NameProfessionCategory;
            ProfessionName = _vacancy.ProfessionName;
            EmploymentTypeName = _vacancy.EmploymentTypeName;
            ScheduleName = _vacancy.ScheduleName;
            ExperienceName = _vacancy.ExperienceName;
            Description = _vacancy.Description;
            Duties = _vacancy.Duties;
            Requirements = _vacancy.Requirements;
            Terms = _vacancy.Terms;

            SelectedSkills = new ObservableCollection<object>(CollectionConverter<NecessarySkill>.ConvertToObjectList(_executor.GetNecessarySkills(SelectedIdVacancy)));
            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (!_executor.IsClosedVacancy((int)SelectedIdVacancy))
            {
                _executor.UpdateVacancy(SelectedIdVacancy, Description, Duties, Requirements, Terms, Salary, SelectedSkills.ToList());

                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Нельзя менять данные закрытой вакансии");
            }
        });
    }
}
