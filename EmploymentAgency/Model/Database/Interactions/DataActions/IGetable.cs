﻿using EmploymentAgency.Model.Database.Models;
using System.Collections.Generic;

namespace EmploymentAgency.Model.Database.Interactions.DataActions
{
    public interface IGetable
    {
        int GetIdRole(string roleName);

        Organization GetOrganization(int idOrganization);

        List<City> GetCities(int idCountry);

        List<Country> GetCountries();

        List<Country> GetCountries(string countryName);

        List<DrivingLicenseCategory> GetDrivingLicenseCategories();

        List<Education> GetEducations();

        List<Gender> GetGenders();

        List<Industry> GetIndustries();

        List<Language> GetLanguages();

        List<LanguageProficiency> GetLanguageProficiencies();

        List<Skill> GetSkills();

        List<Street> GetStreets(int idCity);

        List<SubIndustry> GetSubIndustries(int idIndustry);

        List<Role> GetRoles();

        List<v_branch> GetBranches(int idIndustry, int idSubIndustry, int idCountry, int idCity, int idStreet, int idOrganization); 

        List<v_branchSimplifiedInformation> GetBranches(int idOrganization);

        List<v_organization> GetOrganizations(int idIndustry, int idSubIndustry, string organizationName);

        List<v_organizationWithoutPhoto> GetOrganizationsWithoutPhoto();
    }
}
