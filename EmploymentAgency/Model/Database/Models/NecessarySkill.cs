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
    
    public partial class NecessarySkill
    {
        public int IdNecessarySkill { get; set; }
        public int IdVacancy { get; set; }
        public int IdSkill { get; set; }
    
        public virtual Skill Skill { get; set; }
        public virtual Vacancy Vacancy { get; set; }
    }
}
