using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeGenderViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private Gender _gender;

        public static int SelectedIdGender { get; set; }


        public string GenderName { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _gender = _executor.GetGender(SelectedIdGender);

            GenderName = _gender.GenderName;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateGender(SelectedIdGender, GenderName))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Пол с такими данными уже существует");
            }
        }, () => (GenderName != null ? GenderName.Length > 0 : false));
    }
}
