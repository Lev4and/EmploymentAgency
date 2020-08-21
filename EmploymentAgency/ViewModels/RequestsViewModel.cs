using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class RequestsViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRequest { get; set; }

        public int? SelectedIdRequestStatus { get; set; }

        public DateTime BeginValueDateOfRegistration { get; set; }

        public DateTime EndValueDateOfRegistration { get; set; }

        public DateTime MaxValueDateOfRegistration { get; set; }

        public DateTime MinValueDateOfRegistration { get; set; }

        public DateTime? BeginValueDateOfConsideration { get; set; }

        public DateTime? EndValueDateOfConsideration { get; set; }

        public DateTime? MaxValueDateOfConsideration { get; set; }

        public DateTime? MinValueDateOfConsideration { get; set; }

        public string ProfessionName { get; set; }

        public ObservableCollection<object> Requests { get; set; }

        public ObservableCollection<RequestStatus> RequestStatuses { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

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

        public ICommand Consider => new DelegateCommand(() =>
        {
            ConsiderRequestViewModel.SelectedIdRequest = (int)SelectedIdRequest;

            WindowService.ShowWindow(new ConsiderRequest());
        }, () => SelectedIdRequest != null);

        private void ResetToDefault()
        {
            SelectedIdRequest = null;
            SelectedIdRequestStatus = null;

            MinValueDateOfConsideration = _executor.GetMinDateOfConsiderationRequest();
            MinValueDateOfRegistration = _executor.GetMinDateOfRegistrationRequest();

            MaxValueDateOfConsideration = _executor.GetMaxDateOfConsiderationRequest();
            MaxValueDateOfRegistration = _executor.GetMaxDateOfRegistrationRequest();

            BeginValueDateOfConsideration = MinValueDateOfConsideration;
            BeginValueDateOfRegistration = MinValueDateOfRegistration;

            EndValueDateOfConsideration = MaxValueDateOfConsideration;
            EndValueDateOfRegistration = MaxValueDateOfRegistration;

            RequestStatuses = new ObservableCollection<RequestStatus>(_executor.GetRequestStatuses(""));

            Find();
        }

        private void Find()
        {
            Requests = new ObservableCollection<object>(CollectionConverter<v_request>.ConvertToObjectList(_executor.GetRequests(
                (SelectedIdRequestStatus != null ? (int)SelectedIdRequestStatus : -1),
                ProfessionName,
                new Model.Logic.Range<DateTime>(BeginValueDateOfRegistration, EndValueDateOfRegistration),
                BeginValueDateOfConsideration,
                EndValueDateOfConsideration)));
        }
    }
}
