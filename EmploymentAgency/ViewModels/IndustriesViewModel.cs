using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class IndustriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public string IndustryName { get; set; }

        public ObservableCollection<object> Industries { get; set; }

        public IndustriesViewModel()
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
            SelectedIdIndustry = null;

            IndustryName = "";

            Find();
        }

        private void Find()
        {
            Industries = new ObservableCollection<object>(CollectionConverter<Industry>.ConvertToObjectList(_executor.GetIndustries(IndustryName)));
        }
    }
}
