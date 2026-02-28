using ShopSphere.Models;

namespace ShopSphere.ViewModels
{
    public class OrderDetailsVM
    {
        public Order Order { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public Address ShippingAddress { get; set; }
    }
}
