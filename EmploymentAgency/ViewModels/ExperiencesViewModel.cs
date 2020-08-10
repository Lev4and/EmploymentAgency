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
        private int? _selectedIdExperience;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdExperience
        {
            get { return _selectedIdExperience; }
            set
            {
                _selectedIdExperience = value;

                IsCanChange = _selectedIdExperience != null ? true : false;
                IsCanRemove = _selectedIdExperience != null ? true : false;
            }
        }

        public string ExperienceName { get; set; }

        public ObservableCollection<object> Experiences { get; set; }

        public ExperiencesViewModel()
        {

        }

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddExperience());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeExperienceViewModel.SelectedIdExperience = (int)SelectedIdExperience;

            WindowService.ShowWindow(new ChangeExperience());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveExperience((int)SelectedIdExperience);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

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
