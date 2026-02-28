using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;
using System.Linq.Expressions;

public interface IAddressRepo : IEntityRepo<Address, int>
{

}

public class AddressRepo : EntityRepo<Address, int>, IAddressRepo
{
    public AddressRepo(ShopSphereDB context) : base(context)
    {
    }
}
