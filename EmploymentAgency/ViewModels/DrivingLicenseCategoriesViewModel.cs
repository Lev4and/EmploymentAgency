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
    public class DrivingLicenseCategoriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdDrivingLicenseCategory { get; set; }

        public string DrivingLicenseCategoryName { get; set; }

        public ObservableCollection<object> DrivingLicenseCategories { get; set; }

        public DrivingLicenseCategoriesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddDrivingLicenseCategory());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeDrivingLicenseCategoryViewModel.SelectedIdDrivingLicenseCategory = (int)SelectedIdDrivingLicenseCategory;

            WindowService.ShowWindow(new ChangeDrivingLicenseCategory());
        }, () => SelectedIdDrivingLicenseCategory != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveDrivingLicenseCategory((int)SelectedIdDrivingLicenseCategory);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdDrivingLicenseCategory != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdDrivingLicenseCategory = null;

            DrivingLicenseCategoryName = "";

            Find();
        }

        private void Find()
        {
            DrivingLicenseCategories = new ObservableCollection<object>(CollectionConverter<DrivingLicenseCategory>.ConvertToObjectList(_executor.GetDrivingLicenseCategories(DrivingLicenseCategoryName)));
        }
    }
}
