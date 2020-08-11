using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddProfessionViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdProfessionCategory { get; set; }

        public string NameProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public AddProfessionViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(_executor.AddProfession((int)SelectedIdProfessionCategory, ProfessionName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Профессия с такими данными уже существует");
            }
        }, () => SelectedIdProfessionCategory != null &&
                 (ProfessionName != null ? ProfessionName.Length > 0 : false));
    }
}
