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
    
    public partial class Tracking
    {
        public Nullable<int> Order_ID { get; set; }
        public decimal Tracking_Number { get; set; }
        public Nullable<int> Company_ID { get; set; }
        public string Order_Status { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual Order_Details Order_Details { get; set; }
    }
}
