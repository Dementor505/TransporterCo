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
    
    public partial class ProductPart
    {
        public int Id_Part { get; set; }
        public int Id_Product { get; set; }
        public Nullable<int> Count { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Product Product1 { get; set; }
    }
}