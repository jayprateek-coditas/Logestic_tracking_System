using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_ON_MVC.Services;
using Project_ON_MVC.Models;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.Routing;

namespace Project_ON_MVC.Controllers
{
    public class OrdersController : Controller
    {
        OrdersContext ordersContext;
        ShipmentContext shipmentContext;
        TrackingContext trackingContext;
        UserContext userContext;
        public OrdersController()
        {
            ordersContext = new OrdersContext();
            shipmentContext = new ShipmentContext();
            userContext = new UserContext();
            trackingContext = new TrackingContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HandleError]
        public ActionResult PlaceOrder()
        {
            var source = (from source_1 in shipmentContext.Get().ToList() select source_1.Source).Distinct().ToList();
            ViewBag.source_1 = source;
            var destination = (from source_1 in shipmentContext.Get().ToList() select source_1.Destination).Distinct().ToList();
            ViewBag.dest_1 = destination;
            return View();

        }
        [HttpPost]
        public ActionResult PlaceOrder(Order_Details order)
        {
            List<string> s1 = new List<string>();
            s1.Add(order.Order_Source);
            s1.Add(order.Order_Destination);
            TempData["order"] = s1;
            TempData["order_data"] = order;
            return RedirectToAction("Display_Route", "Shipment");

            // return View(order);
        }

        public ActionResult ConfirmOrder()
        {
            if (TempData["ship"] != null)
            {
                Shipment shipment = (Shipment)TempData["ship"];
                Order_Details order = (Order_Details)TempData["order_data"];
                double price = (double)(order.Quantity * (order.OrderWeight * 0.05) * order.Order_Valuation) * (0.20) + (shipment.Base_Price * shipment.Distance) * 0.5;
                order.User_ids = Convert.ToInt32(Session["user_store"]);
                order.Order_Time = DateTime.Now;
                order.Total_Price = price;
                order.Ship_ID = shipment.Route_ID;
                ordersContext.add(order);
                Session["Bill"] = price;
                Session["order_d"] = order;
                return RedirectToAction("Pay");
            }
            return View();
        }
        public ActionResult Pay()
        {
            List<string> pay_type = new List<string> { "UPI", "Net Banking ", "Credit Card ", "Paytm", "PhonePay" };
            TempData["list"] = pay_type;
            return View();
        }
        [HttpPost]
        public ActionResult Pay(Payment payment)
        { 
            var order = (Order_Details)Session["order_d"];
            if (payment.Bill == order.Total_Price)
            {
                MY_ProjectEntities1 _context = new MY_ProjectEntities1();
                var ord = _context.Order_Details.Find(order.Order_ID);
                payment.Order_ID = order.Order_ID;
                payment.Pay_Status = true;
                payment.Payment_time = DateTime.Now;
                _context.Payments.Add(payment);
                _context.SaveChanges();
                     
                  List<int>l2=new List<int>();
                 l2.Add(order.Order_ID);
                l2.Add((int)shipmentContext.Get().Where(s => s.Route_ID == order.Ship_ID).ToList()[0].Company_ID);
                TempData["track_data"] = l2;
                return RedirectToAction("Add", "Tracking");
            }

            return View();
        }


        public ActionResult ShowPlacedOrder()
        {
            var user = (int)Session["user_store"];

            var orders = (from user_1 in userContext.Get()
                          join order_data in ordersContext.Get() on user_1.Users_ID equals order_data.User_ids
                          join track in trackingContext.Get() on order_data.Order_ID equals track.Order_ID
                          select track).ToList();
            return View(orders);
        }



    }
}


