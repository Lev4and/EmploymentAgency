using EmploymentAgency.Model.Database.Models;
using System.Collections.Generic;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IGetable
    {
        int GetIdRole(string roleName);

        City GetCity(int idCity);

        Country GetCountry(int idCountry);

        DrivingLicenseCategory GetDrivingLicenseCategory(int idDrivingLicenseCategory);

        Education GetEducation(int idEducation);

        EmploymentType GetEmploymentType(int idEmploymentType);

        Experience GetExperience(int idExperience);

        Gender GetGender(int idGender);

        Industry GetIndustry(int idIndustry);

        Language GetLanguage(int idLanguage);

        LanguageProficiency GetLanguageProficiency(int idLanguageProficiency);

        Organization GetOrganization(int idOrganization);

        Profession GetProfession(int idProfession);

        ProfessionCategory GetProfessionCategory(int idProfessionCategory);

        RequestStatus GetRequestStatus(int idRequestStatus);

        Role GetRole(int idRole);

        Schedule GetSchedule(int idSchedule);

        Skill GetSkill(int idSkill);

        Street GetStreet(int idStreet);

        SubIndustry GetSubIndustry(int idSubIndustry);

        User GetUser(int idUser);

        v_branch GetBranch(int idBranch);

        v_organization GetOrganizationExtendedInformation(int idOrganization);

        v_profession GetProfessionExtendedInformation(int idProfession);

        v_street GetStreetExtendedInformation(int idStreet);

        v_subIndustry GetSubIndustryExtendedInformation(int idSubIndustry);

        v_user GetUserExtendedInformation(int idUser);

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

        List<v_branch> GetBranches(int idIndustry, int idSubIndustry, int idCountry, int idCity, int idStreet, int idOrganization, string organizationName);

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