using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Manager : PageManager
    {
        public Manager()
        {

        }

        public override Page GetAddInformationPage()
        {
            return new SupplementingInformationForManager();
        }

        public override Page GetChangeInformationPage()
        {
            return new ChangeSupplementInformationForManager();
        }
    }
}
