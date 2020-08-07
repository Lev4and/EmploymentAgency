using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class GendersViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdGender { get; set; }

        public string GenderName { get; set; }

        public ObservableCollection<object> Genders { get; set; }

        public GendersViewModel()
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
            SelectedIdGender = null;

            GenderName = "";

            Find();
        }

        private void Find()
        {
            Genders = new ObservableCollection<object>(CollectionConverter<Gender>.ConvertToObjectList(_executor.GetGenders(GenderName)));
        }
    }
}
