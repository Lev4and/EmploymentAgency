using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SkillsViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private QueryExecutor _executor;

        public int? SelectedIdSkill { get; set; }

        public string SkillName { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public SkillsViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));

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
            SelectedIdSkill = null;

            SkillName = "";

            Find();
        }

        private void Find()
        {
            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills(SkillName)));
        }
    }
}
