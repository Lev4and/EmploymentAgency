using DevExpress.Mvvm;
using EmploymentAgency.Model.Configurations;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Model.Logic;
using EmploymentAgency.Services;
using EmploymentAgency.Views.Pages;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class SupplementingInformationForEmployerViewModel : BindableBase
    {
        private int? _selectedIdOrganization;

        private string _organizationName;
        private string _address;

        private readonly PageService _pageService;
        private ConfigurationUser _config;
        private QueryExecutor _executor;

        public int? SelectedIdGender { get; set; }

        public int? SelectedIdOrganization
        {
            get { return _selectedIdOrganization; }
            set
            {
                _selectedIdOrganization = value;

                if(_selectedIdOrganization != null)
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

                if(_organizationName != null)
                {
                    if(_organizationName.Length == 0)
                    {
                        SelectedIdOrganization = null;
                        SelectedIdBranch = null;

                        if(SelectedIdOrganization != null)
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

                    if(Organizations != null)
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

                if(_address != null)
                {
                    if(_address.Length == 0)
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

        public DateTime MinValueDateOfBirth { get; set; }

        public DateTime MaxValueDateOfBirth { get; set; }

        public ObservableCollection<Gender> Genders { get; set; }

        public ObservableCollection<v_branchSimplifiedInformation> Branches { get; set; }

        public ObservableCollection<v_branchSimplifiedInformation> DisplayedBranches { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public SupplementingInformationForEmployerViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _config = ConfigurationUser.GetConfiguration();
            _executor = new QueryExecutor();

            SelectedIdGender = null;
            SelectedIdOrganization = null;
            SelectedIdBranch = null;

            Name = "";
            Surname = "";
            Patronymic = "";
            PhoneNumber = "";
            OrganizationName = "";
            Address = "";

            Photo = null;

            MinValueDateOfBirth = new DateTime(1900, 1, 1);
            MaxValueDateOfBirth = DateTime.Now.Date.AddYears(-18);

            DateOfBirth = MaxValueDateOfBirth;

            Genders = new ObservableCollection<Gender>(_executor.GetGenders());
            Organizations = new ObservableCollection<v_organizationWithoutPhoto>(_executor.GetOrganizationsWithoutPhoto());

            UpdateDisplayedOrganizations();
        });

        public ICommand AddInformation => new DelegateCommand(() =>
        {
            if(_executor.AddEmployer(_config.IdUser, (int)SelectedIdBranch, Name, Surname, Patronymic, (int)SelectedIdGender, Photo, DateOfBirth, PhoneNumber))
            {
                MessageBox.Show("Успешное добавление информации");

                _pageService.ChangePage(new Menu());
            }
            else
            {
                MessageBox.Show("Информация о данном пользователе уже существует");
            }
        }, () => SelectedIdGender != null &&
                 SelectedIdOrganization != null &&
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
            DisplayedBranches = new ObservableCollection<v_branchSimplifiedInformation>(Branches.Where(b => SearchAssistant.GetSearchAction(b.Address, Address, SearchAssistant.SearchType.Contains).Invoke()).Take(15).ToList());
        }

        private void UpdateDisplayedOrganizations()
        {
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o => SearchAssistant.GetSearchAction(o.OrganizationName, OrganizationName, SearchAssistant.SearchType.Contains).Invoke()).Take(15).ToList());
        }
    }
}
