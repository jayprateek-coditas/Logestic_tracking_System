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
            Tracking tracking = new Tracking();
            var l1 = (List<int>)TempData["track_data"];
            TempData.Keep("track_data");
            tracking.Order_ID = l1.ElementAt(0);
            tracking.Company_ID = l1.ElementAt(1);
            tracking.Order_Status = "Order in Process";
            trackingContext.add(tracking);
            return RedirectToAction("ShowPlacedOrder", "Orders");
        }

        public ActionResult TrackingStatusUpdate()
        {
            if (Session["user_store"]!=null)
            {
                var id = (from user in userContext.Get() join company in companyContext.Get() on user.Users_ID equals company.User_ids where user.Users_ID == (int)Session["user_store"] select company).ToList();
                var tracking = trackingContext.Get().Where(company_id => company_id.Company_ID == id[0].Company_ID && (company_id.Order_Status).Contains("Success") == false).ToList();
                if (tracking.Count() == 0)
                {
                    ViewBag.CompanyOrdermsg = "Great !! NO Pending Orders ";
                    return View();
                }

                return View(tracking);
            }
            return RedirectToAction("Login","User");
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
            TempData.Keep("track_value");
            return RedirectToAction("TrackingStatusUpdate");
        }


        public ActionResult TrackingStatus(int id)

        {
            var track = trackingContext.Get_by_TrackID((int)id);
            return View(track);
        }
    }
}