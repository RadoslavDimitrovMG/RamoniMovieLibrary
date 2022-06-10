using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext : IDB<User, int>
    {
        private MovieLibraryContext _context;
        public UserContext(MovieLibraryContext context)
        {
            _context = context;
        }
        public void Create(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Users.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Read(int key)
        {
            try
            {
                return _context.Users.Include(u => u.Movies).SingleOrDefault(u => u.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<User> ReadAll()
        {
            try
            {
                return _context.Users.Include(u => u.Movies).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(User item)
        {
            try
            {
                _context.Users.Update(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
