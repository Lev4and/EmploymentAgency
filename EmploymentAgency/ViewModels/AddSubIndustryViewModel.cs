using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddSubIndustryViewModel : BindableBase
    {
        private string _industryName;

        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public string IndustryName
        {
            get { return _industryName; }
            set
            {
                _industryName = value;

                if (_industryName != null)
                {
                    if (_industryName.Length == 0)
                    {
                        SelectedIdIndustry = null;
                    }
                }

                if (Industries != null)
                {
                    UpdateDisplayedIndustries();
                }
            }
        }

        public string NameSubIndustry { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<Industry> DisplayedIndustries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            IndustryName = "";

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

            UpdateDisplayedIndustries();
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

        private void UpdateDisplayedIndustries()
        {
            DisplayedIndustries = new ObservableCollection<Industry>(Industries.Where(i => i.IndustryName.ToLower().StartsWith(IndustryName.ToLower())).Take(20).ToList());
        }
    }
}
