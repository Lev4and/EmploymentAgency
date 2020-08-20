using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSkillViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Skill _skill;

        public static int SelectedIdSkill { get; set; }


        public string SkillName { get; set; }

        public byte[] Photo { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _skill = _executor.GetSkill(SelectedIdSkill);

            SkillName = _skill.SkillName;

            Photo = _skill.Photo;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateSkill(SelectedIdSkill, SkillName, Photo))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Навык с такими данными уже существует");
            }
        }, () => (SkillName != null ? SkillName.Length > 0 : false));
    }
}
