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
    
    public partial class WorkshopIcons
    {
        public int Id_WI { get; set; }
        public Nullable<int> Id_Workshop { get; set; }
        public Nullable<int> Id_Icon { get; set; }
        public Nullable<double> X_Position { get; set; }
        public Nullable<double> Y_Position { get; set; }
        public Nullable<double> Width_Icon { get; set; }
        public Nullable<double> Heigth_Icon { get; set; }
    
        public virtual Icons Icons { get; set; }
        public virtual Workshop Workshop { get; set; }
    }
}
