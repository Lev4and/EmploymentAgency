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
    public class RolesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRole { get; set; }

        public string RoleName { get; set; }

        public ObservableCollection<object> Roles { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddRole());
        }, () => true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeRoleViewModel.SelectedIdRole = (int)SelectedIdRole;

            WindowService.ShowWindow(new ChangeRole());
        }, () => SelectedIdRole != null ? true : false);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveRole((int)SelectedIdRole);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => SelectedIdRole != null ? true : false);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        private void ResetToDefault()
        {
            SelectedIdRole = null;

            RoleName = "";

            Find();
        }

        private void Find()
        {
            Roles = new ObservableCollection<object>(CollectionConverter<Role>.ConvertToObjectList(_executor.GetRoles(RoleName)));
        }
    }
}
