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
        private int? _selectedIdEducation;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdEducation
        {
            get { return _selectedIdEducation; }
            set
            {
                _selectedIdEducation = value;

                IsCanChange = _selectedIdEducation != null ? true : false;
                IsCanRemove = _selectedIdEducation != null ? true : false;
            }
        }

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
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeEducationViewModel.SelectedIdEducation = (int)SelectedIdEducation;

            WindowService.ShowWindow(new ChangeEducation());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveEducation((int)SelectedIdEducation);

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
