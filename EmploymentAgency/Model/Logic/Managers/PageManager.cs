using EmploymentAgency.Model.Logic.Managers.PageManagers;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers
{
    public abstract class PageManager
    {
        public static List<Type> PageManagers = new List<Type>()
        {
            typeof(Manager),
            typeof(Employer),
            typeof(Applicant)
        };

        public abstract Page GetAddInformationPage();

        public abstract Page GetChangeInformationPage();

        public abstract Page GetMyContractsPage();

        public static PageManager GetPageManager(string roleName)
        {
            return Activator.CreateInstance(PageManagers.Find(t => Activator.CreateInstance(t).ToString() == roleName)) as PageManager;
        }
    }
}
