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
    public class CompanyContext
    {
        MY_ProjectEntities1 _context;
        public CompanyContext()
        {
            _context = new MY_ProjectEntities1();
        }

        
      public  IEnumerable<Company> Get()
        {
            return _context.Companies.ToList();
        }
     public   void add(Company comp)
        {
            try
            {
                _context.Companies.Add(comp);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int Comp_by_ID(int id)
        {
            try
            {
                var comp = (from comps in _context.Companies.ToList() where comps.User_ids == id select comps).ToList();
                return comp[0].Company_ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     public  Company Get_ID(int id)
        {
            try
            {
                var record = _context.Companies.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                return record;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

     public   Company Update(int id, Company entity)
        {
            try
            {
                var record = _context.Companies.Find(id);
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
                var record = _context.Companies.Find(id);    
                if(record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                _context.Companies.Remove(record);
                _context.SaveChanges();
            }
            catch(Exception ex) {
                throw ex;
            }
        }

       public void Delete_UserID(int id)
        {
            var record = _context.Companies.ToList();
             
            foreach (var it in record)
            {
                if(it.User_ids==id)
                {
                    _context.Companies.Remove(it);
                    _context.SaveChanges();
                }
            }
        }
       
       
    }
}