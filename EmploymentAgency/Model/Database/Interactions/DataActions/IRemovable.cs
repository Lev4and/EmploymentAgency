﻿namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IRemovable
    {
        void RemoveBranch(int idBranch);

        void RemoveCity(int idCity);
    }
}
