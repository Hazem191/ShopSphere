using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models
{
    public class OrderItem
    {

        public int Id { get; set; }

        [Range(1, 10000)]
        public int Quantity { get; set; }

        [Range(0.01, 999999)]
        public decimal UnitPrice { get; set; }

        [Range(0.01, 99999999)]
        public decimal LineTotal { get; set; }

        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
