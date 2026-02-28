using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;
using ShopSphere.ViewModels;

namespace ShopSphere.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Order, int> orderRepo;
        private readonly IEntityRepo<Address, int> addressRepo;
        private readonly IEntityRepo<Product, int> productRepo;
        private readonly IEntityRepo<OrderItem, int> orderItemRepo;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ShopSphereDB _context;

        public OrdersController(
            IEntityRepo<Order, int> _orderRepo,
            IEntityRepo<Address, int> _addressRepo,
            IEntityRepo<Product, int> _productRepo,
            IEntityRepo<OrderItem, int> _orderItemRepo,
            UserManager<ApplicationUser> _userManager,
            ShopSphereDB context)
        {
            orderRepo     = _orderRepo;
            addressRepo   = _addressRepo;
            productRepo   = _productRepo;
            orderItemRepo = _orderItemRepo;
            userManager   = _userManager;
            _context      = context;
        }
        #endregion

        #region Private Helper Methods
        private List<CartItemVM> GetCart()
        {
            var json = HttpContext.Session.GetString("Cart");
            return json == null
                ? new List<CartItemVM>()
                : JsonConvert.DeserializeObject<List<CartItemVM>>(json);
        }
        #endregion

        #region Public Actions
        public IActionResult Checkout()
        {
            var userId    = userManager.GetUserId(User);
            var cartItems = GetCart();

            if (!cartItems.Any()) return RedirectToAction("Index", "Cart");

            var vm = new CheckoutVM
            {
                UserAddresses = addressRepo.FindBy(a => a.UserId == userId),
                CartItems     = cartItems,
                TotalAmount   = cartItems.Sum(x => x.LineTotal)
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Checkout(CheckoutVM vm)
        {
            var userId    = userManager.GetUserId(User);
            var cartItems = GetCart();

            if (!cartItems.Any()) return RedirectToAction("Index", "Cart");

            int addressId = vm.SelectedAddressId;
            if (vm.NewAddress != null &&
                !string.IsNullOrWhiteSpace(vm.NewAddress.City) &&
                !string.IsNullOrWhiteSpace(vm.NewAddress.Street) &&
                !string.IsNullOrWhiteSpace(vm.NewAddress.Building))
            {
                if (vm.NewAddress.IsDefault)
                {
                    var oldDefaults = addressRepo.FindBy(a => a.UserId == userId && a.IsDefault);
                    foreach (var old in oldDefaults)
                    {
                        old.IsDefault = false;
                        addressRepo.Update(old);
                    }
                }

                var newAddr = new Address
                {
                    UserId    = userId,
                    City      = vm.NewAddress.City,
                    Street    = vm.NewAddress.Street,
                    Building  = vm.NewAddress.Building,
                    IsDefault = vm.NewAddress.IsDefault
                };
                addressRepo.Add(newAddr);
                addressRepo.SaveChanges();
                addressId = newAddr.Id;
            }

            if (addressId == 0)
            {
                TempData["Error"] = "Please enter a shipping address to complete your order.";
                return RedirectToAction("Checkout");
            }

            // Stock validation
            foreach (var item in cartItems)
            {
                var product = productRepo.GetById(item.ProductId);
                if (product == null || product.StockQuantity < item.Quantity)
                {
                    TempData["Error"] = $"Product '{item.ProductName}' is not available in the requested quantity.";
                    return RedirectToAction("Checkout");
                }
            }

            var order = new Order
            {
                OrderNumber = $"ORD-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                Status      = "Pending",
                TotalAmount = cartItems.Sum(x => x.LineTotal),
                UserId      = userId,
                AddressId   = addressId,
                CreatedAt   = DateTime.UtcNow
            };

            foreach (var item in cartItems)
            {
                var product = productRepo.GetById(item.ProductId);

                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity  = item.Quantity,
                    UnitPrice = item.Price,
                    LineTotal = item.LineTotal
                });

                product.StockQuantity -= item.Quantity;
                productRepo.Update(product);
            }

            orderRepo.Add(order);
            orderRepo.SaveChanges();

            HttpContext.Session.Remove("Cart");

            TempData["Success"] = "Order placed successfully! Your order number is " + order.OrderNumber;
            return RedirectToAction("Details", new { id = order.Id });
        }

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.Address)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            var userId = userManager.GetUserId(User);
            if (order.UserId != userId) return Forbid();

            var items = _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == id)
                .ToList();

            var vm = new OrderDetailsVM
            {
                Order           = order,
                Items           = items,
                ShippingAddress = order.Address
            };

            return View(vm);
        }
        #endregion
    }
}
