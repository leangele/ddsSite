//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ddcSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            this.ServiceXDetailsOrders = new HashSet<ServiceXDetailsOrder>();
        }
    
        public decimal ID { get; set; }
        public string Name { get; set; }
        public bool IsOptional { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Notes { get; set; }
        public bool IsAvailable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceXDetailsOrder> ServiceXDetailsOrders { get; set; }
    }
}
