using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeCityViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private City _city;

        public static int SelectedIdCity { get; set; }

        public string CityName { get; set; }

        public ChangeCityViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _city = _executor.GetCity(SelectedIdCity);

            CityName = _city.CityName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if(_executor.UpdateCity(_city.IdCountry, SelectedIdCity, CityName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Город с такими данными уже существует");
            }
        }, () => (CityName != null ? CityName.Length > 0 : false));
    }
}
