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
                _context.Trackings.Add(track);
                _context.SaveChanges();
        }
        public Tracking Find_by_OrderID(int id)
        {
                var comp = (from track in _context.Trackings.ToList() where track.Order_ID == id select track).ToList();
                return comp[0];
        }
        public Tracking Get_by_TrackID(int id)
        {
            
                var record = _context.Trackings.Find(id);
                
                return record;
        }

        public void  Update_Status(int id,string str="")
        {
                var record = _context.Trackings.Find(id);
                record.Order_Status = str;
                _context.SaveChanges();
        }
        public void Delete_BY_ID(int id)
        {
                var record = _context.Trackings.Find(id);
                _context.Trackings.Remove(record);
                _context.SaveChanges();
        }
    }
}