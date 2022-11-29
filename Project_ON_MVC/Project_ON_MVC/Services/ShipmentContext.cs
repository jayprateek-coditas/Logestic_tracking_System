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
    public class ShipmentContext
    {
        MY_ProjectEntities1 _context;
        public ShipmentContext()
        {
            _context = new MY_ProjectEntities1();
        }


        public IEnumerable<Shipment> Get()
        {
            return _context.Shipments.ToList();
        }
        public void add(Shipment ship)
        {
            try
            {
                _context.Shipments.Add(ship);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Shipment Get_ID(int id)
        {
            try
            {
                var record = _context.Shipments.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Shipment Id {id} is Missing");
                return record;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Shipment Update(int id, Shipment entity)
        {
            try
            {
                var record = _context.Shipments.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Shipment Id {id} is Missing");


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
                var record = _context.Shipments.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Shipment Id {id} is Missing");

                _context.Shipments.Remove(record);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete_CompID(int id)
        {
            var record = _context.Shipments.ToList();

            foreach (var it in record)
            {
                if (it.Company_ID == id)
                {
                    _context.Shipments.Remove(it);
                    _context.SaveChanges();
                    return;
                }
            }
        }

    }
}