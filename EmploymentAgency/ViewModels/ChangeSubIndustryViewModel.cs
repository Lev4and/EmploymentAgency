using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSubIndustryViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private v_subIndustry _subIndustry;

        public static int SelectedIdSubIndustry { get; set; }


        public string IndustryName { get; set; }

        public string NameSubIndustry { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _subIndustry = _executor.GetSubIndustryExtendedInformation(SelectedIdSubIndustry);

            IndustryName = _subIndustry.IndustryName;
            NameSubIndustry = _subIndustry.NameSubIndustry;
        });

        public ICommand Change => new DelegateCommand(() =>
        {
            if (_executor.UpdateSubIndustry(_subIndustry.IdSubIndustry, NameSubIndustry))
            {
                MessageBox.Show("Успешное изменение");
            }
            else
            {
                MessageBox.Show("Подотрасль с такими данными уже существует");
            }
        }, () => (NameSubIndustry != null ? NameSubIndustry.Length > 0 : false));
    }
}
