//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CanonStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            this.Bill_details = new HashSet<Bill_details>();
        }
    
        public int IdBill { get; set; }
        public System.DateTime Date_Created { get; set; }
        public int IdCustomer { get; set; }
        public int IdEmloyee { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual Bill_Status Bill_Status { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_details> Bill_details { get; set; }
        public virtual Emloyee Emloyee { get; set; }
    }
}
