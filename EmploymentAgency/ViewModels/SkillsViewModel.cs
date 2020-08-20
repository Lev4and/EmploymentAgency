using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SkillsViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdSkill { get; set; }

        public string SkillName { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSkill());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeSkillViewModel.SelectedIdSkill = (int)SelectedIdSkill;

            WindowService.ShowWindow(new ChangeSkill());
        }, () => SelectedIdSkill != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSkill((int)SelectedIdSkill);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdSkill != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
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
