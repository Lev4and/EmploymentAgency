using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class EmploymentTypesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdEmploymentType { get; set; }

        public string EmploymentTypeName { get; set; }

        public ObservableCollection<object> EmploymentTypes { get; set; }

        public EmploymentTypesViewModel()
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
            SelectedIdEmploymentType = null;

            EmploymentTypeName = "";

            Find();
        }

        private void Find()
        {
            EmploymentTypes = new ObservableCollection<object>(CollectionConverter<EmploymentType>.ConvertToObjectList(_executor.GetEmploymentTypes(EmploymentTypeName)));
        }
    }
}
