using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Employer : PageManager
    {
        public override Page GetAddInformationPage()
        {
            return new SupplementingInformationForEmployer();
        }

        public override Page GetChangeInformationPage()
        {
            return new ChangeSupplementInformationForEmployer();
        }

        public override Page GetMyContractsPage()
        {
            return new MyContractsEmployer();
        }

        public override string ToString()
        {
            return "Работодатель";
        }
    }
}
