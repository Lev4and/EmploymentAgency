using EmploymentAgency.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EmploymentAgency.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainViewModel>();

            services.AddSingleton<PageService>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
                _provider.GetRequiredService(item.ServiceType);
        }
    }
}
