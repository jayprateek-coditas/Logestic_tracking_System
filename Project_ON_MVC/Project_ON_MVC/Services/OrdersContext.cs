using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Project_ON_MVC.Services;
using Project_ON_MVC.Models;
using System.Reflection;

namespace Project_ON_MVC.Services
{
    public class OrdersContext
    {
        MY_ProjectEntities1 _context;
        public OrdersContext()
        {
            _context = new MY_ProjectEntities1();
        }


        public IEnumerable<Order_Details> Get()
        {
            return _context.Order_Details.ToList();
        }
        public void add(Order_Details comp)
        {
            try
            {
                _context.Order_Details.Add(comp);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int? Order_by_ID(int id)
        {
            try
            {
                var ord = (from orders in _context.Order_Details.ToList() where orders.User_ids == id select orders).ToList();
                return ord[0].User_ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Order_Details Get_ID(int id)
        {
            try
            {
                var record = _context.Order_Details.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                return record;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Order_Details Update(int id, Order_Details entity)
        {
            try
            {
                var record = _context.Order_Details.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");


                record = entity;


                _context.SaveChanges();
                return record;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Delete(int id)
        {
            try
            {
                var record = _context.Order_Details.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                _context.Order_Details.Remove(record);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_UserID(int id)
        {
            var record = _context.Order_Details.ToList();

            foreach (var it in record)
            {
                if (it.User_ids == id)
                {
                    _context.Order_Details.Remove(it);
                    _context.SaveChanges();
                }
            }
        }

    }
}