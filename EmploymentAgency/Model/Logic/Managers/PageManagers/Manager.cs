using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Manager : PageManager
    {
        public override Page GetAddInformationPage()
        {
            return new SupplementingInformationForManager();
        }

        public override Page GetChangeInformationPage()
        {
            return new ChangeSupplementInformationForManager();
        }

        public override Page GetMyContractsPage()
        {
            return null;
        }

        public override string ToString()
        {
            return "Менеджер";
        }
    }
}
