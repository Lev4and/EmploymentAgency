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
        private int? _selectedIdRole;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdRole
        {
            get { return _selectedIdRole; }
            set
            {
                _selectedIdRole = value;

                IsCanChange = _selectedIdRole != null ? true : false;
                IsCanRemove = _selectedIdRole != null ? true : false;
            }
        }

        public string RoleName { get; set; }

        public ObservableCollection<object> Roles { get; set; }

        public RolesViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddRole());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            ChangeRoleViewModel.SelectedIdRole = (int)SelectedIdRole;

            WindowService.ShowWindow(new ChangeRole());
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveRole((int)SelectedIdRole);

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
