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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.DetailOrders = new HashSet<DetailOrder>();
        }
    
        public decimal ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public System.DateTime DateDelivery { get; set; }
        public string Location { get; set; }
        public decimal Score { get; set; }
        public string DateCreation { get; set; }
        public decimal IdClient { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string Coupon { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<decimal> PriceTax { get; set; }
        public Nullable<decimal> PriceTotal { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
