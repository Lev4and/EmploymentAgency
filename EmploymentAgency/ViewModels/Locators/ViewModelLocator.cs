using EmploymentAgency.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmploymentAgency.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();

        public AuthorizationViewModel AuthorizationViewModel => _provider.GetRequiredService<AuthorizationViewModel>();

        public SettingsViewModel SettingsViewModel => _provider.GetRequiredService<SettingsViewModel>();

        public SupplementingInformationForManagerViewModel SupplementingInformationForManagerViewModel => _provider.GetRequiredService<SupplementingInformationForManagerViewModel>();

        public AddOrganizationViewModel AddOrganizationViewModel => _provider.GetRequiredService<AddOrganizationViewModel>();

        public SupplementingInformationForEmployerViewModel SupplementingInformationForEmployerViewModel => _provider.GetRequiredService<SupplementingInformationForEmployerViewModel>();

        public AddSkillViewModel AddSkillViewModel => _provider.GetRequiredService<AddSkillViewModel>();

        public SupplementingInformationForApplicantViewModel SupplementingInformationForApplicantViewModel => _provider.GetRequiredService<SupplementingInformationForApplicantViewModel>();

        public RegistrationViewModel RegistrationViewModel => _provider.GetRequiredService<RegistrationViewModel>();

        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();

        public OrganizationsViewModel OrganizationsViewModel => _provider.GetRequiredService<OrganizationsViewModel>();

        public BranchesViewModel BranchesViewModel => _provider.GetRequiredService<BranchesViewModel>();

        public CountriesViewModel CountriesViewModel => _provider.GetRequiredService<CountriesViewModel>();

        public CitiesViewModel CitiesViewModel => _provider.GetRequiredService<CitiesViewModel>();

        public StreetsViewModel StreetsViewModel => _provider.GetRequiredService<StreetsViewModel>();

        public SkillsViewModel SkillsViewModel => _provider.GetRequiredService<SkillsViewModel>();

        public DrivingLicenseCategoriesViewModel DrivingLicenseCategoriesViewModel => _provider.GetRequiredService<DrivingLicenseCategoriesViewModel>();

        public EducationsViewModel EducationsViewModel => _provider.GetRequiredService<EducationsViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SupplementingInformationForManagerViewModel>();
            services.AddTransient<AddOrganizationViewModel>();
            services.AddTransient<SupplementingInformationForEmployerViewModel>();
            services.AddTransient<AddSkillViewModel>();
            services.AddTransient<SupplementingInformationForApplicantViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<OrganizationsViewModel>();
            services.AddTransient<BranchesViewModel>();
            services.AddTransient<CountriesViewModel>();
            services.AddTransient<CitiesViewModel>();
            services.AddTransient<StreetsViewModel>();
            services.AddTransient<SkillsViewModel>();
            services.AddTransient<DrivingLicenseCategoriesViewModel>();
            services.AddTransient<EducationsViewModel>();

            services.AddSingleton<PageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
                _provider.GetRequiredService(item.ServiceType);
        }
    }
}
