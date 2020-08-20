using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeCountryViewModel : BindableBase
    {
        private byte[] _flag;

        private QueryExecutor _executor;
        private Country _country;

        public static int SelectedIdCountry { get; set; }


        public bool IsCanAddFlag { get; set; }

        public string CountryName { get; set; }

        public byte[] Flag
        {
            get { return _flag; }
            set
            {
                _flag = value;

                IsCanAddFlag = _flag != null ? false : true;
            }
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _country = _executor.GetCountry(SelectedIdCountry);

            IsCanAddFlag = true;

            CountryName = _country.CountryName;

            Flag = _country.Flag;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if(_executor.UpdateCountry(SelectedIdCountry, CountryName, Flag))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Государство с такими данными уже существует");
            }
        }, () => (CountryName != null ? CountryName.Length > 0 : false));
    }
}
