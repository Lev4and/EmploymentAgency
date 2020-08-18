using EmploymentAgency.Model.Database.Models;
using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IUpdatable
    {
        bool UpdateCity(int idCountry, int idCity, string cityName);

        bool UpdateCountry(int idCountry, string countryName, byte[] flag);

        bool UpdateDrivingLicenseCategory(int idDrivingLicenseCategory, string drivingLicenseCategoryName);

        bool UpdateEducation(int idEducation, string educationName);

        bool UpdateEmploymentType(int idEmploymentType, string employmentTypeName);

        bool UpdateExperience(int idExperience, string experienceName);

        bool UpdateGender(int idGender, string genderName);

        bool UpdateIndustry(int idIndustry, string industryName);

        bool UpdateLanguage(int idLanguage, string languageName);

        bool UpdateLanguageProficiency(int idLanguageProficiency, string designation, string languageProficiencyName);

        bool UpdateOrganization(int idOrganization, string organizationName, byte[] photo, DateTime? closingDate);

        bool UpdateProfession(int idProfession, string professionName);

        bool UpdateProfessionCategory(int idProfessionCategory, string nameProfessionCategory);

        bool UpdateRequestStatus(int idRequestStatus, string requestStatusName);

        bool UpdateRole(int idRole, string roleName);

        bool UpdateSchedule(int idSchedule, string scheduleName);

        bool UpdateSkill(int idSkill, string skillName, byte[] photo);

        bool UpdateStreet(int idStreet, string streetName);

        bool UpdateSubIndustry(int idSubIndustry, string nameSubIndustry);

        bool UpdateUser(int idUser, string login, string password);

        bool UpdateUser(int idUser, string login, string password, out User user);

        bool UpdateUser(int idUser, string login, string password, out v_user user);

        void UpdateApplicant(int idApplicant, string name, string surname, string patronymic, byte[] photo, string phoneNumber, int idStreet, string nameHouse, string apartment);

        void UpdateEmployer(int idEmployer, int idBranch, string name, string surname, string patronymic, byte[] photo, string phoneNumber);

        void UpdateBranch(int idBranch, string phoneNumber);
    }
}