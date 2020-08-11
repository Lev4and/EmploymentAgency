using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeProfessionViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_profession _profession;

        public static int SelectedIdProfession { get; set; }


        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public ChangeProfessionViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _profession = _executor.GetProfessionExtendedInformation(SelectedIdProfession);

            NameProfessionCategory = _profession.NameProfessionCategory;
            ProfessionName = _profession.ProfessionName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if(_executor.UpdateProfession(SelectedIdProfession, ProfessionName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Профессия с такими данными уже существует");
            }
        }, () => (ProfessionName != null ? ProfessionName.Length > 0 : false));
    }
}
