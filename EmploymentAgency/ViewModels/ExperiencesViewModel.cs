using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ExperiencesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdExperience { get; set; }

        public string ExperienceName { get; set; }

        public ObservableCollection<object> Experiences { get; set; }

        public ExperiencesViewModel()
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
            SelectedIdExperience = null;

            ExperienceName = "";

            Find();
        }

        private void Find()
        {
            Experiences = new ObservableCollection<object>(CollectionConverter<Experience>.ConvertToObjectList(_executor.GetExperiences(ExperienceName)));
        }
    }
}
