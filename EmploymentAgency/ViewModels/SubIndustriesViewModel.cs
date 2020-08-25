using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SubIndustriesViewModel : BindableBase
    {
        private string _industryName;

        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public int? SelectedIdSubIndustry { get; set; }

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

        public ObservableCollection<object> SubIndustries { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<Industry> DisplayedIndustries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            IndustryName = "";

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

            UpdateDisplayedIndustries();
            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSubIndustry());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeSubIndustryViewModel.SelectedIdSubIndustry = (int)SelectedIdSubIndustry;

            WindowService.ShowWindow(new ChangeSubIndustry());
        }, () => SelectedIdSubIndustry != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSubIndustry((int)SelectedIdSubIndustry);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdSubIndustry != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        });

        private void ResetToDefault()
        {
            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;

            NameSubIndustry = "";

            Find();
        }

        private void Find()
        {
            SubIndustries = new ObservableCollection<object>(CollectionConverter<v_subIndustry>.ConvertToObjectList(_executor.GetSubIndustries(
                (SelectedIdIndustry != null ? (int)SelectedIdIndustry : -1), 
                NameSubIndustry)));
        }

        private void UpdateDisplayedIndustries()
        {
            DisplayedIndustries = new ObservableCollection<Industry>(Industries.Where(i => SearchAssistant.GetSearchAction(i.IndustryName, IndustryName, SearchAssistant.SearchType.Contains).Invoke()).Take(15).ToList());
        }
    }
}
