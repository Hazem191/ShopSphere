using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string City { get; set; }

        [Required]
        [StringLength(200)]
        public string Street { get; set; }

        [Required]
        [StringLength(50)]
        public string Building { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
