using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class MyRequestsViewModel : BindableBase
    {
        private int? _selectedIdProfessionCategory;

        private QueryExecutor _executor;
        private ConfigurationUser _config;

        public int? SelectedIdProfessionCategory
        {
            get { return _selectedIdProfessionCategory; }
            set
            {
                _selectedIdProfessionCategory = value;

                if (_selectedIdProfessionCategory != null)
                    UpdateProfessions();
                else
                    Professions = null;
            }
        }

        public int? SelectedIdProfession { get; set; }

        public int? SelectedIdRequest { get; set; }

        public string ProfessionName { get; set; }

        public ObservableCollection<object> MyRequests { get; set; }

        public ObservableCollection<ProfessionCategory> ProfessionCategories { get; set; }

        public ObservableCollection<Profession> Professions { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddRequest());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {

        }, () => SelectedIdRequest != null);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdRequest != null);

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
            SelectedIdRequest = null;
            SelectedIdProfessionCategory = null;
            SelectedIdProfession = null;

            ProfessionName = "";

            ProfessionCategories = new ObservableCollection<ProfessionCategory>(_executor.GetProfessionCategories());

            Find();
        }

        private void Find()
        {

        }

        private void UpdateProfessions()
        {
            Professions = new ObservableCollection<Profession>(_executor.GetProfessions((int)SelectedIdProfessionCategory));
        }
    }
}
