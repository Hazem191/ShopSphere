using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using System.Linq.Expressions;

namespace ShopSphere.Repository
{

    public interface ICategoryRepo : IEntityRepo<Category, int>
    {

    }
    public class CategoryRepo : EntityRepo<Category, int>, ICategoryRepo
    {
        private readonly ShopSphereDB _context;
        public CategoryRepo(ShopSphereDB context) : base(context)
        {
            _context = context;
        }

        public override List<Category> GetAll()
        {
            return _context.Categories.Include(c => c.Products).ToList();
        }
    }    
}
