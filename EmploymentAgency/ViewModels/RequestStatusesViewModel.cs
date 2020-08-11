using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class RequestStatusesViewModel : BindableBase
    {
        private int? _selectedIdRequestStatus;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdRequestStatus
        {
            get { return _selectedIdRequestStatus; }
            set
            {
                _selectedIdRequestStatus = value;

                IsCanChange = _selectedIdRequestStatus != null ? true : false;
                IsCanRemove = _selectedIdRequestStatus != null ? true : false;
            }
        }

        public string RequestStatusName { get; set; }

        public ObservableCollection<object> RequestStatuses { get; set; }

        public RequestStatusesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddRequestStatus());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeRequestStatusViewModel.SelectedIdRequestStatus = (int)SelectedIdRequestStatus;

            WindowService.ShowWindow(new ChangeRequestStatus());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveRequestStatus((int)SelectedIdRequestStatus);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdRequestStatus = null;

            RequestStatusName = "";

            Find();
        }

        private void Find()
        {
            RequestStatuses = new ObservableCollection<object>(CollectionConverter<RequestStatus>.ConvertToObjectList(_executor.GetRequestStatuses(RequestStatusName)));
        }
    }
}
