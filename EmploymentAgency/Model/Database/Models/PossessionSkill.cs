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
    
    public partial class PossessionSkill
    {
        public int IdPossessionSkill { get; set; }
        public int IdApplicant { get; set; }
        public int IdSkill { get; set; }
    
        public virtual Applicant Applicant { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
