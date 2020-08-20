using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddGenderViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public string GenderName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if (_executor.AddGender(GenderName))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Пол с такими данными уже существует");
            }
        }, () => (GenderName != null ? GenderName.Length > 0 : false));
    }
}
