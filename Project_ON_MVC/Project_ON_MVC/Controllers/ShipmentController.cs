using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Models;
using Project_ON_MVC.Services;
namespace Project_ON_MVC.Controllers
{
    public class ShipmentController : Controller
    {
        // GET: Shipment
        ShipmentContext shipmentContext;
        CompanyContext companyContext;

        public ShipmentController()
        {
            this.shipmentContext = new ShipmentContext();
            this.companyContext = new CompanyContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add_Route()
        {
            //Session["Action"] = "Add_Route";
            //Session["Controller"]="Orders";
            return View();
        }
        [HttpPost]
        public ActionResult Add_Route(Shipment ship)
        {
            if (Session["user_store"] != null)
            {
                if (ModelState.IsValid)
                {
                    var comp_id = companyContext.Comp_by_ID(Convert.ToInt32(Session["user_store"]));

                    var ship_data = (from shipmentData in shipmentContext.Get().ToList() where ship.Source.ToLower().Trim() == shipmentData.Source.ToLower().Trim() && ship.Destination.ToLower().Trim() == shipmentData.Destination.ToLower().Trim() && comp_id == shipmentData.Company_ID select shipmentData).ToList();

                    if (ship_data.Count() > 0)
                    {
                        ViewBag.RouteError = "Already Having this Route !!";
                        return View();
                    }
                    else
                    {
                        ship.Company_ID = comp_id;
                        shipmentContext.add(ship);
                    }
                }
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        
        public ActionResult Display_Route()
        {
            if (Session["user_store"] != null)
            {
                var order = (List<string>)TempData["order"];
                var routes = (shipmentContext.Get()).Where(value => value.Source == order[0] && value.Destination == order[1]).ToList();
                TempData.Keep("order");
                if (routes.Count() > 0)
                {
                    return View(routes);
                }
                else
                {
                    ViewBag.route_msg = "Currently WE Don't Have Any Service in this Route  So we Can't Take your Order";
                }
                return View();
            }
            return RedirectToAction("Login", "User");
        }
    //    [HttpPost]
        public ActionResult Confirm(int? id)
        {

           int id_store = (int)id;
            TempData["ship"] = shipmentContext.Get_ID_Details(id_store);
            return RedirectToAction("ConfirmOrder", "Orders");
        }

     //  [HttpPost]
        public ActionResult Show_Company(int? id)
        {
            TempData["comp_store_id"]=(int)id;
            return RedirectToAction("DisplayCompany", "Company");
        }
    }
}