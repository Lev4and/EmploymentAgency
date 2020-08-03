using Converters;
using DevExpress.Mvvm;
using EmploymentAgency.Model.Database.Interactions;
using EmploymentAgency.Model.Database.Models;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace EmploymentAgency.ViewModels
{
    public class AddOrganizationViewModel : BindableBase
    {
        private int? _selectedIdIndustry;
        private int? _selectedIdSubIndustry;
        private int? _selectedIdCountry;
        private int? _selectedIdCity;
        private int? _selectedIdOrganization;

        private string _industryName;
        private string _nameSubIndustry;
        private string _countryName;
        private string _cityName;
        private string _streetName;
        private string _organizationName;

        private QueryExecutor _executor;

        public int? SelectedIdIndustry
        {
            get { return _selectedIdIndustry; }
            set
            {
                _selectedIdIndustry = value;

                if (_selectedIdIndustry != null)
                    UpdateSubIndustries();
                else
                    SubIndustries = null;
            }
        }

        public int? SelectedIdSubIndustry
        {
            get { return _selectedIdSubIndustry; }
            set
            {
                _selectedIdSubIndustry = value;

                UpdateDisplayedOrganizations();
            }
        }

        public int? SelectedIdCountry
        {
            get { return _selectedIdCountry; }
            set
            {
                _selectedIdCountry = value;

                if (_selectedIdCountry != null)
                {
                    UpdateCities();

                    Streets = null;
                }
                else
                {
                    Cities = null;
                }
            }
        }

        public int? SelectedIdCity
        {
            get { return _selectedIdCity; }
            set
            {
                _selectedIdCity = value;

                if (_selectedIdCity != null)
                {
                    UpdateStreets();
                    UpdateDisplayedStreets();
                }
                else
                {
                    Streets = null;
                    DisplayedStreets = null;
                }
            }
        }

        public int? SelectedIdStreet { get; set; }

        public int? SelectedIdOrganization
        {
            get { return _selectedIdOrganization; }
            set
            {
                _selectedIdOrganization = value;

                if (_selectedIdOrganization != null)
                    Photo = _executor.GetOrganization((int)_selectedIdOrganization).Photo;
                else
                    Photo = null;
            }
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set
            {
                _organizationName = value;

                if(_organizationName != null)
                {
                    if (_organizationName.Length == 0)
                        Photo = null;
                }

                if(Organizations != null)
                    UpdateDisplayedOrganizations();
            }
        }

        public string NameHouse { get; set; }

        public string PhoneNumber { get; set; }

        public string IndustryName
        {
            get { return _industryName; }
            set
            {
                _industryName = value;

                if(_industryName != null)
                {
                    if (_industryName.Length == 0)
                    {
                        SelectedIdIndustry = null;
                        SelectedIdSubIndustry = null;
                        SelectedIdOrganization = null;

                        IndustryName = "";
                        NameSubIndustry = "";
                        OrganizationName = "";

                        Photo = null;
                    }
                }
            }
        }

        public string NameSubIndustry
        {
            get { return _nameSubIndustry; }
            set
            {
                _nameSubIndustry = value;

                if(_nameSubIndustry != null)
                {
                    if (_nameSubIndustry.Length == 0)
                    {
                        SelectedIdSubIndustry = null;
                        SelectedIdOrganization = null;

                        OrganizationName = "";

                        Photo = null;
                    }
                }
            }
        }

        public string CountryName
        {
            get { return _countryName; }
            set
            {
                _countryName = value;

                if(_countryName != null)
                {
                    if (_countryName.Length == 0)
                    {
                        SelectedIdCountry = null;
                        SelectedIdCity = null;
                        SelectedIdStreet = null;

                        CityName = "";
                        StreetName = "";
                    }
                }
            }
        }

        public string CityName
        {
            get { return _cityName; }
            set
            {
                _cityName = value;

                if(_cityName != null)
                {
                    if (_cityName.Length == 0)
                    {
                        SelectedIdCity = null;
                        SelectedIdStreet = null;

                        StreetName = "";
                    }
                }
            }
        }

        public string StreetName
        {
            get { return _streetName; }
            set
            {
                _streetName = value;

                if(_streetName != null)
                {
                    if (_streetName.Length == 0)
                    {
                        SelectedIdStreet = null;

                        StreetName = "";
                    }
                }

                if(Streets != null)
                {
                    UpdateDisplayedStreets();
                }
            }
        }

        public byte[] Photo { get; set; }

        public ObservableCollection<Industry> Industries { get; set; }

        public ObservableCollection<SubIndustry> SubIndustries { get; set; }

        public ObservableCollection<Country> Countries { get; set; }

        public ObservableCollection<City> Cities { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> Organizations { get; set; }

        public ObservableCollection<Street> Streets { get; set; }

        public ObservableCollection<Street> DisplayedStreets { get; set; }

        public ObservableCollection<v_organizationWithoutPhoto> DisplayedOrganizations { get; set; }

        public AddOrganizationViewModel()
        {

        }

        public ICommand Loaded => new DelegateCommand(() =>
        {
            _executor = new QueryExecutor();

            SelectedIdIndustry = null;
            SelectedIdSubIndustry = null;
            SelectedIdOrganization = null;
            SelectedIdCountry = null;
            SelectedIdCity = null;
            SelectedIdStreet = null;

            IndustryName = "";
            NameSubIndustry = "";
            OrganizationName = "";
            CountryName = "";
            CityName = "";
            NameHouse = "";
            PhoneNumber = "";

            Photo = null;

            SubIndustries = new ObservableCollection<SubIndustry>();
            Cities = new ObservableCollection<City>();
            Streets = new ObservableCollection<Street>();

            Industries = new ObservableCollection<Industry>(_executor.GetIndustries());
            Countries = new ObservableCollection<Country>(_executor.GetCountries());

            UpdateOrganizations();
            UpdateDisplayedOrganizations();
        });

        public ICommand Add => new DelegateCommand(() =>
        {
            if(SelectedIdOrganization != null)
            {
                if(_executor.AddBranch((int)SelectedIdOrganization, (int)SelectedIdStreet, NameHouse, PhoneNumber))
                {
                    MessageBox.Show("Успешное добавление");

                    UpdateDisplayedOrganizations();
                }
                else
                {
                    MessageBox.Show("Организация с таким данными уже существует");
                }
            }
            else
            {
                Organization organization = null;

                if(_executor.AddOrganization((int)SelectedIdSubIndustry, OrganizationName, Photo, out organization))
                {
                    _executor.AddBranch(organization.IdOrganization, (int)SelectedIdStreet, NameHouse, PhoneNumber);

                    MessageBox.Show("Успешное добавление");

                    UpdateOrganizations();
                    UpdateDisplayedOrganizations();
                }
                else
                {
                    MessageBox.Show("Организация с таким данными уже существует");
                }
            }
        }, () => SelectedIdSubIndustry != null &&
                 SelectedIdStreet != null &&
                 (OrganizationName != null ? OrganizationName.Length > 0 : false) &&
                 (NameHouse != null ? NameHouse.Length > 0 : false) &&
                 (PhoneNumber != null ? PhoneNumber.Length > 0 : false));

        public ICommand AddPhoto => new DelegateCommand(() =>
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.jpeg, *.png, *webp)|*.bmp;*.jpg;*.jpeg;*.png;*.webp";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                Photo = FileConverter.GetBytesFromFile(openFileDialog.FileName);
            }
        }, () => Photo == null);

        public ICommand RemovePhoto => new DelegateCommand(() =>
        {
            Photo = null;
        }, () => Photo != null);

        private void UpdateCities()
        {
            Cities = new ObservableCollection<City>(_executor.GetCities((int)SelectedIdCountry));
        }

        private void UpdateStreets()
        {
            Streets = new ObservableCollection<Street>(_executor.GetStreets((int)SelectedIdCity));
        }

        private void UpdateSubIndustries()
        {
            SubIndustries = new ObservableCollection<SubIndustry>(_executor.GetSubIndustries((int)SelectedIdIndustry));
        }

        private void UpdateOrganizations()
        {
            Organizations = new ObservableCollection<v_organizationWithoutPhoto>(_executor.GetOrganizationsWithoutPhoto());
        }

        private void UpdateDisplayedStreets()
        {
            DisplayedStreets = new ObservableCollection<Street>(Streets.Where(s => s.StreetName.ToLower().StartsWith(StreetName.ToLower())).Take(100).ToList());
        }

        private void UpdateDisplayedOrganizations()
        {
            DisplayedOrganizations = new ObservableCollection<v_organizationWithoutPhoto>(Organizations.Where(o =>
                                                                                                             (OrganizationName.Length > 0 ? o.OrganizationName.ToLower().StartsWith(OrganizationName.ToLower()) : true) &&
                                                                                                             (SelectedIdSubIndustry != null ? o.IdSubIndustry == (int)SelectedIdSubIndustry : false)).Take(100).ToList());
        }
    }
}
