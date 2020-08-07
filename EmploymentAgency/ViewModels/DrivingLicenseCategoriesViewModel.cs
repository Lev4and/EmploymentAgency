using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
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
