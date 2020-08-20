using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeEducationViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Education _education;

        public static int SelectedIdEducation;


        public string EducationName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _education = _executor.GetEducation(SelectedIdEducation);

            EducationName = _education.EducationName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateEducation(SelectedIdEducation, EducationName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Образование с такими данными уже существует");
            }
        }, () => (EducationName != null ? EducationName.Length > 0 : false));
    }
}
