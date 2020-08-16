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
    public class ProfessionsViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdProfession { get; set; }

        public int? SelectedIdProfessionCategory { get; set; }

        public string ProfessionName { get; set; }

        public ObservableCollection<object> Professions { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ProfessionsViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddProfession());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeProfessionViewModel.SelectedIdProfession = (int)SelectedIdProfession;

            WindowService.ShowWindow(new ChangeProfession());
        }, () => SelectedIdProfession != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveProfession((int)SelectedIdProfession);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdProfession != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdProfessionCategory != null || (ProfessionName != null ? ProfessionName.Length > 0 : false));

        private void ResetToDefault()
        {
            SelectedIdProfession = null;
            SelectedIdProfessionCategory = null;

            ProfessionName = "";

            Find();
        }

        private void Find()
        {
            Professions = new ObservableCollection<object>(CollectionConverter<v_profession>.ConvertToObjectList(_executor.GetProfessions(
                (SelectedIdProfessionCategory != null ? (int)SelectedIdProfessionCategory : -1),
                ProfessionName)));
        }
    }
}
