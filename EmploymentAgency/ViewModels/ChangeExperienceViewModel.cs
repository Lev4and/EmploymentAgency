using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeExperienceViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Experience _experience;

        public static int SelectedIdExperience { get; set; }


        public string ExperienceName { get; set; }

        public ChangeExperienceViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _experience = _executor.GetExperience(SelectedIdExperience);

            ExperienceName = _experience.ExperienceName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateExperience(SelectedIdExperience, ExperienceName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Опыт с такими данными уже существует");
            }
        }, () => (ExperienceName != null ? ExperienceName.Length > 0 : false));
    }
}
