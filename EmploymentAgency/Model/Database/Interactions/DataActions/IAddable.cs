using EmploymentAgency.Model.Database.Models;
using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IAddable
    {
        bool AddApplicant(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber, int idStreet, string nameHouse, string apartment, List<object> possessionSkills, List<object> possessionDrivingLicenseCategories, List<EducationalActivity> educationalActivities, List<KnowledgeLanguage> knowledgeLanguages, List<LaborActivity> laborActivities);

        bool AddBranch(int idOrganization, int idStreet, string nameHouse, string phoneNumber);

        bool AddCity(int idCountry, string cityName);

        bool AddCountry(string countryName, byte[] flag);

        bool AddDrivingLicenseCategory(string drivingLicenseCategoryName);

        bool AddEducation(string educationName);

        bool AddEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate);

        bool AddEmployer(int idUser, int idBranch, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddEmploymentType(string employmentTypeName);

        bool AddExperience(string experienceName);

        bool AddGender(string genderName);

        bool AddIndustry(string industryName);

        bool AddKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency);

        bool AddLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate);

        bool AddLanguage(string languageName);

        bool AddLanguageProficiency(string designation, string languageProficiencyName);

        bool AddManager(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);

        bool AddNecessarySkill(int idVacancy, int idSkill);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo);

        bool AddOrganization(int idSubIndustry, string organizationName, byte[] photo, out Organization organization);

        bool AddPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory);

        bool AddPossessionSkill(int idApplicant, int idSkill);

        bool AddProfession(int idProfessionCategory, string professionName);

        bool AddProfessionCategory(string nameProfessionCategory);

        bool AddRequestStatus(string requestStatusName);

        bool AddRole(string roleName);

        bool AddSchedule(string scheduleName);

        bool AddSkill(string skillName, byte[] photo);

        bool AddStreet(int idCity, string streetName);

        bool AddSubIndustry(int idIndustry, string nameSubIndustry);

        bool AddUser(int idRole, string login, string password);

        bool AddUser(int idRole, string login, string password, out User user);

        bool AddUser(int idRole, string login, string password, out v_user user);

        void AddEducationalActivities(int idApplicant, List<EducationalActivity> educationalActivities);

        void AddKnowledgeLanguages(int idApplicant, List<KnowledgeLanguage> knowledgeLanguages);

        void AddLaborActivities(int idApplicant, List<LaborActivity> laborActivities);

        void AddNecessarySkills(int idVacancy, List<object> necessarySkills);

        void AddPossessionDrivingLicenseCategories(int idApplicant, List<object> possessionDrivingLicenseCategories);

        void AddPossessionSkills(int idApplicant, List<object> possessionSkills);

        void AddVacancy(int idEmployer, int idProfession, int idEmploymentType, int idSchedule, int idExperience, string description, string duties, string requirements, string terms, int? salary, List<object> necessarySkills);
    }
}