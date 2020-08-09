using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddDrivingLicenseCategoryViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string DrivingLicenseCategoryName { get; set; }

        public AddDrivingLicenseCategoryViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(_executor.AddDrivingLicenseCategory(DrivingLicenseCategoryName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Водительское право с такими данными уже существует");
            }
        }, () => (DrivingLicenseCategoryName != null ? DrivingLicenseCategoryName.Length > 0 : false));
    }
}
