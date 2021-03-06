﻿using System;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IContainsable
    {
        bool ContainsApplicant(int idApplicant);

        bool ContainsBranch(int idOrganization, int idStreet, string nameHouse);

        bool ContainsCity(int idCountry, string cityName);

        bool ContainsCountry(string countryName);

        bool ContainsContract(int idEmploymentRequest);

        bool ContainsDrivingLicenseCategory(string drivingLicenseCategoryName);

        bool ContainsEducation(string educationName);

        bool ContainsEducationalActivity(int idApplicant, int idEducation, string nameEducationalnstitution, string address, DateTime startDate, DateTime? endDate);

        bool ContainsEmployer(int idEmployer);

        bool ContainsEmploymentRequest(int idSuitableVacancy);

        bool ContainsEmploymentType(string employmentTypeName);

        bool ContainsExperience(string experienceName);

        bool ContainsGender(string genderName);

        bool ContainsIndustry(string industryName);

        bool ContainsKnowledgeLanguage(int idApplicant, int idLanguage, int idLanguageProficiency);

        bool ContainsLaborActivity(int idApplicant, string organizationName, string organizationAddress, string professionName, string activity, DateTime startDate, DateTime? endDate);

        bool ContainsLanguage(string languageName);

        bool ContainsLanguageProficiency(string designation, string languageProficiencyName);

        bool ContainsManager(int idManager);

        bool ContainsNecessarySkill(int idVacancy, int idSkill);

        bool ContainsOrganization(string organizationName);

        bool ContainsPossessionDrivingLicenseCategory(int idApplicant, int idDrivingLicenseCategory);

        bool ContainsPossessionSkill(int idApplicant, int idSkill);

        bool ContainsProfession(string professionName);

        bool ContainsProfessionCategory(string nameProfessionCategory);

        bool ContainsPreferredEmploymentType(int idRequest, int idEmploymentType);

        bool ContainsPreferredSchedule(int idRequest, int idSchedule);

        bool ContainsRequestStatus(string requestStatusName);

        bool ContainsRole(string roleName);

        bool ContainsSchedule(string scheduleName);

        bool ContainsSkill(string skillName);

        bool ContainsStreet(int idCity, string streetName);

        bool ContainsSubIndustry(int idIndustry, string nameSubIndustry);

        bool ContainsSuitableVacancy(int idRequest, int idVacancy);

        bool ContainsUser(string login);
    }
}