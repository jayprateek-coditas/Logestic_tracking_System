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
        PaymentContext paymentContext;
        public OrdersController()
        {
            ordersContext = new OrdersContext();
            shipmentContext = new ShipmentContext();
            userContext = new UserContext();
            trackingContext = new TrackingContext();
            paymentContext = new PaymentContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult PlaceOrder()
        {
            if (Session["user_store"] != null)
            {
                var source = (from source_data in shipmentContext.Get().ToList() select source_data.Source).Distinct().ToList();
                ViewBag.source_data = source;
                var destination = (from source_data in shipmentContext.Get().ToList() select source_data.Destination).Distinct().ToList();
                ViewBag.dest_data = destination;
                return View();
            }
            else
                return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult PlaceOrder(Order_Details order)
        {
            if (ModelState.IsValid)
            {
                List<string> store = new List<string>();
                store.Add(order.Order_Source);
                store.Add(order.Order_Destination);
                TempData["order"] = store;
                TempData["order_data"] = order;
                return RedirectToAction("Display_Route", "Shipment");
            }
            ViewBag.PlaceOrderError = "Please Enter Valid Address";
            return RedirectToAction("PlaceOrder");
            // return View(order);
        }

        public ActionResult ConfirmOrder()
        {
            if (Session["user_store"] != null)
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

                    Payment payment = new Payment();
                    payment.Order_ID = order.Order_ID;
                    payment.Bill = (double)order.Total_Price;
                    payment.Pay_Status = false;
                    paymentContext.add(payment);
                    Session["order_d"] = order;
                    return RedirectToAction("Pay", new { pay_id = payment.Invoice_No });
                }
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult Pay(int pay_id)
        {
            if (Session["user_store"] != null)
            {
                Payment pay = paymentContext.Get_ID(pay_id);

                //List<string> pay_type = new List<string> { "UPI", "Net Banking ", "Credit Card ", "Paytm", "PhonePay" };
               // TempData["list"] = pay_type;
                ViewBag.Pay = pay;
                TempData["payment_invoice"] = pay.Invoice_No;
                return View(pay);
            }
            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public ActionResult Pay(Payment payment)
        {
            // var order = (Order_Details)Session["order_d"];
            if (Session["user_store"] != null)
            {
                MY_ProjectEntities1 _context = new MY_ProjectEntities1();
                //  var ord = _context.Order_Details.Find(order.Order_ID);
                var pay_update = _context.Payments.Find((int)(TempData["payment_invoice"]));
                pay_update.Pay_Status = true;
                pay_update.Payment_time = DateTime.Now;
                pay_update.Payment_Mode = "Credit Card";
                _context.SaveChanges();
                List<int> l2 = new List<int>();
                l2.Add((int)pay_update.Order_ID);     // storing order id 
                l2.Add((int)pay_update.Order_Details.Shipment.Company_ID);  // storing company id
                TempData["track_data"] = l2;
                return RedirectToAction("Add", "Tracking");
            }
             return RedirectToAction("Login", "User");
        }


        public ActionResult ShowPlacedOrder()
        {
            if (Session["user_store"] != null)
            {
                var user = (int)Session["user_store"];

                var orders = (from user_1 in userContext.Get()
                              join order_data in ordersContext.Get() on user_1.Users_ID equals order_data.User_ids
                              join track in trackingContext.Get() on order_data.Order_ID equals track.Order_ID
                              select track).ToList();
                return View(orders);
            }
            return RedirectToAction("Login", "User");
        }
        public ActionResult ShowUnPaidOrder()
        {
            if (Session["user_store"] != null)
            {
                var user = (int)Session["user_store"];
                var orders = (from users in userContext.Get()
                              join order_data in ordersContext.Get() on users.Users_ID equals order_data.User_ids
                              join pay in paymentContext.Get() on order_data.Order_ID equals pay.Order_ID
                              where pay.Pay_Status == false
                              select pay
                             ).ToList();

                return View(orders);
            }
            return RedirectToAction("Login", "User");
        }

        
    }
}


