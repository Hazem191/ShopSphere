using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Models;
using ShopSphere.Repository;
using System.Security.Claims;

namespace ShopSphere.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        #region Fields & Constructor
        private readonly IWishlistRepo wishlistRepo;
        private readonly IEntityRepo<Product, int> productRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public WishlistController(
            IWishlistRepo _wishlistRepo,
            IEntityRepo<Product, int> _productRepo,
            UserManager<ApplicationUser> _userManager)
        {
            wishlistRepo = _wishlistRepo;
            productRepo = _productRepo;
            userManager = _userManager;
        }
        #endregion


        #region Public Actions

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(User);
            var items = wishlistRepo.GetAll()
                .Where(w => w.UserId == userId)
                .Select(w => w.Product)
                .ToList();
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                ViewBag.Cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShopSphere.ViewModels.CartItemVM>>(cartJson);
            }
            return View(items);
        }

        [HttpPost]
        public IActionResult Toggle(int productId)
        {
            var userId = userManager.GetUserId(User);
            var existing = wishlistRepo.FindBy(w => w.UserId == userId && w.ProductId == productId).FirstOrDefault();

            if (existing != null)
            {
                wishlistRepo.Delete(existing.Id);
                wishlistRepo.SaveChanges();
                return Json(new { success = true, added = false, message = "Removed from wishlist" });
            }
            else
            {
                var item = new WishlistItem
                {
                    UserId = userId,
                    ProductId = productId
                };
                wishlistRepo.Add(item);
                wishlistRepo.SaveChanges();
                return Json(new { success = true, added = true, message = "Added to wishlist" });
            }
        }
        #endregion
    }
}
