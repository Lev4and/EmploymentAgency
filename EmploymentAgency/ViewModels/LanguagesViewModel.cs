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
        private int? _selectedIdLanguage;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdLanguage
        {
            get { return _selectedIdLanguage; }
            set
            {
                _selectedIdLanguage = value;

                IsCanChange = _selectedIdLanguage != null ? true : false;
                IsCanRemove = _selectedIdLanguage != null ? true : false;
            }
        }

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
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeLanguageViewModel.SelectedIdLanguage = (int)SelectedIdLanguage;

            WindowService.ShowWindow(new ChangeLanguage());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveLanguage((int)SelectedIdLanguage);

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
