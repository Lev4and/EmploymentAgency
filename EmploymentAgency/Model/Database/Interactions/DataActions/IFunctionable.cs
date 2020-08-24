namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IFunctionable
    {
        bool BreakContract(int idContract);

        bool DoesItHaveActiveContracts(int idApplicant);

        bool IsBrokeContract(int idContract);

        bool IsClosedVacancy(int idVacancy);

        bool IsRelatedToStaff(int idUser);

        bool IsRequestConsidered(int idRequest);

        bool NecessaryToSupplementTheInformation(int idUser);

        void CloseVacancy(int idVacancy);
    }
}
