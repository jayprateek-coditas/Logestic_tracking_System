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
        UserContext userContext;
        CompanyContext compContext;
        OrdersContext ordersContext;
        MY_ProjectEntities1 StoreProcedure_Context;
        public AdminController()
        {
            userContext = new UserContext();
            compContext = new CompanyContext();
            ordersContext = new OrdersContext();
            StoreProcedure_Context = new MY_ProjectEntities1();
        }
        public ActionResult Request_List()
        {
            List<User> users = userContext.Get().ToList();
            List<Company> companies = compContext.Get().ToList();


            var Un_verify_Company = (from user in users
                             join company in companies on user.Users_ID equals company.User_ids
                             where user.user_status == "-1"
                             select new
                             {
                                 user = user,
                                 company = company,
                             }).ToList().Distinct();

            List<Register_Company> register_Companies = new List<Register_Company>();

            foreach (var iterator in Un_verify_Company)
            {
                Register_Company register_Company = new Register_Company();
                register_Company.user = iterator.user;
                register_Company.company = iterator.company;
                register_Companies.Add(register_Company);
            }

            return View(register_Companies);
        }

        public ActionResult Approve(int id)
        {
            User user = userContext.Update(id);
           return  RedirectToAction("Request_List");
        }

        public ActionResult Delete(int id)
        {
            
            compContext.Delete_UserID(id);
            userContext.Delete(id);
            return RedirectToAction("Request_List");
        }
        
        public ActionResult ShowOrders()
        {
           var data= ordersContext.Get();   
            return View(data);
        }

        public ActionResult Companies()
        {
            var data = (from company in StoreProcedure_Context.GET_ALL_COMPANY() join user in userContext.Get() on company.User_ids equals 
                       user.Users_ID where user.user_status=="1" select company).ToList();
            return View(data);
        }

        public ActionResult Users()
        {
            var data = userContext.Get();
            return View(data);
        }
        public ActionResult Disable(int userid)
        {

            return View();
        }

    }
}