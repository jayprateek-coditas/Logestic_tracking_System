using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project_ON_MVC.Models;
using Project_ON_MVC.Services;


namespace Logestic_Tracking.Controllers
{

    public class UserController : Controller
    {
        // GET: User
        UserContext _user;
    
        public UserController()
        {
           _user = new UserContext();
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
            var u = _user.Get().Where(a => a.Email == user.Email && a.Password == user.Password).ToList();

            Session["user_store"] = u[0].Users_ID;
            if (u.Count() ==0)
            {
                ViewBag.msg = "Invalid id OR Password !!";
                return View();
            }
            else if (u[0].user_status!="1")
            {
                ViewData["msg"] = "You Are not Verified till Now !!";
                return View();
            }

            Session["email"] = u[0].Email;
            if (u[0].Role_ID==11)
            {
                return RedirectToAction("Request_List","Admin");
            }
            else if (u[0].Role_ID==13)
                     {
              //  Session["user_store"] = u[0].Users_ID;
                //TempData["comp_store"] = u[0].Users_ID;
                return RedirectToAction("Search", "Company");
            }
           // Session["user_store"] = u[0].Users_ID;
             return RedirectToAction("PlaceOrder", "Orders");
            
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
                _user.add(user);
            }
            return View(user);
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

            
        
       
    }
}