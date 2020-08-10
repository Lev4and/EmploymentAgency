using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeLanguageProficiencyViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private LanguageProficiency _languageProficiency;

        public static int SelectedIdLanguageProficiency { get; set; }

        
        public string Designation { get; set; }

        public string LanguageProficiencyName { get; set; }

        public ChangeLanguageProficiencyViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _languageProficiency = _executor.GetLanguageProficiency(SelectedIdLanguageProficiency);

            Designation = _languageProficiency.Designation;
            LanguageProficiencyName = _languageProficiency.LanguageProficiencyName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateLanguageProficiency(SelectedIdLanguageProficiency, Designation, LanguageProficiencyName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Уровень знания языка с такими данными уже существует");
            }
        }, () => (Designation != null ? Designation.Length > 0 : false) && (LanguageProficiencyName != null ? LanguageProficiencyName.Length > 0 : false));
    }
}
