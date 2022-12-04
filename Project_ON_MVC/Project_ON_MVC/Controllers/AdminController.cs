using Project_ON_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Services;

namespace Project_ON_MVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        UserContext _user;
        CompanyContext _comp;
        OrdersContext _orders;

         public AdminController()
        {
            _user = new UserContext();
            _comp = new CompanyContext();
            _orders = new OrdersContext();
        }



        public ActionResult Request_List()
        {
            List<User> users = _user.Get().ToList();
            List<Company> comp = _comp.Get().ToList();


            var un_verify = (from un in users
                             join comp_1 in comp on un.Users_ID equals comp_1.User_ids
                             where un.user_status == "-1"
                             select new
                             {
                                 user = un,
                                 comp = comp_1,
                             }).ToList().Distinct();

            List<Register_Company> r1 = new List<Register_Company>();

            foreach (var it in un_verify)
            {
                Register_Company r2 = new Register_Company();
                r2.user = it.user;
                r2.company = it.comp;
                r1.Add(r2);
            }

            return View(r1);
        }

        public ActionResult Approve(int id)
        {
            User u1 = _user.Update(id);
           return  RedirectToAction("Request_List");
        }

        public ActionResult Delete(int id)
        {
            
            _comp.Delete_UserID(id);
            _user.Delete(id);
            return RedirectToAction("Request_List");
        }
        
        public ActionResult ShowOrders()
        {
           var data= _orders.Get();   
            return View(data);
        }

        public ActionResult Companies()
        {
            var data = _comp.Get();
            return View(data);
        }

        public ActionResult Users()
        {
            var data = _user.Get();
            return View(data);
        }
        public ActionResult Disable(int userid)
        {

            return View();
        }

    }
}