using ShopSphere.Models;

namespace ShopSphere.ViewModels
{
    public class NewAddressVM
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CheckoutVM
    {
        public int SelectedAddressId { get; set; }

        public List<Address> UserAddresses { get; set; }

        public List<CartItemVM> CartItems { get; set; }

        public decimal TotalAmount { get; set; }

        // Inline new address
        public NewAddressVM NewAddress { get; set; }
    }
}
