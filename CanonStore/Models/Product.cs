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
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Bill_details = new HashSet<Bill_details>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        public int Type { get; set; }
        public Nullable<int> Mount { get; set; }
        [Required]
        public Nullable<int> Warranty { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bill_details> Bill_details { get; set; }
        public virtual Mount_Product Mount_Product { get; set; }
        public virtual Type_Product Type_Product { get; set; }
        public virtual Storage Storage { get; set; }

    }
}
