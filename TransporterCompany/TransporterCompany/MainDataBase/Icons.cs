//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransporterCompany.MainDataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Icons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Icons()
        {
            this.WorkshopIcons = new HashSet<WorkshopIcons>();
        }
    
        public int Id_Icon { get; set; }
        public byte[] Icon_Source { get; set; }
        public string Name_Icon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkshopIcons> WorkshopIcons { get; set; }
    }
}
