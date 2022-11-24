using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Models;
namespace Project_ON_MVC.Controllers
{
    public class CompanyController : Controller
    {
        MY_ProjectEntities context = new MY_ProjectEntities();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Register_()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register_(Register_Company reg)
        {


            reg.user.user_status = "-1";
            if (ModelState.IsValid)
            {

                context.Users.Add(reg.user);
                context.SaveChanges();
                ModelState.Clear();
            }
            var id = (from ans in context.Users where ans.Email == reg.user.Email && ans.Password == reg.user.Password select new { ans.Users_ID }).ToList();

            if (id.Count == 1)
            {
                reg.company.User_ids = id[0].Users_ID; ;



                reg.company.User = null;
                if (ModelState.IsValid)
                {
                    context.Companies.Add(reg.company);
                    context.SaveChanges();
                    ModelState.Clear();
                }


                return View(reg);
            }
            else
            {
                ViewData["Company_Msg"] = "Email ID or Password already registered ";
            }

            return View();
        }

    }
}