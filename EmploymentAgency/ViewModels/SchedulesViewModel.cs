using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SchedulesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdSchedule { get; set; }

        public string ScheduleName { get; set; }

        public ObservableCollection<object> Schedules { get; set; }

        public SchedulesViewModel()
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
            SelectedIdSchedule = null;

            ScheduleName = "";

            Find();
        }

        private void Find()
        {
            Schedules = new ObservableCollection<object>(CollectionConverter<Schedule>.ConvertToObjectList(_executor.GetSchedules(ScheduleName)));
        }
    }
}
