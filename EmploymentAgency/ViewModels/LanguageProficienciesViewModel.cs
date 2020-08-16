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
    public class LanguageProficienciesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdLanguageProficiency { get; set; }

        public string LanguageProficiencyName { get; set; }

        public ObservableCollection<object> LanguageProficiencies { get; set; }

        public LanguageProficienciesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddLanguageProficiency());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeLanguageProficiencyViewModel.SelectedIdLanguageProficiency = (int)SelectedIdLanguageProficiency;

            WindowService.ShowWindow(new ChangeLanguageProficiency());
        }, () => SelectedIdLanguageProficiency != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveLanguageProficiency((int)SelectedIdLanguageProficiency);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdLanguageProficiency != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdLanguageProficiency = null;

            LanguageProficiencyName = "";

            Find();
        }

        private void Find()
        {
            LanguageProficiencies = new ObservableCollection<object>(CollectionConverter<LanguageProficiency>.ConvertToObjectList(_executor.GetLanguageProficiencies(LanguageProficiencyName)));
        }
    }
}
