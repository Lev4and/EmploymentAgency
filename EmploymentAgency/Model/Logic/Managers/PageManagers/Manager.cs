using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Manager : PageManager
    {
        public Manager()
        {

        }

        public override Page GetPage()
        {
            return new SupplementingInformationForManager();
        }
    }
}
