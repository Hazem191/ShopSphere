using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ShopSphere.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string OrderNumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Range(0.01, 99999999)]
        public decimal TotalAmount { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
