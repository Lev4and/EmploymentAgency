using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeStreetViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_street _street;

        public static int SelectedIdStreet { get; set; }


        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string StreetName { get; set; }

        public ChangeStreetViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _street = _executor.GetStreetExtendedInformation(SelectedIdStreet);

            CountryName = _street.CountryName;
            CityName = _street.CityName;
            StreetName = _street.StreetName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateStreet((int)_street.IdStreet, StreetName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Улица с такими данными уже существует");
            }
        }, () => (StreetName != null ? StreetName.Length > 0 : false));
    }
}
