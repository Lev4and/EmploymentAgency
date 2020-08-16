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
    public class EducationsViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdEducation { get; set; }

        public string EducationName { get; set; }

        public ObservableCollection<object> Educations { get; set; }

        public EducationsViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddEducation());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeEducationViewModel.SelectedIdEducation = (int)SelectedIdEducation;

            WindowService.ShowWindow(new ChangeEducation());
        }, () => SelectedIdEducation != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveEducation((int)SelectedIdEducation);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdEducation != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdEducation = null;

            EducationName = "";

            Find();
        }

        private void Find()
        {
            Educations = new ObservableCollection<object>(CollectionConverter<Education>.ConvertToObjectList(_executor.GetEducations(EducationName)));
        }
    }
}
