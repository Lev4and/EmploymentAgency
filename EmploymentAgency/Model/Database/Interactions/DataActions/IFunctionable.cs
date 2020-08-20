namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IFunctionable
    {
        bool IsClosedVacancy(int idVacancy);

        bool IsRelatedToStaff(int idUser);

        bool NecessaryToSupplementTheInformation(int idUser);

        void CloseVacancy(int idVacancy);
    }
}
