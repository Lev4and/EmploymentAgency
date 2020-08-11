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

        void UpdateBranch(int idBranch, string phoneNumber);
    }
}