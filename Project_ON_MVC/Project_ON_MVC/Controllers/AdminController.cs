using Project_ON_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Models;

namespace Project_ON_MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        MY_ProjectEntities context;

         public AdminController(MY_ProjectEntities context)
        {
            this.context = context;
        }

       
        public ActionResult Index()
        {
            List<User> users = context.Users.ToList();
            List<Company>comp=context.Companies.ToList();


            var un_verify = (from un in users
                             join comp_1 in comp on un.Users_ID equals comp_1.User_ids
                             where un.user_status == "-1"
                             select new
                             {
                                 user = un,
                                 comp = comp_1,
                             }).ToList().Distinct();

            List<Register_Company> r1=new List<Register_Company>();
           
             foreach(var it in un_verify)
            {
                Register_Company r2=null;
                r2.user = it.user;
                r2.company = it.comp;
                r1.Add(r2); 
            }

            return View();
        }
    }
}