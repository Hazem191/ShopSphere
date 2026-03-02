using System.ComponentModel.DataAnnotations;

namespace ShopSphere.ViewModels
{
    public class ReviewVM
    {
        public int ProductId { get; set; }
        
        [Required(ErrorMessage = "Please select a rating")]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Please enter your comment")]
        [StringLength(1000)]
        public string Comment { get; set; }

        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
