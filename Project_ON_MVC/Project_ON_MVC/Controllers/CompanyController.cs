using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Models;
namespace Project_ON_MVC.Controllers
{
    public class CompanyController : Controller
    {
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

            return View(reg);
        }
        

        
    }
}