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
    
    public partial class EmploymentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmploymentType()
        {
            this.PreferredEmploymentType = new HashSet<PreferredEmploymentType>();
            this.Vacancy = new HashSet<Vacancy>();
        }
    
        public int IdEmploymentType { get; set; }
        public string EmploymentTypeName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreferredEmploymentType> PreferredEmploymentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vacancy> Vacancy { get; set; }
    }
}
