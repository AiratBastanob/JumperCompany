//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LanguageSchool
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductSale
    {
        public int Id { get; set; }
        public string NameProduct { get; set; }
        public string NameAgent { get; set; }
        public Nullable<System.DateTime> ImplementationDate { get; set; }
        public Nullable<int> ProductionQuantity { get; set; }
        public Nullable<int> IdAgent { get; set; }
        public Nullable<int> IdProduct { get; set; }
    
        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
    }
}
