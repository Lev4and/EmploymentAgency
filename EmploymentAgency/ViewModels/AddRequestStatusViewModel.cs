using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddRequestStatusViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string RequestStatusName { get; set; }

        public AddRequestStatusViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddRequestStatus(RequestStatusName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Статус рассмотрения заявки с такими данными уже существует");
            }
        }, () => (RequestStatusName != null ? RequestStatusName.Length > 0 : false));
    }
}
