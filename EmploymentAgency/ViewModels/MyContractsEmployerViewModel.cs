using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MyContractsEmployerViewModel : BindableBase
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

        public string ApplicantFullName { get; set; }

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

        public ICommand ShowDetailRequest => new DelegateCommand(() =>
        {
            ConsiderRequestViewModel.SelectedIdRequest = _executor.GetContract((int)SelectedIdContract).EmploymentRequest.SuitableVacancy.Request.IdRequest;

            WindowService.ShowWindow(new ConsiderRequest());
        }, () => SelectedIdContract != null);

        public ICommand BreakContract => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите прекратить действие трудового договора?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if(_executor.BreakContract((int)SelectedIdContract))
                {
                    MessageBox.Show("Трудовой договор успешно разорван");

                    Find();
                }
                else
                {
                    MessageBox.Show("Трудовой договор уже был разорван");
                }
            }
        }, () => SelectedIdContract != null);

        private void ResetToDefault()
        {
            SelectedIdContract = null;

            ApplicantFullName = "";

            MinValueBreakDate = _executor.GetMinBreakDateMyContractsEmployer(_config.IdUser);
            MinValueDateOfConclusion = _executor.GetMinDateOfConclusionMyContractsEmployer(_config.IdUser);
            MinValueSalary = _executor.GetMinSalaryMyContractsEmployer(_config.IdUser);

            MaxValueBreakDate = _executor.GetMaxBreakDateMyContractsEmployer(_config.IdUser);
            MaxValueDateOfConclusion = _executor.GetMaxDateOfConclusionMyContractsEmployer(_config.IdUser);
            MaxValueSalary = _executor.GetMaxSalaryMyContractsEmployer(_config.IdUser);

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
            Contracts = new ObservableCollection<object>(CollectionConverter<v_contract>.ConvertToObjectList(_executor.GetContractsForEmployer(
                _config.IdUser,
                ApplicantFullName,
                new Model.Logic.Range<DateTime>(BeginValueDateOfConclusion, EndValueDateOfConclusion),
                BeginValueBreakDate,
                EndValueBreakDate,
                BeginValueSalary,
                EndValueSalary)));
        }
    }
}
