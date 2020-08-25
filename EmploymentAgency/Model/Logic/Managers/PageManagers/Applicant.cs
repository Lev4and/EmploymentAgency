using EmploymentAgency.Views.Pages;
using System;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers.PageManagers
{
    public class Applicant : PageManager
    {
        public override Page GetAddInformationPage()
        {
            return new SupplementingInformationForApplicant();
        }

        public override Page GetChangeInformationPage()
        {
            return new ChangeSupplementInformationForApplicant();
        }

        public override Page GetMyContractsPage()
        {
            return new MyContractsApplicant();
        }

        public override string ToString()
        {
            return "Соискатель";
        }
    }
}
