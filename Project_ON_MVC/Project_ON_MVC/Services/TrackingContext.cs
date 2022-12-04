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
    public class TrackingContext
    {
        MY_ProjectEntities1 _context;
        public TrackingContext()
        {
            _context = new MY_ProjectEntities1();
        }


        public IEnumerable<Tracking> Get()
        {
            return _context.Trackings.ToList();
        }
        public void add(Tracking track)
        {
            try
            {
                _context.Trackings.Add(track);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tracking Find_by_OrderID(int id)
        {
            try
            {
                var comp = (from track in _context.Trackings.ToList() where track.Order_ID == id select track).ToList();
                return comp[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Tracking Get_by_TrackID(int id)
        {
            try
            {
                var record = _context.Trackings.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void  Update_Status(int id,string str="")
        {
            try
            {
                var record = _context.Trackings.Find(id);
                  
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                record.Order_Status = str;

                //                record = entity;


                _context.SaveChanges();
                
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
                var record = _context.Trackings.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                _context.Trackings.Remove(record);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}