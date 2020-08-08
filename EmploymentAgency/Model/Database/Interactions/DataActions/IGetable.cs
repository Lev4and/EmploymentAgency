using EmploymentAgency.Model.Database.Models;
using System.Collections.Generic;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IGetable
    {
        int GetIdRole(string roleName);

        Country GetCountry(int idCountry);

        Organization GetOrganization(int idOrganization);

        v_branch GetBranch(int idBranch);

        List<City> GetCities(int idCountry);

        List<Country> GetCountries();

        List<Country> GetCountries(string countryName);

        List<DrivingLicenseCategory> GetDrivingLicenseCategories();

        List<DrivingLicenseCategory> GetDrivingLicenseCategories(string drivingLicenseCategoryName);

        List<Education> GetEducations();

        List<Education> GetEducations(string educationName);

        List<EmploymentType> GetEmploymentTypes(string employmentTypeName);

        List<Experience> GetExperiences(string experienceName);

        List<Gender> GetGenders();

        List<Gender> GetGenders(string genderName);

        List<Industry> GetIndustries();

        List<Industry> GetIndustries(string industryName);

        List<Language> GetLanguages();

        List<Language> GetLanguages(string languageName);

        List<LanguageProficiency> GetLanguageProficiencies();

        List<LanguageProficiency> GetLanguageProficiencies(string languageProficiencyName);

        List<Schedule> GetSchedules(string scheduleName);

        List<Skill> GetSkills();

        List<Skill> GetSkills(string skillName);

        List<Street> GetStreets(int idCity);

        List<SubIndustry> GetSubIndustries(int idIndustry);

        List<ProfessionCategory> GetProfessionCategories();

        List<ProfessionCategory> GetProfessionCategories(string nameProfessionCategory);

        List<RequestStatus> GetRequestStatuses(string requestStatusName);

        List<Role> GetRoles();

        List<Role> GetRoles(string roleName);

        List<v_branch> GetBranches(int idIndustry, int idSubIndustry, int idCountry, int idCity, int idStreet, int idOrganization); 

        List<v_branchSimplifiedInformation> GetBranches(int idOrganization);

        List<v_city> GetCities();

        List<v_city> GetCities(int idCountry, string cityName);

        List<v_street> GetStreets();

        List<v_street> GetStreets(int idCountry, int idCity, string streetName);

        List<v_subIndustry> GetSubIndustries(int idIndustry, string nameSubIndustry);

        List<v_profession> GetProfessions(int idProfessionCategory, string professionName);

        List<v_organization> GetOrganizations(int idIndustry, int idSubIndustry, string organizationName);

        List<v_organizationWithoutPhoto> GetOrganizationsWithoutPhoto();

        List<v_user> GetUsers(int idRole, string login);
    }
}
