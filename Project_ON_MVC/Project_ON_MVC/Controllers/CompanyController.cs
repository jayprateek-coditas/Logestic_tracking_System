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

            var user_data = (from user in userContext.Get() where user.Email == register.user.Email select user).ToList();
            var company_data = (from company in companyContext.Get() where company.Company_Name.ToLower().Trim() == register.company.Company_Name.ToLower().Trim() select company).ToList();

            if(user_data.Count()>0)
            {
                ViewBag.CompanyRegisterError = "Email ID Already Exists !! ";
                return RedirectToAction("Register_","Company");
            }
            if(company_data.Count()>0)
            {
                ViewBag.CompanyRegisterError = "COMPANY NAME  ALREADY EXISTS !! ";
                return RedirectToAction("Register_", "Company");
            }

            if (ModelState.IsValid)
            {
                var password = Security.Encrypt(register.user.Password);
                register.user.Password = password;

                userContext.add(register.user);
                ModelState.Clear();
            }
            var get_user_id = from users in userContext.Get() where users.Email == register.user.Email && users.Password == register.user.Password select users.Users_ID;
            if (get_user_id!=null)
            {
                register.company.User_ids = get_user_id.ToList()[0];
                //register.company.User = null;

                    companyContext.add(register.company);
                    ModelState.Clear();
                
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.CompanyRegisterError = "Email ID  already registered ";
                return RedirectToAction("Register_", "Company");
            }
        }

        public ActionResult DisplayCompany()
        {
            int id = (int)TempData["comp_store_id"];
            TempData.Keep("comp_store_id");
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
            var Company_ids = companyContext.Get().Where(store => store.User_ids == (int)Session["user_store"]).ToList()[0].Company_ID;
            var Company_panding_orders = trackContext.Get().Where(value => value.Company_ID == Company_ids && value.Order_Status.Contains("Success") == false);
            return View(Company_panding_orders);
        }


        
        public ActionResult Search()
        {
            List <Tracking>list= new List<Tracking>();
            return View(list);
        
        }
        
        [HttpPost]
        public ActionResult Search(string str)
        {
            if (str.Count()>0)
            {
                string str_value = "";
                foreach (var iteration in str)
                {
                    if (iteration >= '0' && iteration <= '9')
                        str_value += iteration;
                }
                var value = Convert.ToInt64(str_value);
                var data = trackContext.Get().Where(store => store.Tracking_Number == value).ToList();

                return View(data);
            }

            return RedirectToAction("Search", "Company");
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