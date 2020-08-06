using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ExperiencesViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdExperience { get; set; }

        public string ExperienceName { get; set; }

        public ObservableCollection<object> Experiences { get; set; }

        public ExperiencesViewModel(PageService pageService)
        {
            _pageService = pageService;
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

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new EmploymentAgency.Views.Pages.Menu());
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
