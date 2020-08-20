using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddOrganizationViewModel : BindableBase
    {
        private int? _selectedIdIndustry;

        private string _industryName;
        private string _nameSubIndustry;

        private QueryExecutor _executor;

        public int? SelectedIdIndustry
        {
            get { return _selectedIdIndustry; }
            set
            {
                _selectedIdIndustry = value;

                if (_selectedIdIndustry != null)
                    UpdateSubIndustries();
                else
                    SubIndustries = null;
            }
        }

        public int? SelectedIdSubIndustry { get; set; }

        public string OrganizationName { get; set; }

        public string IndustryName
        {
            get { return _industryName; }
            set
            {
                _industryName = value;

                if(_industryName != null)
                {
                    if (_industryName.Length == 0)
                    {
                        SelectedIdIndustry = null;
                        SelectedIdSubIndustry = null;

                        IndustryName = "";
                        NameSubIndustry = "";
                    }
                }
            }
        }

        public string NameSubIndustry
        {
            get { return _nameSubIndustry; }
            set
            {
                _nameSubIndustry = value;

                if(_nameSubIndustry != null)
                {
                    if (_nameSubIndustry.Length == 0)
                    {
                        SelectedIdSubIndustry = null;
                    }
                }
            }
        }

        public byte[] Photo { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<SubIndustry> SubIndustries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;

            IndustryName = "";
            NameSubIndustry = "";
            OrganizationName = "";

            Photo = null;

            SubIndustries = new ObservableCollection<SubIndustry>();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(_executor.AddOrganization((int)SelectedIdSubIndustry, OrganizationName, Photo))
            {
                MessageBox.Show("Успешное добавление");
            }
            else
            {
                MessageBox.Show("Организация с такими данными уже существует");
            }
        }, () => SelectedIdSubIndustry != null &&
                 (OrganizationName != null ? OrganizationName.Length > 0 : false));

        private void UpdateSubIndustries()
        {
            SubIndustries = new ObservableCollection<SubIndustry>(_executor.GetSubIndustries((int)SelectedIdIndustry));
        }
    }
}
