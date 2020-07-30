using EmploymentAgency.Views.Pages;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Employer : PageManager
    {
        public Employer()
        {

        }

        public override Page GetPage()
        {
            return new SupplementingInformationForEmployer();
        }
    }
}
