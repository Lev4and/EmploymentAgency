namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IRemovable
    {
        void CompleteRemovalPossessionSkills(int idApplicant);

        void CompleteRemovalPossessionDrivingLicenseCategories(int idApplicant);

        void CompleteRemovalEducationActivities(int idApplicant);

        void CompleteRemovalKnowledgeLanguages(int idApplicant);

        void CompleteRemovalLaborActivities(int idApplicant);

        void CompleteRemovalNecessarySkills(int idVacancy);

        void RemoveBranch(int idBranch);

        void RemoveCity(int idCity);

        void RemoveCountry(int idCountry);

        void RemoveDrivingLicenseCategory(int idDrivingLicenseCategory);

        void RemoveEducation(int idEducation);

        void RemoveEmploymentType(int idEmploymentType);

        void RemoveExperience(int idExperience);

        void RemoveGender(int idGender);

        void RemoveIndustry(int idIndustry);

        void RemoveLanguage(int idLanguage);

        void RemoveLanguageProficiency(int idLanguageProficiency);

        void RemoveOrganization(int idOrganization);

        void RemoveProfession(int idProfession);

        void RemoveProfessionCategory(int idProfessionCategory);

        void RemoveRequestStatus(int idRequestStatus);

        void RemoveRole(int idRole);

        void RemoveSchedule(int idSchedule);

        void RemoveSkill(int idSkill);

        void RemoveStreet(int idStreet);

        void RemoveSubIndustry(int idSubIndustry);

        void RemoveUser(int idUser);

        void RemoveVacancy(int idVacancy);
    }
}