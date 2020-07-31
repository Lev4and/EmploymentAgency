using EmploymentAgency.Views.Pages;
using System;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Applicant : PageManager
    {
        public Applicant()
        {

        }

        public override Page GetPage()
        {
            return new SupplementingInformationForApplicant();
        }
    }
}
