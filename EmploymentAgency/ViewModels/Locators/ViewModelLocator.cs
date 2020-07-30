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

            services.AddSingleton<PageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
                _provider.GetRequiredService(item.ServiceType);
        }
    }
}
