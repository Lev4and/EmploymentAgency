using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeLanguageViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Language _language;

        public static int SelectedIdLanguage { get; set; }


        public string LanguageName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _language = _executor.GetLanguage(SelectedIdLanguage);

            LanguageName = _language.LanguageName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateLanguage(SelectedIdLanguage, LanguageName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Язык с такими данными уже существует");
            }
        }, () => (LanguageName != null ? LanguageName.Length > 0 : false));
    }
}
