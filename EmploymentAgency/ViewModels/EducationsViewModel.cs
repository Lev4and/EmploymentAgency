using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class EducationsViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdEducation { get; set; }

        public string EducationName { get; set; }

        public ObservableCollection<object> Educations { get; set; }

        public EducationsViewModel()
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
            SelectedIdEducation = null;

            EducationName = "";

            Find();
        }

        private void Find()
        {
            Educations = new ObservableCollection<object>(CollectionConverter<Education>.ConvertToObjectList(_executor.GetEducations(EducationName)));
        }
    }
}
