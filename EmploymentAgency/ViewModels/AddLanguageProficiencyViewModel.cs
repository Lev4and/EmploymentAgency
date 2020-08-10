using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddLanguageProficiencyViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string Designation { get; set; }

        public string LanguageProficiencyName { get; set; }

        public AddLanguageProficiencyViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddLanguageProficiency(Designation, LanguageProficiencyName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Уровень знания языка с такими данными уже существует");
            }
        }, () => (Designation != null ? Designation.Length > 0 : false) && (LanguageProficiencyName != null ? LanguageProficiencyName.Length > 0 : false));
    }
}
