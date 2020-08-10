using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeIndustryViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Industry _industry;

        public static int SelectedIdIndustry { get; set; }

        
        public string IndustryName { get; set; }

        public ChangeIndustryViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _industry = _executor.GetIndustry(SelectedIdIndustry);

            IndustryName = _industry.IndustryName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateIndustry(SelectedIdIndustry, IndustryName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Отрасль с такими данными уже существует");
            }
        }, () => (IndustryName != null ? IndustryName.Length > 0 : false));
    }
}
