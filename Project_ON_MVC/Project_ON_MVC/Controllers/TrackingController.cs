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
        public TrackingController()
        {
            trackingContext = new TrackingContext();
        }

        public ActionResult Add()
        {
            Tracking t1 = new Tracking();
            var l1 = (List<int>)TempData["track_data"];
            t1.Order_ID = l1.ElementAt(0);
            t1.Company_ID = l1.ElementAt(1);
            t1.Order_Status = "Order is in Process";
            trackingContext.add(t1);
            return RedirectToAction("ShowPlacedOrder", "Orders");
        }

        public ActionResult Tracking_Update(int id)
        {
            var t1 = trackingContext.Get_by_TrackID(id);
            TempData["track_value"] = t1.Tracking_Number;
            List<String> l1 = new List<string> { "Order in Process", "Order in Picked", "Order Shipped", "Order out for Delivary", "Order Successfully Delivary" };
            TempData["order_stages"] = l1;

            return View(t1);
        }
        [HttpPost] 
        public ActionResult Tracking_Update(Tracking tracking)
        {
            trackingContext.Update_Status((int)TempData["track_value"],tracking.Order_Status);
            return RedirectToAction("Search","Company");
        }
       
    }
}