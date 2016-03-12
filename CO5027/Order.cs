//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CO5027
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderedProducts = new HashSet<OrderedProduct>();
        }
    
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public decimal TotalCost { get; set; }
        public decimal AmountPaid { get; set; }
        public System.DateTime DateStamp { get; set; }
        public string PayerId { get; set; }
        public string PaymentId { get; set; }
        public string PaymentToken { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
    }
}
