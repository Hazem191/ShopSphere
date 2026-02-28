using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;
using System.Linq.Expressions;

public interface IProductRepo : IEntityRepo<Product, int>
{

}

public class ProductRepo : EntityRepo<Product, int>, IProductRepo
{
    private readonly ShopSphereDB _context;
    public ProductRepo(ShopSphereDB context) : base(context)
    {
        _context = context;
    }

    public override List<Product> GetAll()
    {
        return _context.Products.Include(p => p.Category).ToList();
    }
}