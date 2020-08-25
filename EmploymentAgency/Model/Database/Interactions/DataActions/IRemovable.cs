namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IRemovable
    {
        void CompleteRemovalEducationActivities(int idApplicant);

        void CompleteRemovalKnowledgeLanguages(int idApplicant);

        void CompleteRemovalLaborActivities(int idApplicant);

        void CompleteRemovalNecessarySkills(int idVacancy);

        void CompleteRemovalPossessionSkills(int idApplicant);

        void CompleteRemovalPossessionDrivingLicenseCategories(int idApplicant);

        void CompleteRemovalPreferredEmploymentTypes(int idRequest);

        void CompleteRemovalPreferredSchedules(int idRequest);

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

        void RemoveRequest(int idRequest);

        void RemoveRequestStatus(int idRequestStatus);

        void RemoveRole(int idRole);

        void RemoveSchedule(int idSchedule);

        void RemoveSkill(int idSkill);

        void RemoveStreet(int idStreet);

        void RemoveSubIndustry(int idSubIndustry);

        void RemoveSuitableVacancy(int idRequest);

        void RemoveUser(int idUser);

        void RemoveVacancy(int idVacancy);
    }
}