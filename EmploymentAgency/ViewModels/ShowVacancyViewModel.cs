using Converters;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ShowVacancyViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_vacancy _vacancy;

        public static int SelectedIdVacancy { get; set; }

        public int? Salary { get; set; }

        public string OrganizationName { get; set; }

        public string AddressBranch { get; set; }

        public string PhoneNumberBranch { get; set; }

        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public string EmploymentTypeName { get; set; }

        public string ScheduleName { get; set; }

        public string ExperienceName { get; set; }

        public string Description { get; set; }

        public string Duties { get; set; }

        public string Requirements { get; set; }

        public string Terms { get; set; }

        public string EmployerFullName { get; set; }

        public string PhoneNumberEmployer { get; set; }

        public byte[] OrganizationPhoto { get; set; }

        public byte[] EmployerPhoto { get; set; }

        public ObservableCollection<Skill> Skills { get; set; }

        public ObservableCollection<object> NecessarySkills { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _vacancy = _executor.GetVacancyExtendedInformation(SelectedIdVacancy);

            Salary = _vacancy.Salary;

            OrganizationName = _vacancy.OrganizationName;
            AddressBranch = _vacancy.AddressBranch;
            PhoneNumberBranch = _vacancy.PhoneNumberBranch;
            NameProfessionCategory = _vacancy.NameProfessionCategory;
            ProfessionName = _vacancy.ProfessionName;
            EmploymentTypeName = _vacancy.EmploymentTypeName;
            ScheduleName = _vacancy.ScheduleName;
            ExperienceName = _vacancy.ExperienceName;
            Description = _vacancy.Description;
            Duties = _vacancy.Duties;
            Requirements = _vacancy.Requirements;
            Terms = _vacancy.Terms;
            EmployerFullName = _vacancy.EmployerFullName;
            PhoneNumberEmployer = _vacancy.PhoneNumberEmployer;
            OrganizationPhoto = _vacancy.OrganizationPhoto;
            EmployerPhoto = _vacancy.EmployerPhoto;

            Skills = new ObservableCollection<Skill>(_executor.GetSkills());
            NecessarySkills = GetNecessarySkills();
        });

        private ObservableCollection<object> GetNecessarySkills()
        {
            var result = new ObservableCollection<object>();
            var necessarySkills = _executor.GetNecessarySkills(SelectedIdVacancy);

            Skills.ForEach(s =>
            {
                if (necessarySkills.Any(n => n.IdSkill == s.IdSkill))
                    result.Add(s);
            });

            return result;
        }
    }
}
