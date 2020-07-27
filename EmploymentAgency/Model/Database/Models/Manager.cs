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
    
    public partial class Manager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manager()
        {
            this.SuitableVacancy = new HashSet<SuitableVacancy>();
        }
    
        public int IdManager { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int IdGender { get; set; }
        public byte[] Photo { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuitableVacancy> SuitableVacancy { get; set; }
    }
}
