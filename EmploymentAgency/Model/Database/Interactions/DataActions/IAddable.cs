using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IAddable
    {
        bool AddManager(int idUser, string name, string surname, string patronymic, int idGender, byte[] photo, DateTime dateOfBirth, string phoneNumber);
    }
}
