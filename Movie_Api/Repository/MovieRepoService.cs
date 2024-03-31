using Microsoft.EntityFrameworkCore;
using Movie_Api.Models;

namespace Movie_Api.Repository
{
    public class MovieRepoService(MovieDbContext _context) : IRepositry<Movie>
    {
        private readonly MovieDbContext context = _context;
        public bool Add(Movie movie)
        {
            if (movie != null)
            {
                context.Add(movie);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public Movie Details(int id)
        {
            if (id > 0)
            {
                var DetailsOfMovie = context.Movies.Include(m => m.Categories).Where(c => c.Id == id).FirstOrDefault();
                return DetailsOfMovie;
            }
            return null;
        }

        public List<Movie> GetAll()
        {
            return context.Movies.Include(m => m.Categories).ToList();
        }

        public bool Remove(Movie movie)
        {
            if (movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Movie movie)
        {
            if (movie != null)
            {
                context.Movies.Update(movie);
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
