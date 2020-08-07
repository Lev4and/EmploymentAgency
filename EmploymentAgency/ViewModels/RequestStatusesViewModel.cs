using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class RequestStatusesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRequestStatus { get; set; }

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
