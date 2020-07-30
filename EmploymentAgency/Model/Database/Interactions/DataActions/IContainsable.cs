namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IContainsable
    {
        bool ContainsApplicant(int idApplicant);

        bool ContainsBranch(int idOrganization, int idStreet, string nameHouse);

        bool ContainsEmployer(int idEmployer);

        bool ContainsManager(int idManager);

        bool ContainsOrganization(string organizationName);
    }
}
