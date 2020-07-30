using EmploymentAgency.Model.Database.Models;
using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IAddable
    {
        bool AddBranch(int idOrganization, int idStreet, string nameHouse, string phoneNumber);

        bool AddManager(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo, out Organization organization);
    }
}
