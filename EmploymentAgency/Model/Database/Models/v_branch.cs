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
    
    public partial class v_branch
    {
        public int IdBranch { get; set; }
        public int IdOrganization { get; set; }
        public string OrganizationName { get; set; }
        public byte[] Photo { get; set; }
        public int IdIndustry { get; set; }
        public string IndustryName { get; set; }
        public int IdSubIndustry { get; set; }
        public string NameSubIndustry { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int IdCountry { get; set; }
        public int IdCity { get; set; }
        public int IdStreet { get; set; }
    }
}