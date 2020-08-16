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
    public class ExperiencesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdExperience { get; set; }

        public string ExperienceName { get; set; }

        public ObservableCollection<object> Experiences { get; set; }

        public ExperiencesViewModel()
        {

        }

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddExperience());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeExperienceViewModel.SelectedIdExperience = (int)SelectedIdExperience;

            WindowService.ShowWindow(new ChangeExperience());
        }, () => SelectedIdExperience != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveExperience((int)SelectedIdExperience);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdExperience != null ? true : false);

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
            SelectedIdExperience = null;

            ExperienceName = "";

            Find();
        }

        private void Find()
        {
            Experiences = new ObservableCollection<object>(CollectionConverter<Experience>.ConvertToObjectList(_executor.GetExperiences(ExperienceName)));
        }
    }
}
