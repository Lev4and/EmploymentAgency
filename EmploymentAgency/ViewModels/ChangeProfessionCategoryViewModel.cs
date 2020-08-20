using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeProfessionCategoryViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ProfessionCategory _professionCategory;

        public static int SelectedIdProfessionCategory { get; set; }


        public string NameProfessionCategory { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _professionCategory = _executor.GetProfessionCategory(SelectedIdProfessionCategory);

            NameProfessionCategory = _professionCategory.NameProfessionCategory;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateProfessionCategory((int)SelectedIdProfessionCategory, NameProfessionCategory))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Сфера деятельности с такими данными уже существует");
            }
        }, () => (NameProfessionCategory != null ? NameProfessionCategory.Length > 0 : false));
    }
}
