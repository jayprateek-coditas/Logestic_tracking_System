using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Project_ON_MVC.Models;
using Project_ON_MVC.Services;

namespace Project_ON_MVC.Controllers
{
    public class TrackingController : Controller
    {
        // GET: Tracking
        TrackingContext trackingContext;
        UserContext userContext;
        CompanyContext companyContext;
        public TrackingController()
        {
            trackingContext = new TrackingContext();
            userContext = new UserContext();
            companyContext = new CompanyContext();
        }

        public ActionResult Add()
        {
            Tracking t1 = new Tracking();
            var l1 = (List<int>)TempData["track_data"];
            t1.Order_ID = l1.ElementAt(0);
            t1.Company_ID = l1.ElementAt(1);
            t1.Order_Status = "Order in Process";
            trackingContext.add(t1);
            return RedirectToAction("ShowPlacedOrder", "Orders");
        }

        public ActionResult TrackingStatusUpdate()
        {
            var id = (from user in userContext.Get() join company in companyContext.Get() on user.Users_ID equals company.User_ids where user.Users_ID == (int)Session["user_store"] select company).ToList();
            var t1 = trackingContext.Get().Where(company_id => company_id.Company_ID == id[0].Company_ID && !(company_id.Order_Status).Contains("Success"));
            if (t1.Count() == 0)
            {
                ViewBag.CompanyOrdermsg = "Sorry You Are not Having any Pending Orders ";
                return View();
            }
            
            return View(t1);

        }

        public ActionResult Tracking_Update(int id)
        {
            TempData["track_value"] =(int)id;
            List<String> l1 = new List<string> { "Order in Process", "Order is Picked", "Order Shipped", "Order out for Delivary", "Order Successfully Delivary" };
            ViewBag.order_stages = l1;
            var tracking_data = trackingContext.Get_by_TrackID((int)id);
            return View(tracking_data);
        }
        [HttpPost]
        public ActionResult Tracking_Update(Tracking track)
        {

            trackingContext.Update_Status((int)TempData["track_value"],track.Order_Status);
            return RedirectToAction("TrackingStatusUpdate");
        }


        public ActionResult TrackingStatus(int id)

        {
            var t1 = trackingContext.Get_by_TrackID((int)id);
            
           
            return View(t1);
        }

    }
}