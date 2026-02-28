using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;

public interface IOrderRepo : IEntityRepo<Order, int>
{
}

public class OrderRepo : EntityRepo<Order, int>, IOrderRepo
{
    public OrderRepo(ShopSphereDB context) : base(context)
    {
    }
}