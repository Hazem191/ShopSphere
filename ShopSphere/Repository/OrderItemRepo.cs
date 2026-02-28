using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;
using System.Linq.Expressions;

public interface IOrderItemRepo : IEntityRepo<OrderItem, int>
{

}

public class OrderItemRepo : EntityRepo<OrderItem, int>, IOrderItemRepo
{
    private readonly ShopSphereDB context;

    public OrderItemRepo(ShopSphereDB _context) : base(_context)
    {
        
    }

}