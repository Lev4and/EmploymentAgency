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
    
    public partial class PreferredSchedule
    {
        public int IdPreferredSchedule { get; set; }
        public int IdRequest { get; set; }
        public int IdSchedule { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}