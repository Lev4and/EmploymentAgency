using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddProfessionCategoryViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string NameProfessionCategory { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddProfessionCategory(NameProfessionCategory))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Сфера деятельности с такими данными уже существует");
            }
        }, () => (NameProfessionCategory != null ? NameProfessionCategory.Length > 0 : false));
    }
}
