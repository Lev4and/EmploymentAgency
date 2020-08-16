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
    public class EmploymentTypesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdEmploymentType { get; set; }

        public string EmploymentTypeName { get; set; }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public EmploymentTypesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddEmploymentType());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeEmploymentTypeViewModel.SelectedIdEmploymentType = (int)SelectedIdEmploymentType;

            WindowService.ShowWindow(new ChangeEmploymentType());
        }, () => SelectedIdEmploymentType != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveEmploymentType((int)SelectedIdEmploymentType);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdEmploymentType != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdEmploymentType = null;

            EmploymentTypeName = "";

            Find();
        }

        private void Find()
        {
            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes(EmploymentTypeName)));
        }
    }
}
