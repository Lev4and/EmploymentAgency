//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmploymentAgency.Model.Database.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class v_employmentRequest
    {
        public int IdEmploymentRequest { get; set; }
        public int IdSuitableVacancy { get; set; }
        public int IdRequest { get; set; }
        public int IdVacancy { get; set; }
        public int IdApplicant { get; set; }
        public byte[] ApplicantPhoto { get; set; }
        public string ApplicantFullName { get; set; }
        public int ApplicantIdGender { get; set; }
        public string ApplicantGenderName { get; set; }
        public Nullable<int> ApplicantAge { get; set; }
        public int ApplicantIdCountry { get; set; }
        public string ApplicantCountryName { get; set; }
        public int ApplicantIdCity { get; set; }
        public string ApplicantCityName { get; set; }
        public int ApplicantIdStreet { get; set; }
        public string ApplicantStreetName { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantPhoneNumber { get; set; }
        public bool ApplicantIsEmployed { get; set; }
        public int RequestIdProfession { get; set; }
        public string RequestProfessionName { get; set; }
        public int RequestIdExperience { get; set; }
        public string RequestExperienceName { get; set; }
        public Nullable<int> RequestSalary { get; set; }
        public int VacancyIdOrganization { get; set; }
        public byte[] VacancyPhoto { get; set; }
        public string VacancyOrganizationName { get; set; }
        public int IdBranch { get; set; }
        public int BranchIdCountry { get; set; }
        public string BranchCountryName { get; set; }
        public int BranchIdCity { get; set; }
        public string BranchCityName { get; set; }
        public int BranchIdStreet { get; set; }
        public string BranchStreetName { get; set; }
        public string BranchAddress { get; set; }
        public string PhoneNumber { get; set; }
        public int VacancyIdProfession { get; set; }
        public string VacancyProfessionName { get; set; }
        public int VacancyIdEmploymentType { get; set; }
        public string VacancyEmploymentTypeName { get; set; }
        public int VacancyIdSchedule { get; set; }
        public string VacancyScheduleName { get; set; }
        public int VacancyIdExperience { get; set; }
        public string VacancyExperienceName { get; set; }
        public Nullable<int> VacancySalary { get; set; }
        public Nullable<bool> IsSuitable { get; set; }
    }
}
