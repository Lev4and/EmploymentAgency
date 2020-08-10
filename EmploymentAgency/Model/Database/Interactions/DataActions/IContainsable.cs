using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IContainsable
    {
        bool ContainsApplicant(int idApplicant);

        bool ContainsBranch(int idOrganization, int idStreet, string nameHouse);

        bool ContainsCity(int idCountry, string cityName);

        bool ContainsCountry(string countryName);

        bool ContainsDrivingLicenseCategory(string drivingLicenseCategoryName);

        bool ContainsEducation(string educationName);

        bool ContainsEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate);

        bool ContainsEmployer(int idEmployer);

        bool ContainsEmploymentType(string employmentTypeName);

        bool ContainsKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency);

        bool ContainsLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate);

        bool ContainsManager(int idManager);

        bool ContainsOrganization(string organizationName);

        bool ContainsPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory);

        bool ContainsPossessionSkill(int idApplicant, int idSkill);

        bool ContainsSkill(string skillName);

        bool ContainsUser(string login);
    }
}
