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
    public class UsersViewModel : BindableBase
    {
        private int? _selectedIdUser;

        private QueryExecutor _executor;

        public bool IsCanAdd { get; set; } = true;

        public bool IsCanChange { get; set; }

        public bool IsCanRemove { get; set; }

        public int? SelectedIdRole { get; set; }

        public int? SelectedIdUser
        {
            get { return _selectedIdUser; }
            set
            {
                _selectedIdUser = value;

                IsCanChange = _selectedIdUser != null ? true : false;
                IsCanRemove = _selectedIdUser != null ? true : false;
            }
        }

        public string Login { get; set; }

        public ObservableCollection<object> Users { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public UsersViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            Roles = new ObservableCollection<Role>(_executor.GetRoles());

            ResetToDefault();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            WindowService.ShowWindow(new AddUser());
        }, () => IsCanAdd == true);

        public ICommand Change => new DelegateCommand(() =>
        {
            if(_executor.IsRelatedToStaff((int)SelectedIdUser))
            {
                ChangeUserViewModel.SelectedIdUser = (int)SelectedIdUser;

                WindowService.ShowWindow(new ChangeUser());
            }
            else
            {
                MessageBox.Show("Нельзя менять данные пользователей неработающих в этой компанией", "Предупреждение");
            }
        }, () => IsCanChange);

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _executor.RemoveUser((int)SelectedIdUser);

                MessageBox.Show("Успешное удаление");

                Find();
            }
        }, () => IsCanRemove);

        public ICommand ToFind => new DelegateCommand(() =>
        {
            Find();
        });

        public ICommand ResetFilters => new DelegateCommand(() =>
        {
            ResetToDefault();
        }, () => SelectedIdRole != null || (Login != null ? Login.Length > 0 : false));

        private void ResetToDefault()
        {
            SelectedIdRole = null;
            SelectedIdUser = null;

            Login = "";

            Find();
        }

        private void Find()
        {
            Users = new ObservableCollection<object>(CollectionConverter<v_user>.ConvertToObjectList(_executor.GetUsers(
                (SelectedIdRole != null ? (int)SelectedIdRole : -1),
                Login)));
        }
    }
}
