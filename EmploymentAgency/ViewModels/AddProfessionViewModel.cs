using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddProfessionViewModel : BindableBase
    {
        private string _nameProfessionCategory;

        private QueryExecutor _executor;

        public int? SelectedIdProfessionCategory { get; set; }

        public string NameProfessionCategory
        {
            get { return _nameProfessionCategory; }
            set
            {
                _nameProfessionCategory = value;

                if (_nameProfessionCategory != null)
                {
                    if (_nameProfessionCategory.Length == 0)
                    {
                        SelectedIdProfessionCategory = null;

                        ProfessionName = "";
                    }
                }

                if (ProfessionCategories != null)
                {
                    UpdateDisplayedProfessionCategories();
                }
            }
        }

        public string ProfessionName { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<ProfessionCategory> DisplayedProfessionCategories { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            NameProfessionCategory = "";

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            UpdateDisplayedProfessionCategories();
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

        private void UpdateDisplayedProfessionCategories()
        {
            DisplayedProfessionCategories = new ObservableCollection<ProfessionCategory>(ProfessionCategories.Where(p => p.NameProfessionCategory.ToLower().StartsWith(NameProfessionCategory.ToLower())).Take(20).ToList());
        }
    }
}
