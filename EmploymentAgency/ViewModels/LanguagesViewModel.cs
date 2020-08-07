using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class LanguagesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdLanguage { get; set; }

        public string LanguageName { get; set; }

        public ObservableCollection<object> Languages { get; set; }

        public LanguagesViewModel()
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
            SelectedIdLanguage = null;

            LanguageName = "";

            Find();
        }

        private void Find()
        {
            Languages = new ObservableCollection<object>(CollectionConverter<Language>.ConvertToObjectList(_executor.GetLanguages(LanguageName)));
        }
    }
}
