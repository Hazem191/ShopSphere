using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopSphere.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
