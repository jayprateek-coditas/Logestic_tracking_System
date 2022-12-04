using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Models;
using Project_ON_MVC.Services;
namespace Project_ON_MVC.Controllers
{
    //[Authorize(Roles ="Company")]
    public class CompanyController : Controller
    {
        CompanyContext companyContext;
        UserContext userContext;
        TrackingContext trackContext;
        public CompanyController()
        {
            companyContext = new CompanyContext();
            userContext = new UserContext();
            trackContext = new TrackingContext();
        }
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
        public ActionResult Register_(Register_Company register)
        {
            register.user.Role_ID = 13;
            register.user.user_status = "-1";
            if (ModelState.IsValid)
            {
                userContext.add(register.user);
                ModelState.Clear();
            }
            var id = (from users in userContext.Get() where users.Email == register.user.Email && users.Password == register.user.Password select new { users.Users_ID }).ToList();
            if (id.Count == 1)
            {
                register.company.User_ids = id[0].Users_ID;
                register.company.User = null;
                if (ModelState.IsValid)
                {
                    companyContext.add(register.company);
                    ModelState.Clear();
                }
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewData["Company_Msg"] = "Email ID or Password already registered ";
            }

            return RedirectToAction("Login", "User");
        }
        public ActionResult DisplayCompany()
        {
            int id = (int)TempData["comp_store_id"];
            Company comp = companyContext.Get_ID(id);
            return View(comp);
        }
        public ActionResult DisplayCompany_Orders()
        {
            if (Session["user_store"] != null)
            {
                var Company_ids = companyContext.Get().Where(value => value.User_ids == (int)Session["user_store"]).ToList()[0].Company_ID;
                var Company_total_orders = trackContext.Get().Where(value => value.Company_ID == Company_ids).ToList();
                return View(Company_total_orders);
            }

            return RedirectToAction("Login", "User");

        }

        public ActionResult DisplayCompany_Panding_Orders()
        {
            var Company_ids = companyContext.Get().Where(s => s.User_ids == (int)Session["user_store"]).ToList()[0].Company_ID;
            var Company_panding_orders = trackContext.Get().Where(c => c.Company_ID == Company_ids && c.Order_Status.Contains("Success") == false);
            return View(Company_panding_orders);
        }


       
        public ActionResult Search()
        {
            List <Tracking>l1= new List<Tracking>();
            return View(l1);
        
        }
        
        [HttpPost]
        public ActionResult Search(string str)
        {
            string str_value = "";
             foreach(var it in str)
            {
                if (it >= '0' && it <= '9')
                    str_value += it;
            }
            var value = Convert.ToInt64(str_value);
            var data = trackContext.Get().Where(t => t.Tracking_Number == value).ToList();

            return View(data);
        }


        public ActionResult ShowAllRoutes()
        {

            if (Session["user_store"] != null)
            {
                var Company_ids = (companyContext.Get().Where(s => s.User_ids == (int)Session["user_store"]).ToList()[0].Shipments).ToList();
                return View(Company_ids);
            }

            
            
                return RedirectToAction("Login", "Users");
            
        }
    }
}