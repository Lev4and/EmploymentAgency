using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSupplementInformationForManagerViewModel : BindableBase
    {
        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_manager _manager;

        public int? SelectedIdGender { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ObservableCollection<Gender> Genders { get; set; }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _manager = _executor.GetManagerExtendedInformation(_config.IdUser);

            SelectedIdGender = _manager.IdGender;

            Name = _manager.Name;
            Surname = _manager.Surname;
            Patronymic = _manager.Patronymic;
            PhoneNumber = _manager.PhoneNumber;
            DateOfBirth = _manager.DateOfBirth;

            Genders = new ObservableCollection<Gender>(_executor.GetGenders());
        });

        public ICommand ChangeInformation => new DelegateCommand(() =>
        {
            _executor.UpdateManager(_manager.IdManager, Name, Surname, Patronymic, Photo, PhoneNumber);

            MessageBox.Show("Успешное изменение информации");
        }, () => (Name != null ? Name.Length > 0 : false) &&
                 (Surname != null ? Surname.Length > 0 : false) &&
                 (Patronymic != null ? Patronymic.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));
    }
}
