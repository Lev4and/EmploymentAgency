using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ProfessionCategoriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdProfessionCategory { get; set; }

        public string NameProfessionCategory { get; set; }

        public ObservableCollection<object> ProfessionCategories { get; set; }

        public ProfessionCategoriesViewModel()
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
            SelectedIdProfessionCategory = null;

            NameProfessionCategory = "";

            Find();
        }

        private void Find()
        {
            ProfessionCategories = new ObservableCollection<object>(CollectionConverter<ProfessionCategory>.ConvertToObjectList(_executor.GetProfessionCategories(NameProfessionCategory)));
        }
    }
}
