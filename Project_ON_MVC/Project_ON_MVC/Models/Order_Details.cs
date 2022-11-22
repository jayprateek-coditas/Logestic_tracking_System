//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_ON_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Details
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order_Details()
        {
            this.Payments = new HashSet<Payment>();
            this.Trackings = new HashSet<Tracking>();
        }
    
        public int Order_ID { get; set; }
        public decimal Order_Valuation { get; set; }
        public int Quantity { get; set; }
        public Nullable<double> Weight { get; set; }
        public string Order_Source { get; set; }
        public string Order_Destination { get; set; }
        public string Source_Description { get; set; }
        public string Dest_Description { get; set; }
        public decimal Total_Price { get; set; }
        public System.DateTime Order_Time { get; set; }
        public Nullable<int> User_ids { get; set; }
    
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}