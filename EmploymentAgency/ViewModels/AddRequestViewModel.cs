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
    public class AddRequestViewModel : BindableBase
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

                if (_selectedIdProfessionCategory != null)
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

        public int? SelectedIdExperience { get; set; }

        public int? Salary { get; set; }

        public string AboutMe { get; set; }

        public string NameProfessionCategory
        {
            get { return _nameProfessionCategory; }
            set
            {
                _nameProfessionCategory = value;

                if (_nameProfessionCategory != null)
                {
                    if (_nameProfessionCategory.Length == 0)
                    {
                        SelectedIdProfessionCategory = null;
                        SelectedIdProfession = null;

                        ProfessionName = "";
                    }
                }

                if (ProfessionCategories != null)
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

                if (_professionName != null)
                {
                    if (_professionName.Length == 0)
                    {
                        SelectedIdProfession = null;
                    }
                }

                if (Professions != null)
                {
                    UpdateDisplayedProfessions();
                }
            }
        }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public ObservableCollection<object> SelectedEmploymentTypes { get; set; }

        public ObservableCollection<object> Schedules { get; set; }

        public ObservableCollection<object> SelectedSchedules { get; set; }

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
            SelectedIdExperience = null;

            SelectedEmploymentTypes = new ObservableCollection<object>();
            SelectedSchedules = new ObservableCollection<object>();

            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes()));
            Schedules = new ObservableCollection<object>(CollectionConverter<Schedule>.ConvertToObjectList(_executor.GetSchedules()));
            Experiences = new ObservableCollection<Experience>(_executor.GetExperiences());
            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            UpdateDisplayedProfessionCategories();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            _executor.AddRequest(_config.IdUser, (int)SelectedIdProfession, (int)SelectedIdExperience, Salary, AboutMe, SelectedEmploymentTypes.ToList(), SelectedSchedules.ToList());

            MessageBox.Show("Успешное добавление");
        }, () => SelectedIdProfession != null &&
                 SelectedIdExperience != null);

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }

        private void UpdateDisplayedProfessions()
        {
            DisplayedProfessions = new ObservableCollection<Profession>(Professions.Where(p => p.ProfessionName.ToLower().StartsWith(ProfessionName.ToLower())).ToList());
        }

        private void UpdateDisplayedProfessionCategories()
        {
            DisplayedProfessionCategories = new ObservableCollection<ProfessionCategory>(ProfessionCategories.Where(p => p.NameProfessionCategory.ToLower().StartsWith(NameProfessionCategory.ToLower())).ToList());
        }
    }
}
