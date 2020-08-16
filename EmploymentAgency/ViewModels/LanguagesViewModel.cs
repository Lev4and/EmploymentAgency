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
    public class LanguagesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdLanguage { get; set; }

        public string LanguageName { get; set; }

        public ObservableCollection<object> Languages { get; set; }

        public LanguagesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddLanguage());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeLanguageViewModel.SelectedIdLanguage = (int)SelectedIdLanguage;

            WindowService.ShowWindow(new ChangeLanguage());
        }, () => SelectedIdLanguage != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveLanguage((int)SelectedIdLanguage);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdLanguage != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdLanguage = null;

            LanguageName = "";

            Find();
        }

        private void Find()
        {
            Languages = new ObservableCollection<object>(CollectionConverter<Language>.ConvertToObjectList(_executor.GetLanguages(LanguageName)));
        }
    }
}
