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
    
    public partial class GET_ALL_ORDERS_Result
    {
        public int Order_ID { get; set; }
        public Nullable<int> Order_Valuation { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> OrderWeight { get; set; }
        public string Order_Source { get; set; }
        public string Order_Destination { get; set; }
        public string Source_Description { get; set; }
        public string Dest_Description { get; set; }
        public Nullable<double> Total_Price { get; set; }
        public Nullable<System.DateTime> Order_Time { get; set; }
        public Nullable<int> User_ids { get; set; }
        public Nullable<int> Ship_ID { get; set; }
    }
}
