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
        private int? _selectedIdProfessionCategory;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdProfessionCategory
        {
            get { return _selectedIdProfessionCategory; }
            set
            {
                _selectedIdProfessionCategory = value;

                IsCanChange = _selectedIdProfessionCategory != null ? true : false;
                IsCanRemove = _selectedIdProfessionCategory != null ? true : false;
            }
        }

        public string NameProfessionCategory { get; set; }

        public ObservableCollection<object> ProfessionCategories { get; set; }

        public ProfessionCategoriesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddProfessionCategory());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeProfessionCategoryViewModel.SelectedIdProfessionCategory = (int)SelectedIdProfessionCategory;

            WindowService.ShowWindow(new ChangeProfessionCategory());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveProfessionCategory((int)SelectedIdProfessionCategory);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

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
