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
    public class PaymentContext
    {
        MY_ProjectEntities1 _context;
        public PaymentContext()
        {
            _context = new MY_ProjectEntities1();
        }


        public IEnumerable<Payment> Get()
        {
            return _context.Payments.ToList();
        }
        public void add(Payment pay)
        {
            
            
                _context.Payments.Add(pay);
                _context.SaveChanges();
            
        }
        public int Order_by_ID(int id)
        {
            
            
                var comp = (from comps in _context.Companies.ToList() where comps.User_ids == id select comps).ToList();
                return comp[0].Company_ID;
            
        }
        public Payment Get_ID(int id)
        {
            
            
                var record = _context.Payments.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                return record;

        }

        public Payment Update(int id,Order_Details entity)
        {
          
          
                var record = _context.Payments.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");

//                record = entity;


                _context.SaveChanges();
                return record;
        }
        public void Delete(int id)
        {
            
            
                var record = _context.Companies.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                _context.Companies.Remove(record);
                _context.SaveChanges();
            
        }

        public void Delete_UserID(int id)
        {
            var record = _context.Companies.ToList();

            foreach (var it in record)
            {
                if (it.User_ids == id)
                {
                    _context.Companies.Remove(it);
                    _context.SaveChanges();
                }
            }
        }

    }
}