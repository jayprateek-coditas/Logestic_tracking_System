using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_ON_MVC.Models;

namespace Logestic_Tracking.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MY_ProjectEntities context;
        public UserController()
        {
            context = new MY_ProjectEntities();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            User u = context.Users.Where(a => a.Email == user.Email && a.Password ==user.Password).SingleOrDefault();
            FormsAuthentication.SetAuthCookie(user.Email,false);

            if (u == null)
            {
                ViewData["msg"] = "Invalid id OR Password !!";
                return View();
            }
            else if(u.user_status!="1")
            {
                ViewData["msg"] = "You Are not Verified till Now !!";
                return View();
            }
            
             return RedirectToAction("About", "Home");
            
        }

        public ActionResult SignUp()
        {
            return View();

            
           // ViewData["roles"] = my_role;
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            user.Role_ID = 12;
            user.user_status = "1";
            if(ModelState.IsValid)
            {
                
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View(user);

        }

       
    }
}