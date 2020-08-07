using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class UsersViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRole { get; set; }

        public int? SelectedIdUser { get; set; }

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
