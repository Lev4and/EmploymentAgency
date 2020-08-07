using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SubIndustriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public int? SelectedIdSubIndustry { get; set; }

        public string NameSubIndustry { get; set; }

        public ObservableCollection<object> SubIndustries { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public SubIndustriesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

            ResetToDefault();
        });

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdIndustry != null || (NameSubIndustry != null ? NameSubIndustry.Length > 0 : false));

        private void ResetToDefault()
        {
            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;

            NameSubIndustry = "";

            Find();
        }

        private void Find()
        {
            SubIndustries = new ObservableCollection<object>(CollectionConverter<v_subIndustry>.ConvertToObjectList(_executor.GetSubIndustries(
                (SelectedIdIndustry != null ? (int)SelectedIdIndustry : -1), 
                NameSubIndustry)));
        }
    }
}
