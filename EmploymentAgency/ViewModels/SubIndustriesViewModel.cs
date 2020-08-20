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
        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public int? SelectedIdSubIndustry { get; set; }

        public string NameSubIndustry { get; set; }

        public ObservableCollection<object> SubIndustries { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());

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
    }
}
