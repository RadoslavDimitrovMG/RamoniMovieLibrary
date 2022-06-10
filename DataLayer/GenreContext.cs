using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GenreContext : IDB<Genre, int>
    {
        private MovieLibraryContext _context;
        public GenreContext(MovieLibraryContext context)
        {
            _context = context;
        }
        public void Create(Genre item)
        {
            try
            {
                _context.Genres.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Genres.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Genre Read(int key)
        {
            try
            {
                return _context.Genres.Include(m => m.Movies).SingleOrDefault(m => m.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public IEnumerable<Genre> ReadAll()
        {
            try
            {
                return _context.Genres.Include(m => m.Movies).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Genre item)
        {
            try
            {
                _context.Genres.Update(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
