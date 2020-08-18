using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class ChangeSupplementInformationForEmployerViewModel : BindableBase
    {
        private int? _selectedIdOrganization;

        private string _organizationName;
        private string _address;

        private QueryExecutor _executor;
        private ConfigurationUser _config;
        private v_employer _employer;

        public int? SelectedIdGender { get; set; }

        public int? SelectedIdOrganization
        {
            get { return _selectedIdOrganization; }
            set
            {
                _selectedIdOrganization = value;

                if (_selectedIdOrganization != null)
                {
                    UpdateBranches();
                    UpdateDisplayedBranches();
                }
            }
        }

        public int? SelectedIdBranch { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;

                if (_organizationName != null)
                {
                    if (_organizationName.Length == 0)
                    {
                        SelectedIdOrganization = null;
                        SelectedIdBranch = null;

                        if (SelectedIdOrganization != null)
                        {
                            UpdateBranches();
                            UpdateDisplayedBranches();
                        }
                        else
                        {
                            Branches = null;
                            DisplayedBranches = null;
                        }

                        Address = "";
                    }

                    if (Organizations != null)
                        UpdateDisplayedOrganizations();
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;

                if (_address != null)
                {
                    if (_address.Length == 0)
                    {
                        SelectedIdBranch = null;
                    }

                    if (Branches != null)
                        UpdateDisplayedBranches();
                }
            }
        }

        public byte[] Photo { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ObservableCollection<Gender> Genders { get; set; }

        public ObservableCollection<v_branchSimplifiedInformation> Branches { get; set; }

        public ObservableCollection<v_branchSimplifiedInformation> DisplayedBranches { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public ChangeSupplementInformationForEmployerViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();
            _config = ConfigurationUser.GetConfiguration();

            _employer = _executor.GetEmployerExtendedInformation(_config.IdUser);

            Name = _employer.Name;
            Surname = _employer.Surname;
            Patronymic = _employer.Patronymic;
            PhoneNumber = _employer.PhoneNumber;
            OrganizationName = _employer.OrganizationName;
            Address = _employer.AddressOrganization;
            Photo = _employer.Photo;
            DateOfBirth = _employer.DateOfBirth;

            SelectedIdGender = _employer.IdGender;
            SelectedIdOrganization = _employer.IdOrganization;
            SelectedIdBranch = _employer.IdBranch;

            Genders = new ObservableCollection<Gender>(_executor.GetGenders());
            Organizations = new ObservableCollection<v_organizationWithoutPhoto>(_executor.GetOrganizationsWithoutPhoto());
        });

        public ICommand ChangeInformation => new DelegateCommand(() =>
        {
            _executor.UpdateEmployer(_employer.IdEmployer, (int)SelectedIdBranch, Name, Surname, Patronymic, Photo, PhoneNumber);

            MessageBox.Show("Успешное изменение информации");
        }, () => SelectedIdOrganization != null &&
                 SelectedIdBranch != null &&
                 (Name != null ? Name.Length > 0 : false) &&
                 (Surname != null ? Surname.Length > 0 : false) &&
                 (Patronymic != null ? Patronymic.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));

        private void UpdateBranches()
        {
            Branches = new ObservableCollection<v_branchSimplifiedInformation>(_executor.GetBranches((int)SelectedIdOrganization).ToList());
        }

        private void UpdateDisplayedBranches()
        {
            DisplayedBranches = new ObservableCollection<v_branchSimplifiedInformation>(Branches.Where(b => b.Address.ToLower().StartsWith(Address.ToLower())).Take(15).ToList());
        }

        private void UpdateDisplayedOrganizations()
        {
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o => o.OrganizationName.ToLower().StartsWith(OrganizationName.ToLower())).Take(15).ToList());
        }
    }
}
