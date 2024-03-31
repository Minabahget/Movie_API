using Microsoft.EntityFrameworkCore;
using Movie_Api.Models;

namespace Movie_Api.Repository
{
    public class CategoryRepoService(MovieDbContext _context) : IRepositry<Category>
    {
        private readonly MovieDbContext context = _context;

        public bool Add(Category category)
        {

            if (category != null)
            {

                context.Add(category);
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public Category Details(int id)
        {
            if (id > 0)
            {
                var DetailsOfCategory = context.Categories.Include(m => m.Movies).Where(c => c.Id == id).FirstOrDefault();
                return DetailsOfCategory;
            }
            return null;
        }

        public List<Category> GetAll()
        {
            return context.Categories.Include(m => m.Movies).ToList();
        }

        public bool Remove(Category category)
        {
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Category category)
        {
            if (category != null)
            {
                context.Categories.Update(category);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
