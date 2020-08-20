using EmploymentAgency.Model.Database.Models;
using EmploymentAgency.Views.Pages;
using System;
using System.Collections.Generic;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IGetable
    {
        int? GetMaxNumberOfAcceptedApplicantsMyVacancy(int idEmployer);

        int? GetMaxNumberOfApprovedApplicantsMyVacancy(int idEmployer);

        int? GetMaxNumberOfInterestedApplicantsMyVacancy(int idEmployer);

        int? GetMaxNumberOfPotentialApplicantsMyVacancy(int idEmployer);

        int? GetMaxSalaryMyVacancy(int idEmployer);

        int? GetMinNumberOfAcceptedApplicantsMyVacancy(int idEmployer);

        int? GetMinNumberOfApprovedApplicantsMyVacancy(int idEmployer);

        int? GetMinNumberOfInterestedApplicantsMyVacancy(int idEmployer);

        int? GetMinNumberOfPotentialApplicantsMyVacancy(int idEmployer);

        int? GetMinSalaryMyVacancy(int idEmployer);

        int GetIdRole(string roleName);

        DateTime? GetMaxClosingDateMyVacancy(int idEmployer);

        DateTime? GetMinClosingDateMyVacancy(int idEmployer);

        DateTime GetMaxDateOfRegistrationMyVacancy(int idEmployer);

        DateTime GetMinDateOfRegistrationMyVacancy(int idEmployer);

        Applicant GetApplicant(int idApplicant);

        City GetCity(int idCity);

        Country GetCountry(int idCountry);

        DrivingLicenseCategory GetDrivingLicenseCategory(int idDrivingLicenseCategory);

        Education GetEducation(int idEducation);

        Employer GetEmployer(int idEmployer);

        EmploymentType GetEmploymentType(int idEmploymentType);

        Experience GetExperience(int idExperience);

        Gender GetGender(int idGender);

        Industry GetIndustry(int idIndustry);

        Language GetLanguage(int idLanguage);

        LanguageProficiency GetLanguageProficiency(int idLanguageProficiency);

        Manager GetManager(int idManager);

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

        Vacancy GetVacancy(int idVacancy);

        v_applicant GetApplicantExtendedInformation(int idApplicant);

        v_branch GetBranch(int idBranch);

        v_employer GetEmployerExtendedInformation(int idEmployer);

        v_manager GetManagerExtendedInformation(int idManager);

        v_organization GetOrganizationExtendedInformation(int idOrganization);

        v_profession GetProfessionExtendedInformation(int idProfession);

        v_street GetStreetExtendedInformation(int idStreet);

        v_subIndustry GetSubIndustryExtendedInformation(int idSubIndustry);

        v_user GetUserExtendedInformation(int idUser);

        v_vacancy GetVacancyExtendedInformation(int idVacancy);

        List<City> GetCities(int idCountry);

        List<Country> GetCountries();

        List<Country> GetCountries(string countryName);

        List<DrivingLicenseCategory> GetDrivingLicenseCategories();

        List<DrivingLicenseCategory> GetDrivingLicenseCategories(string drivingLicenseCategoryName);

        List<Education> GetEducations();

        List<Education> GetEducations(string educationName);

        List<EmploymentType> GetEmploymentTypes();

        List<EmploymentType> GetEmploymentTypes(string employmentTypeName);

        List<Experience> GetExperiences();

        List<Experience> GetExperiences(string experienceName);

        List<Gender> GetGenders();

        List<Gender> GetGenders(string genderName);

        List<Industry> GetIndustries();

        List<Industry> GetIndustries(string industryName);

        List<Language> GetLanguages();

        List<Language> GetLanguages(string languageName);

        List<LanguageProficiency> GetLanguageProficiencies();

        List<LanguageProficiency> GetLanguageProficiencies(string languageProficiencyName);

        List<NecessarySkill> GetNecessarySkills(int idVacancy);

        List<Schedule> GetSchedules();

        List<Schedule> GetSchedules(string scheduleName);

        List<Skill> GetSkills(int idApplicant);

        List<DrivingLicenseCategory> GetDrivingLicenseCategories(int idApplicant);

        List<EducationalActivity> GetEducationActivities(int idApplicant);

        List<KnowledgeLanguage> GetKnowledgeLanguages(int idApplicant);

        List<LaborActivity> GetLaborActivities(int idApplicant);

        List<Skill> GetSkills();

        List<Skill> GetSkills(string skillName);

        List<Street> GetStreets(int idCity);

        List<SubIndustry> GetSubIndustries(int idIndustry);

        List<PossessionDrivingLicenseCategory> GetPossessionDrivingLicenseCategories(int idApplicant);

        List<PossessionSkill> GetPossessionSkills(int idApplicant);

        List<Profession> GetProfessions(int idProfessionCategory);

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

        List<v_vacancy> GetMyVacancies(int idEmployer, int idProfessionCategory, int idProfession, string professionName, DateTime? beginValueClosingDate, DateTime? endValueClosingDate, DateTime beginValueDateOfRegistration, DateTime endValueDateOfRegistration, int? beginValueNumberOfAcceptedApplicants, int? endValueNumberOfAcceptedApplicants, int? beginValueNumberOfApprovedApplicants, int? endValueNumberOfApprovedApplicants, int? beginValueNumberOfInterestedApplicants, int? endValueNumberOfInterestedApplicants, int? beginValueNumberOfPotentialApplicants, int? endValueNumberOfPotentialApplicants, int? beginValueSalary, int? endValueSalary);
    }
}