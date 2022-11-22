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
        public ActionResult Login(UserController user)
        {
            return View();
        }

        public ActionResult SignUp()
        {
            var role = context.Roles.ToList();


            List<SelectListItem> my_rol = new List<SelectListItem>();//creating list of select list items
           
            foreach (var ro in role)
            {
                my_rol.Add(new SelectListItem() { Value = (ro.Role_ID).ToString(), Text = ro.Role_Name });//populating select list with items
            }
            ViewData.Add("Roles_1", my_rol.AsEnumerable());//adding list to view data
            return View();

            
           // ViewData["roles"] = my_role;
        }
        [HttpPost]
        public ActionResult SignUp(User user)
        {
            if(ModelState.IsValid)
            {
                
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View(user);

        }
    }
}