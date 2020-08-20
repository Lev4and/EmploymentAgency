using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddEducationViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string EducationName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddEducation(EducationName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Образование с такими данными уже существует");
            }
        }, () => (EducationName != null ? EducationName.Length > 0 : false));
    }
}
