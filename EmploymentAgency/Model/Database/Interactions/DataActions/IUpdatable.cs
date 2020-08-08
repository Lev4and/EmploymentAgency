namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IUpdatable
    {
        void UpdateBranch(int idBranch, string phoneNumber);

        void UpdateCountry(int idCountry, byte[] flag);
    }
}
