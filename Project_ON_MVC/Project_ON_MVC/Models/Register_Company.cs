using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project_ON_MVC.Models;
namespace Project_ON_MVC.Models
{
    public class Register_Company
    {
      public User user { get; set; }
        public Company company { get; set; }
        //public IEnumerable<Shipment> shipments = null;
    }
}