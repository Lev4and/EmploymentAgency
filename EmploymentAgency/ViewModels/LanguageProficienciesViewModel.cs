using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class LanguageProficienciesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdLanguageProficiency { get; set; }

        public string LanguageProficiencyName { get; set; }

        public ObservableCollection<object> LanguageProficiencies { get; set; }

        public LanguageProficienciesViewModel()
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
            SelectedIdLanguageProficiency = null;

            LanguageProficiencyName = "";

            Find();
        }

        private void Find()
        {
            LanguageProficiencies = new ObservableCollection<object>(CollectionConverter<LanguageProficiency>.ConvertToObjectList(_executor.GetLanguageProficiencies(LanguageProficiencyName)));
        }
    }
}
