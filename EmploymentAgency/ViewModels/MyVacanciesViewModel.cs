using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MyVacanciesViewModel : BindableBase
    {
        private int? _selectedIdProfessionCategory;

        private string _nameProfessionCategory;
        private string _professionName;

        private QueryExecutor _executor;
        private ConfigurationUser _config;

        public int? BeginValueNumberOfAcceptedApplicants { get; set; }

        public int? BeginValueNumberOfApprovedApplicants { get; set; }

        public int? BeginValueNumberOfInterestedApplicants { get; set; }

        public int? BeginValueNumberOfPotentialApplicants { get; set; }

        public int? BeginValueSalary { get; set; }

        public int? EndValueNumberOfAcceptedApplicants { get; set; }

        public int? EndValueNumberOfApprovedApplicants { get; set; }

        public int? EndValueNumberOfInterestedApplicants { get; set; }

        public int? EndValueNumberOfPotentialApplicants { get; set; }

        public int? EndValueSalary { get; set; }

        public int? MaxValueNumberOfAcceptedApplicants { get; set; }

        public int? MaxValueNumberOfApprovedApplicants { get; set; }

        public int? MaxValueNumberOfInterestedApplicants { get; set; }

        public int? MaxValueNumberOfPotentialApplicants { get; set; }

        public int? MaxValueSalary { get; set; }

        public int? MinValueNumberOfAcceptedApplicants { get; set; }

        public int? MinValueNumberOfApprovedApplicants { get; set; }

        public int? MinValueNumberOfInterestedApplicants { get; set; }

        public int? MinValueNumberOfPotentialApplicants { get; set; }

        public int? MinValueSalary { get; set; }

        public int? SelectedIdVacancy { get; set; }

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
                    Professions = null;
            }
        }

        public int? SelectedIdProfession { get; set; }

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

        public DateTime BeginValueDateOfRegistration { get; set; }

        public DateTime EndValueDateOfRegistration { get; set; }

        public DateTime MaxValueDateOfRegistration { get; set; }

        public DateTime MinValueDateOfRegistration { get; set; }

        public DateTime? BeginValueClosingDate { get; set; }

        public DateTime? EndValueClosingDate { get; set; }

        public DateTime? MaxValueClosingDate { get; set; }

        public DateTime? MinValueClosingDate { get; set; }

        public ObservableCollection<object> MyVacancies { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<ProfessionCategory> DisplayedProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ObservableCollection<Profession> DisplayedProfessions { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddVacancy());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeVacancyViewModel.SelectedIdVacancy = (int)SelectedIdVacancy;

            WindowService.ShowWindow(new ChangeVacancy());
        }, () => SelectedIdVacancy != null);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveVacancy((int)SelectedIdVacancy);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdVacancy != null);

        public ICommand CloseVacancy => new DelegateCommand(() =>
        {
            if(!_executor.IsClosedVacancy((int)SelectedIdVacancy))
            {
                if (MessageBox.Show("Вы действительно хотите закрыть вакансию?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _executor.CloseVacancy((int)SelectedIdVacancy);

                    MessageBox.Show("Успешное закрытие");

                    Find();
                }

            }
            else
            {
                MessageBox.Show("Вакансия уже закрыта");
            }
        }, () => SelectedIdVacancy != null);

        public ICommand ShowDetails => new DelegateCommand(() =>
        {
            DetailVacancyViewModel.SelectedIdVacancy = (int)SelectedIdVacancy;

            WindowService.ShowWindow(new DetailVacancy());
        }, () => SelectedIdVacancy != null);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        });

        private void ResetToDefault()
        {
            SelectedIdVacancy = null;
            SelectedIdProfessionCategory = null;
            SelectedIdProfession = null;

            MinValueClosingDate = _executor.GetMinClosingDateMyVacancy(_config.IdUser);
            MinValueDateOfRegistration = _executor.GetMinDateOfRegistrationMyVacancy(_config.IdUser);
            MinValueNumberOfAcceptedApplicants = _executor.GetMinNumberOfAcceptedApplicantsMyVacancy(_config.IdUser);
            MinValueNumberOfApprovedApplicants = _executor.GetMinNumberOfApprovedApplicantsMyVacancy(_config.IdUser);
            MinValueNumberOfInterestedApplicants = _executor.GetMinNumberOfInterestedApplicantsMyVacancy(_config.IdUser);
            MinValueNumberOfPotentialApplicants = _executor.GetMinNumberOfPotentialApplicantsMyVacancy(_config.IdUser);
            MinValueSalary = _executor.GetMinSalaryMyVacancy(_config.IdUser);

            MaxValueClosingDate = _executor.GetMaxClosingDateMyVacancy(_config.IdUser);
            MaxValueDateOfRegistration = _executor.GetMaxDateOfRegistrationMyVacancy(_config.IdUser);
            MaxValueNumberOfAcceptedApplicants = _executor.GetMaxNumberOfAcceptedApplicantsMyVacancy(_config.IdUser);
            MaxValueNumberOfApprovedApplicants = _executor.GetMaxNumberOfApprovedApplicantsMyVacancy(_config.IdUser);
            MaxValueNumberOfInterestedApplicants = _executor.GetMaxNumberOfInterestedApplicantsMyVacancy(_config.IdUser);
            MaxValueNumberOfPotentialApplicants = _executor.GetMaxNumberOfPotentialApplicantsMyVacancy(_config.IdUser);
            MaxValueSalary = _executor.GetMaxSalaryMyVacancy(_config.IdUser);

            BeginValueClosingDate = MinValueClosingDate;
            BeginValueDateOfRegistration = MinValueDateOfRegistration;
            BeginValueNumberOfAcceptedApplicants = MinValueNumberOfAcceptedApplicants;
            BeginValueNumberOfApprovedApplicants = MinValueNumberOfApprovedApplicants;
            BeginValueNumberOfInterestedApplicants = MinValueNumberOfInterestedApplicants;
            BeginValueNumberOfPotentialApplicants = MinValueNumberOfPotentialApplicants;
            BeginValueSalary = MinValueSalary;

            EndValueClosingDate = MaxValueClosingDate;
            EndValueDateOfRegistration = MaxValueDateOfRegistration;
            EndValueNumberOfAcceptedApplicants = MaxValueNumberOfAcceptedApplicants;
            EndValueNumberOfApprovedApplicants = MaxValueNumberOfApprovedApplicants;
            EndValueNumberOfInterestedApplicants = MaxValueNumberOfInterestedApplicants;
            EndValueNumberOfPotentialApplicants = MaxValueNumberOfPotentialApplicants;
            EndValueSalary = MaxValueSalary;

            NameProfessionCategory = "";
            ProfessionName = "";

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            UpdateDisplayedProfessionCategories();

            Find();
        }

        private void Find()
        {
            MyVacancies = new ObservableCollection<object>(CollectionConverter<v_vacancy>.ConvertToObjectList(_executor.GetMyVacancies(_config.IdUser, 
                (SelectedIdProfessionCategory != null ? (int)SelectedIdProfessionCategory : -1),
                (SelectedIdProfession != null ? (int)SelectedIdProfession : -1), 
                ProfessionName,
                BeginValueClosingDate,
                EndValueClosingDate,
                BeginValueDateOfRegistration,
                EndValueDateOfRegistration,
                BeginValueNumberOfAcceptedApplicants,
                EndValueNumberOfAcceptedApplicants,
                BeginValueNumberOfApprovedApplicants,
                EndValueNumberOfApprovedApplicants,
                BeginValueNumberOfInterestedApplicants,
                EndValueNumberOfInterestedApplicants,
                BeginValueNumberOfPotentialApplicants,
                EndValueNumberOfPotentialApplicants,
                BeginValueSalary,
                EndValueSalary)));
        }

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }

        private void UpdateDisplayedProfessionCategories()
        {
            DisplayedProfessionCategories = new ObservableCollection<ProfessionCategory>(ProfessionCategories.Where(p => SearchAssistant.GetSearchAction(p.NameProfessionCategory, NameProfessionCategory, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedProfessions()
        {
            DisplayedProfessions = new ObservableCollection<Profession>(Professions.Where(p => SearchAssistant.GetSearchAction(p.ProfessionName, ProfessionName, SearchAssistant.SearchType.AllCriteria).Invoke()).Take(15).ToList());
        }
    }
}
