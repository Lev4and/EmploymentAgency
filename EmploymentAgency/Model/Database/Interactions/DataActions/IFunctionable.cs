namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IFunctionable
    {
        bool IsClosedVacancy(int idVacancy);

        bool IsRelatedToStaff(int idUser);

        bool IsRequestConsidered(int idRequest);

        bool NecessaryToSupplementTheInformation(int idUser);

        void CloseVacancy(int idVacancy);
    }
}
