using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddScheduleViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string ScheduleName { get; set; }

        public AddScheduleViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddSchedule(ScheduleName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("График работы с такими данными уже существует");
            }
        }, () => (ScheduleName != null ? ScheduleName.Length > 0 : false));
    }
}
