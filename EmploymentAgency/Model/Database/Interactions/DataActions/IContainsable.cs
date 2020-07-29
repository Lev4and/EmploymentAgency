namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IContainsable
    {
        bool ContainsApplicant(int idApplicant);

        bool ContainsEmployer(int idEmployer);

        bool ContainsManager(int idManager);

        bool ContainsOrganization(string organizationName);
    }
}
