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
        private int? _selectedIdSchedule;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdSchedule
        {
            get { return _selectedIdSchedule; }
            set
            {
                _selectedIdSchedule = value;

                IsCanChange = _selectedIdSchedule != null ? true : false;
                IsCanRemove = _selectedIdSchedule != null ? true : false;
            }
        }

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

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSchedule());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeScheduleViewModel.SelectedIdSchedule = (int)SelectedIdSchedule;

            WindowService.ShowWindow(new ChangeSchedule());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSchedule((int)SelectedIdSchedule);

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
