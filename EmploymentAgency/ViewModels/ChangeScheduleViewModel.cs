using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeScheduleViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Schedule _schedule;

        public static int SelectedIdSchedule { get; set; }

        
        public string ScheduleName { get; set; }

        public ChangeScheduleViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _schedule = _executor.GetSchedule(SelectedIdSchedule);

            ScheduleName = _schedule.ScheduleName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateSchedule(SelectedIdSchedule, ScheduleName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("График работы с такими данными уже существует");
            }
        }, () => (ScheduleName != null ? ScheduleName.Length > 0 : false));
    }
}
