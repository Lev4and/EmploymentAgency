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
    public class SchedulesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdSchedule { get; set; }

        public string ScheduleName { get; set; }

        public ObservableCollection<object> Schedules { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSchedule());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeScheduleViewModel.SelectedIdSchedule = (int)SelectedIdSchedule;

            WindowService.ShowWindow(new ChangeSchedule());
        }, () => SelectedIdSchedule != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSchedule((int)SelectedIdSchedule);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdSchedule != null ? true : false);

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
