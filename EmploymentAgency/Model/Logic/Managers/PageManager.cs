using EmploymentAgency.Model.Logic.Managers.PageManagers;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace EmploymentAgency.Model.Logic.Managers
{
    public abstract class PageManager
    {
        public enum RoleName { Manager = 2, Employer = 4, Applicant = 5 }

        public static List<Type> PageManagers = new List<Type>()
        {
            typeof(Manager),
            typeof(Employer),
            typeof(Applicant)
        };

        public abstract Page GetAddInformationPage();

        public abstract Page GetChangeInformationPage();

        public static PageManager GetPageManager(int idRole)
        {
            var roleName = Enum.GetName(typeof(RoleName), idRole);

            return Activator.CreateInstance(PageManagers.Find(t => t.Name == Enum.GetName(typeof(RoleName), idRole))) as PageManager;
        }
    }
}
