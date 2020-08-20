using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddLanguageViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string LanguageName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddLanguage(LanguageName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Язык с такими данными уже существует");
            }
        }, () => (LanguageName != null ? LanguageName.Length > 0 : false));
    }
}
