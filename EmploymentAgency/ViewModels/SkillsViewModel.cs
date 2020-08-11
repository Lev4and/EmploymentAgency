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
        private int? _selectedIdSkill;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdSkill
        {
            get { return _selectedIdSkill; }
            set
            {
                _selectedIdSkill = value;

                IsCanChange = _selectedIdSkill != null ? true : false;
                IsCanRemove = _selectedIdSkill != null ? true : false;
            }
        }

        public string SkillName { get; set; }

        public ObservableCollection<object> Skills { get; set; }

        public SkillsViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Skills = new ObservableCollection<object>(CollectionConverter<Skill>.ConvertToObjectList(_executor.GetSkills()));

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSkill());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeSkillViewModel.SelectedIdSkill = (int)SelectedIdSkill;

            WindowService.ShowWindow(new ChangeSkill());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSkill((int)SelectedIdSkill);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

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
