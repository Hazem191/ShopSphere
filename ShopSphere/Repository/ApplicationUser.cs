using ShopSphere.Data;
using ShopSphere.Models;
using System.Linq.Expressions;

namespace ShopSphere.Repository
{
    public interface IUserRepo : IEntityRepo<ApplicationUser, string>
    {

    }
    public class UserRepo : EntityRepo<ApplicationUser, string>
    {

        public UserRepo(ShopSphereDB _context) : base(_context)
        {
        }

    }
}

