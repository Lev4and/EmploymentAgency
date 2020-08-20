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
    public class IndustriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdIndustry { get; set; }

        public string IndustryName { get; set; }

        public ObservableCollection<object> Industries { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddIndustry());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeIndustryViewModel.SelectedIdIndustry = (int)SelectedIdIndustry;

            WindowService.ShowWindow(new ChangeIndustry());
        }, () => SelectedIdIndustry != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveIndustry((int)SelectedIdIndustry);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdIndustry != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdIndustry = null;

            IndustryName = "";

            Find();
        }

        private void Find()
        {
            Industries = new ObservableCollection<object>(CollectionConverter<Industry>.ConvertToObjectList(_executor.GetIndustries(IndustryName)));
        }
    }
}
