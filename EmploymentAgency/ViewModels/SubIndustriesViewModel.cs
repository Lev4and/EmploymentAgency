using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SubIndustriesViewModel : BindableBase
    {
        private int? _selectedIdSubIndustry;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdIndustry { get; set; }

        public int? SelectedIdSubIndustry
        {
            get { return _selectedIdSubIndustry; }
            set
            {
                _selectedIdSubIndustry = value;

                IsCanChange = _selectedIdSubIndustry != null ? true : false;
                IsCanRemove = _selectedIdSubIndustry != null ? true : false;
            }
        }

        public string NameSubIndustry { get; set; }

        public ObservableCollection<object> SubIndustries { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public SubIndustriesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddSubIndustry());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeSubIndustryViewModel.SelectedIdSubIndustry = (int)SelectedIdSubIndustry;

            WindowService.ShowWindow(new ChangeSubIndustry());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveSubIndustry((int)SelectedIdSubIndustry);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdIndustry != null || (NameSubIndustry != null ? NameSubIndustry.Length > 0 : false));

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
    }
}
