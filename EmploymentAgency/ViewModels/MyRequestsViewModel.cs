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
    public class MyRequestsViewModel : BindableBase
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
                    Professions = null;
            }
        }

        public int? SelectedIdProfession { get; set; }

        public int? SelectedIdRequest { get; set; }

        public int? SelectedIdRequestStatus { get; set; }

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

        public ObservableCollection<object> MyRequests { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<ProfessionCategory> DisplayedProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ObservableCollection<Profession> DisplayedProfessions { get; set; }

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

        public ICommand ShowDetails => new DelegateCommand(() =>
        {
            DetailRequestViewModel.SelectedIdRequest = (int)SelectedIdRequest;

            WindowService.ShowWindow(new DetailRequest());
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

            NameProfessionCategory = "";
            ProfessionName = "";

            MinValueDateOfRegistration = _executor.GetMinDateOfRegistrationMyRequest(_config.IdUser);
            MaxValueDateOfRegistration = _executor.GetMaxDateOfRegistrationMyRequest(_config.IdUser);

            BeginValueDateOfRegistration = MinValueDateOfRegistration;

            EndValueDateOfRegistration = MaxValueDateOfRegistration;

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());
            RequestStatuses = new ObservableCollection<RequestStatus>(_executor.GetRequestStatuses(""));

            UpdateDisplayedProfessionCategories();

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
