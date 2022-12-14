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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MY_ProjectEntities1 : DbContext
    {
        public MY_ProjectEntities1()
            : base("name=MY_ProjectEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tracking> Trackings { get; set; }
    
        public virtual ObjectResult<GET_ALL_COMPANY_Result> GET_ALL_COMPANY()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GET_ALL_COMPANY_Result>("GET_ALL_COMPANY");
        }
    
        public virtual ObjectResult<GET_ALL_ORDERS_Result> GET_ALL_ORDERS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GET_ALL_ORDERS_Result>("GET_ALL_ORDERS");
        }
    
        public virtual ObjectResult<GET_ALL_USERS_Result> GET_ALL_USERS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GET_ALL_USERS_Result>("GET_ALL_USERS");
        }
    }
}
