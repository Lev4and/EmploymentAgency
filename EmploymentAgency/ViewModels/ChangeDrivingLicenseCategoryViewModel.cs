using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeDrivingLicenseCategoryViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private DrivingLicenseCategory _drivingLicenseCategory;

        public static int SelectedIdDrivingLicenseCategory;


        public string DrivingLicenseCategoryName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _drivingLicenseCategory = _executor.GetDrivingLicenseCategory(SelectedIdDrivingLicenseCategory);

            DrivingLicenseCategoryName = _drivingLicenseCategory.DrivingLicenseCategoryName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateDrivingLicenseCategory(SelectedIdDrivingLicenseCategory, DrivingLicenseCategoryName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Водительское право с такими данными уже существует");
            }
        }, () => (DrivingLicenseCategoryName != null ? DrivingLicenseCategoryName.Length > 0 : false));
    }
}
