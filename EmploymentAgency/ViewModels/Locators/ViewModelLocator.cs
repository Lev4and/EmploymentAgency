using Microsoft.Extensions.DependencyInjection;

namespace EmploymentAgency.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;
        
        public static void Init()
        {
            var services = new ServiceCollection();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
                _provider.GetRequiredService(item.ServiceType);
        }
    }
}
