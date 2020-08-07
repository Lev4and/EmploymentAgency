using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class RolesViewModel : BindableBase
    {
        private QueryExecutor _executor;

        public int? SelectedIdRole { get; set; }

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
