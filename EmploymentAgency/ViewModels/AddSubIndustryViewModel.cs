using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddSubIndustryViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public string IndustryName { get; set; }

        public string NameSubIndustry { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddSubIndustry((int)SelectedIdIndustry, NameSubIndustry))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Подотрасль с такими данными уже существует");
            }
        }, () => SelectedIdIndustry != null && 
                 (NameSubIndustry != null ? NameSubIndustry.Length > 0 : false));
    }
}
