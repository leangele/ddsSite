//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ddcApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.DetailOrders = new HashSet<DetailOrder>();
        }
    
        public decimal ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> InLabWorkingDays { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Notes { get; set; }
        public bool IsAvailable { get; set; }
        public Nullable<bool> IsDigital { get; set; }
        public Nullable<decimal> PriceEach { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
