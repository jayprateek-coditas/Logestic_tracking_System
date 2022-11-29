using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Project_ON_MVC.Services;
using Project_ON_MVC.Models;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_ON_MVC.Services
{
    public class UserContext 
    {
        MY_ProjectEntities1 _context;
        public UserContext()
        {
             _context = new MY_ProjectEntities1();
        }


      public  IEnumerable<User> Get()
        {
            return  _context.Users.ToList();
        }

        public User Get_ID(int id)
        {
            try
            {
                var record = _context.Users.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                return record;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       public void add(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }  
            catch(Exception ex)
            {
                throw ex;
            }
        }

       public User Update(int id, User entity=null)
        {
            try
            {
                var record =_context.Users.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");
                if (entity != null)
                {
                    record.Email = entity.Email;
                    record.Password = record.Password;
                }
                else
                {
                    record.user_status = "1";
                }
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
                var record = _context.Users.Find(id);
                if (record == null)
                    throw new Exception($"The Record with Category Id {id} is Missing");

                _context.Users.Remove(record);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}