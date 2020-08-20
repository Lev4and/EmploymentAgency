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
    public class MyRequestsViewModel : BindableBase
    {
        private int? _selectedIdProfessionCategory;

        private QueryExecutor _executor;
        private ConfigurationUser _config;

        public int? SelectedIdProfessionCategory
        {
            get { return _selectedIdProfessionCategory; }
            set
            {
                _selectedIdProfessionCategory = value;

                if (_selectedIdProfessionCategory != null)
                    UpdateProfessions();
                else
                    Professions = null;
            }
        }

        public int? SelectedIdProfession { get; set; }

        public int? SelectedIdRequest { get; set; }

        public int? SelectedIdRequestStatus { get; set; }

        public string ProfessionName { get; set; }

        public DateTime BeginValueDateOfRegistration { get; set; }

        public DateTime EndValueDateOfRegistration { get; set; }

        public DateTime MaxValueDateOfRegistration { get; set; }

        public DateTime MinValueDateOfRegistration { get; set; }

        public ObservableCollection<object> MyRequests { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ObservableCollection<RequestStatus> RequestStatuses { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddRequest());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeRequestViewModel.SelectedIdRequest = (int)SelectedIdRequest;

            WindowService.ShowWindow(new ChangeRequest());
        }, () => SelectedIdRequest != null);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveRequest((int)SelectedIdRequest);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdRequest != null);

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
            SelectedIdRequest = null;
            SelectedIdRequestStatus = null;
            SelectedIdProfessionCategory = null;
            SelectedIdProfession = null;

            ProfessionName = "";

            MinValueDateOfRegistration = _executor.GetMinDateOfRegistrationMyRequest(_config.IdUser);
            MaxValueDateOfRegistration = _executor.GetMaxDateOfRegistrationMyRequest(_config.IdUser);

            BeginValueDateOfRegistration = MinValueDateOfRegistration;

            EndValueDateOfRegistration = MaxValueDateOfRegistration;

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());
            RequestStatuses = new ObservableCollection<RequestStatus>(_executor.GetRequestStatuses(""));

            Find();
        }

        private void Find()
        {
            MyRequests = new ObservableCollection<object>(CollectionConverter<v_request>.ConvertToObjectList(_executor.GetMyRequests(_config.IdUser,
                (SelectedIdProfessionCategory != null ? (int)SelectedIdProfessionCategory : -1),
                (SelectedIdProfession != null ? (int)SelectedIdProfession : -1),
                (SelectedIdRequestStatus != null ? (int)SelectedIdRequestStatus : -1),
                ProfessionName,
                new Model.Logic.Range<DateTime>(BeginValueDateOfRegistration, EndValueDateOfRegistration))));
        }

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }
    }
}
