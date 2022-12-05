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
        UserContext userContext;
    
        public UserController()
        {
           userContext = new UserContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(User user)
        {
            var users = userContext.Get().Where(value => value.Email == user.Email && value.Password == user.Password).ToList();
            FormsAuthentication.SetAuthCookie(user.Email, false);
            
            if (users.Count() ==0)
            {
                ViewBag.msg = "Invalid id OR Password !!";
                return View();
            }
            else if (users[0].user_status!="1")
            {
                ViewData["msg"] = "You Are not Verified till Now !!";
                return View();
            }
            Session["user_store"] = users[0].Users_ID;
            Session["Role"] = users[0].Role_ID;
            Session["email"] = users[0].Email;
            if (users[0].Role_ID==11)
            {
                return RedirectToAction("Request_List","Admin");
            }
            else if (users[0].Role_ID==13)
                     {
              //  Session["user_store"] = u[0].Users_ID;
                //TempData["comp_store"] = u[0].Users_ID;
                return RedirectToAction("Search", "Company");
            }
           // Session["user_store"] = u[0].Users_ID;
             return RedirectToAction("PlaceOrder", "Orders");
        }
        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return View();

            
           // ViewData["roles"] = my_role;
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            user.Role_ID = 12;
            user.user_status = "1";
            var usersData = (from users in userContext.Get() where users.Email == user.Email select users).ToList();
            if(usersData.Count()>0)
            {
                ViewBag.RegisterError = "EMAIL ALREADY EXIST ";
                return View();
            }
            if(ModelState.IsValid)
            {
                userContext.add(user);
                return RedirectToAction("Login", "User"); ;
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }

            
    }
}