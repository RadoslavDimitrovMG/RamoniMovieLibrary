using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MovieContext : IDB<Movie, int>
    {
        private MovieLibraryContext _context;
        public MovieContext(MovieLibraryContext context)
        {
            _context = context;
        }
        public void Create(Movie item)
        {
            try
            {
                List<Genre> genres = new List<Genre>();

                foreach (Genre g in item.Genres)
                {
                    Genre genreFromDB = _context.Genres.Find(g.ID);

                    if (genreFromDB != null)
                    {
                        genres.Add(genreFromDB);
                    }
                    else
                    {
                        genres.Add(g);
                    }
                }

                item.Genres = genres;

                _context.Movies.Add(item);
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
                _context.Movies.Remove(Read(key));
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Movie Read(int key)
        {
            try
            {
                return _context.Movies.Include(m => m.Genres).Include(m => m.Users).SingleOrDefault(m => m.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Movie> ReadAll()
        {
            try
            {
                return _context.Movies.Include(m => m.Genres).Include(m => m.Users).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Movie item)
        {
            try
            {
                _context.Movies.Update(item);
                _context.SaveChanges();  
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
