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

        void UpdateBranch(int idBranch, string phoneNumber);
    }
}
