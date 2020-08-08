using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeBranchViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_branch _branch;

        public static int SelectedIdBranch { get; set; }


        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string StreetName { get; set; }

        public string NameHouse { get; set; }

        public string OrganizationName { get; set; }

        public string PhoneNumber { get; set; }

        public ChangeBranchViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            _branch = _executor.GetBranch(SelectedIdBranch);

            CountryName = _branch.CountryName;
            CityName = _branch.CityName;
            StreetName = _branch.StreetName;
            NameHouse = _branch.NameHouse;
            OrganizationName = _branch.OrganizationName;
            PhoneNumber = _branch.PhoneNumber;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            _executor.UpdateBranch(SelectedIdBranch, PhoneNumber);

            MessageBox.Show("Успешное изменение");
        }, () => (PhoneNumber != null ? PhoneNumber.Length > 0 : false));
    }
}
