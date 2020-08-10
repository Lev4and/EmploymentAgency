namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IRemovable
    {
        void RemoveBranch(int idBranch);

        void RemoveCity(int idCity);

        void RemoveCountry(int idCountry);

        void RemoveEducation(int idEducation);

        void RemoveEmploymentType(int idEmploymentType);

        void RemoveDrivingLicenseCategory(int idDrivingLicenseCategory);
    }
}
