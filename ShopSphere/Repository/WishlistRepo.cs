using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;

namespace ShopSphere.Repository
{
    public interface IWishlistRepo : IEntityRepo<WishlistItem, int>
    {
    }

    public class WishlistRepo : EntityRepo<WishlistItem, int>, IWishlistRepo
    {
        private readonly ShopSphereDB _context;
        public WishlistRepo(ShopSphereDB context) : base(context)
        {
            _context = context;
        }

        public override List<WishlistItem> GetAll()
        {
            return _context.WishlistItems.Include(w => w.Product).ToList();
        }
    }
}
