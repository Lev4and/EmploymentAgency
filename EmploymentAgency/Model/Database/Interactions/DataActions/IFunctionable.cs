namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IFunctionable
    {
        bool IsRelatedToStaff(int idUser);

        bool NecessaryToSupplementTheInformation(int idUser);
    }
}
