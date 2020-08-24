using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MyContractsApplicantViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;

        public int? SelectedIdContract { get; set; }

        public int? BeginValueSalary { get; set; }

        public int? EndValueSalary { get; set; }

        public int? MaxValueSalary { get; set; }

        public int? MinValueSalary { get; set; }

        public DateTime BeginValueDateOfConclusion { get; set; }

        public DateTime EndValueDateOfConclusion { get; set; }

        public DateTime MaxValueDateOfConclusion { get; set; }

        public DateTime MinValueDateOfConclusion { get; set; }

        public DateTime? BeginValueBreakDate { get; set; }

        public DateTime? EndValueBreakDate { get; set; }

        public DateTime? MaxValueBreakDate { get; set; }

        public DateTime? MinValueBreakDate { get; set; }

        public string OrganizationName { get; set; }

        public ObservableCollection<object> Contracts { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            ResetToDefault();
        });

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        });

        public ICommand ShowDetailVacancy => new DelegateCommand(() =>
        {
            ShowVacancyViewModel.SelectedIdVacancy = _executor.GetContract((int)SelectedIdContract).EmploymentRequest.SuitableVacancy.Vacancy.IdVacancy;

            WindowService.ShowWindow(new ShowVacancy());
        }, () => SelectedIdContract != null);

        private void ResetToDefault()
        {
            SelectedIdContract = null;

            OrganizationName = "";

            MinValueBreakDate = _executor.GetMinBreakDateMyContractsApplicant(_config.IdUser);
            MinValueDateOfConclusion = _executor.GetMinDateOfConclusionMyContractsApplicant(_config.IdUser);
            MinValueSalary = _executor.GetMinSalaryMyContractsApplicant(_config.IdUser);

            MaxValueBreakDate = _executor.GetMaxBreakDateMyContractsApplicant(_config.IdUser);
            MaxValueDateOfConclusion = _executor.GetMaxDateOfConclusionMyContractsApplicant(_config.IdUser);
            MaxValueSalary = _executor.GetMaxSalaryMyContractsApplicant(_config.IdUser);

            BeginValueBreakDate = MinValueBreakDate;
            BeginValueDateOfConclusion = MinValueDateOfConclusion;
            BeginValueSalary = MinValueSalary;

            EndValueBreakDate = MaxValueBreakDate;
            EndValueDateOfConclusion = MaxValueDateOfConclusion;
            EndValueSalary = MaxValueSalary;

            Find();
        }

        private void Find()
        {
            Contracts = new ObservableCollection<object>(CollectionConverter<v_contract>.ConvertToObjectList(_executor.GetContractsForApplicant(
                _config.IdUser,
                OrganizationName,
                new Model.Logic.Range<DateTime>(BeginValueDateOfConclusion, EndValueDateOfConclusion),
                BeginValueBreakDate,
                EndValueBreakDate,
                BeginValueSalary,
                EndValueSalary)));
        }
    }
}
