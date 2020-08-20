using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddIndustryViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string IndustryName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddIndustry(IndustryName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Отрасль с такими данными уже существует");
            }
        }, () => (IndustryName != null ? IndustryName.Length > 0 : false));
    }
}
