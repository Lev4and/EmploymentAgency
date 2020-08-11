using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeRequestStatusViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private RequestStatus _requestStatus;

        public static int SelectedIdRequestStatus { get; set; }


        public string RequestStatusName { get; set; }

        public ChangeRequestStatusViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _requestStatus = _executor.GetRequestStatus(SelectedIdRequestStatus);

            RequestStatusName = _requestStatus.RequestStatusName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateRequestStatus(SelectedIdRequestStatus, RequestStatusName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Статус рассмотрения заявки с такими данными уже существует");
            }
        }, () => (RequestStatusName != null ? RequestStatusName.Length > 0 : false));
    }
}
