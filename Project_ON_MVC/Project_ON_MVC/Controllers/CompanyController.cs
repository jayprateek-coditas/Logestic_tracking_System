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
        CompanyContext _context;
        UserContext _user;
        TrackingContext _track;
        public CompanyController()
        {
            _context = new CompanyContext();
            _user = new UserContext();
            _track = new TrackingContext();
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
        public ActionResult Register_(Register_Company reg)
        {

            reg.user.Role_ID = 13;
            reg.user.user_status = "-1";
            if (ModelState.IsValid)
            {
                _user.add(reg.user);
                ModelState.Clear();
            }
            var id = (from ans in _user.Get() where ans.Email == reg.user.Email && ans.Password == reg.user.Password select new { ans.Users_ID }).ToList();

            if (id.Count == 1)
            {
                reg.company.User_ids = id[0].Users_ID;
                reg.company.User = null;
                if (ModelState.IsValid)
                {
                    _context.add(reg.company);
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

        public ActionResult DisplayCompany()
        {
            int id = (int)TempData["comp_store_id"];
            Company comp = _context.Get_ID(id);
            return View(comp);
        }
        public ActionResult DisplayCompany_Orders()
        {
            var Company_ids = _context.Get().Where(s => s.User_ids == (int)Session["user_store"]).ToList()[0].Company_ID;
            var Company_total_orders = _track.Get().Where(c => c.Company_ID == Company_ids).ToList();
            return View(Company_total_orders);
        }

        public ActionResult DisplayCompany_Panding_Orders()
        {
            var Company_ids = _context.Get().Where(s => s.User_ids == (int)Session["user_store"]).ToList()[0].Company_ID;
            var Company_panding_orders = _track.Get().Where(c => c.Company_ID == Company_ids && c.Order_Status.Contains("Success") == false);
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
            var data = _track.Get().Where(t => t.Tracking_Number == value).ToList();

            return View(data);
             
           
        }
    }
}