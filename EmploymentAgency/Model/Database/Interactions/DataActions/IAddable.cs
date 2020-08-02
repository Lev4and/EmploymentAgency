using EmploymentAgency.Model.Database.Models;
using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IAddable
    {
        bool AddApplicant(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddBranch(int idOrganization, int idStreet, string nameHouse, string phoneNumber);

        bool AddEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate);

        bool AddEmployer(int idUser, int idBranch, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency);

        bool AddLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate);

        bool AddManager(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo, out Organization organization);

        bool AddPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory);

        bool AddPossessionSkill(int idApplicant, int idSkill);

        bool AddSkill(string skillName, byte[] photo);

        bool AddUser(int idRole, string login, string password);
    }
}
