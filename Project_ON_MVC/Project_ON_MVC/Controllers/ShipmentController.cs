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
        ShipmentContext ship_context;
        CompanyContext companyContext;

        public ShipmentController()
        {
            this.ship_context = new ShipmentContext();
            this.companyContext = new CompanyContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add_Route()
        {
            Session["Action"] = "Add_Route";
            Session["Controller"]="Orders";
            return View();
        }
        [HttpPost]
        public ActionResult Add_Route(Shipment ship)
        {

            var comp_id = companyContext.Comp_by_ID(Convert.ToInt32(Session["user_store"]));
            
            var ship_data = (from ship_d in ship_context.Get().ToList() where ship.Source.ToLower() == ship_d.Source.ToLower() && ship.Destination.ToLower() == ship_d.Destination.ToLower() && comp_id == ship_d.Company_ID select ship_d).ToList();
           
            if(ship_data.Count()>0)
            {
                TempData["error"] = "Already Having this Route !!";
                return View();
            }

            else
            {
                ship.Company_ID=comp_id;
                ship_context.add(ship);
            }
                                 
            return View(ship);
        }
        
        public ActionResult Display_Route()
        {
            if (Session["user_store"] != null)
            {
                var order = (List<string>)TempData["order"];
                var routes = (ship_context.Get()).Where(s => s.Source == order[0] && s.Destination == order[1]).ToList();

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

           int id1 = (int)id;
            TempData["ship"] = ship_context.Get_ID(id1);
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