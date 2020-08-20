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
    public class ProfessionCategoriesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdProfessionCategory { get; set; }

        public string NameProfessionCategory { get; set; }

        public ObservableCollection<object> ProfessionCategories { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddProfessionCategory());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeProfessionCategoryViewModel.SelectedIdProfessionCategory = (int)SelectedIdProfessionCategory;

            WindowService.ShowWindow(new ChangeProfessionCategory());
        }, () => SelectedIdProfessionCategory != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveProfessionCategory((int)SelectedIdProfessionCategory);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdProfessionCategory != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdProfessionCategory = null;

            NameProfessionCategory = "";

            Find();
        }

        private void Find()
        {
            ProfessionCategories = new ObservableCollection<object>(CollectionConverter<ProfessionCategory>.ConvertToObjectList(_executor.GetProfessionCategories(NameProfessionCategory)));
        }
    }
}
