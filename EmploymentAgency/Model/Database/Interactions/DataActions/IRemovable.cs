namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IRemovable
    {
        void RemoveBranch(int idBranch);

        void RemoveCity(int idCity);

        void RemoveCountry(int idCountry);

        void RemoveDrivingLicenseCategory(int idDrivingLicenseCategory);

        void RemoveEducation(int idEducation);

        void RemoveEmploymentType(int idEmploymentType);

        void RemoveExperience(int idExperience);

        void RemoveGender(int idGender);

        void RemoveIndustry(int idIndustry);

        void RemoveLanguage(int idLanguage);

        void RemoveLanguageProficiency(int idLanguageProficiency);
    }
}
