using EmploymentAgency.Model.Database.Models;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IAccordanable
    {
        bool CorrectDataUser(string login, string password);

        bool CorrectDataUser(string login, string password, out v_user user);
    }
}
