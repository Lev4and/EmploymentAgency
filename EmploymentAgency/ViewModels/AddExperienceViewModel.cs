using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddExperienceViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string ExperienceName { get; set; }

        public AddExperienceViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddExperience(ExperienceName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Опыт с такими данными уже существует");
            }
        }, () => (ExperienceName != null ? ExperienceName.Length > 0 : false));
    }
}
