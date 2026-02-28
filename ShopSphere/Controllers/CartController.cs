using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using ShopSphere.Extensions;
using ShopSphere.Models;
using ShopSphere.Repository;
using ShopSphere.ViewModels;

namespace ShopSphere.Controllers
{
    public class CartController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Product, int> productRepo;
        private readonly IStringLocalizer<CartController> _localizer;

        public CartController(IEntityRepo<Product, int> _repo, IStringLocalizer<CartController> localizer)
        {
            productRepo = _repo;
            _localizer = localizer;
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

        private void SaveCart(List<CartItemVM> cart)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
        #endregion

        #region Public Actions
        public IActionResult Index()
        {
            return View(GetCart());
        }

        // Works as GET (direct link) and POST (AJAX)
        [AcceptVerbs("GET", "POST")]
        public IActionResult Add(int productId)
        {
            var product = productRepo.GetById(productId);
            if (product == null)
                return Request.IsAjax() ? Json(new { success = false }) : NotFound();

            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item == null)
            {
                cart.Add(new CartItemVM
                {
                    ProductId   = product.Id,
                    ProductName = product.Name,
                    Price       = product.Price,
                    Quantity    = 1,
                    ImageUrl    = product.ImageUrl
                });
            }
            else
            {
                item.Quantity++;
            }

            SaveCart(cart);

            // AJAX request → return JSON
            if (Request.IsAjax())
            {
                int totalItems = cart.Sum(c => c.Quantity);
                string message = string.Format(_localizer["تمت إضافة {0} إلى السلة"].Value, product.Name);
                return Json(new { success = true, message, totalItems });
            }

            // Normal GET (e.g. from Details page) → redirect back
            TempData["Success"] = product.Name + " added to cart!";
            return RedirectToAction("Index", "Catalog");
        }

        // AJAX: update quantity by delta (+1 or -1)
        [AcceptVerbs("GET", "POST")]
        public IActionResult UpdateQty(int productId, int delta)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                item.Quantity += delta;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }
            SaveCart(cart);

            int totalItems = cart.Sum(c => c.Quantity);
            decimal newLineTotal = (item != null && item.Quantity > 0) ? item.Quantity * item.Price : 0;
            decimal grandTotal   = cart.Sum(c => c.LineTotal);

            return Json(new { success = true, totalItems, newQty = item?.Quantity ?? 0, newLineTotal, grandTotal });
        }

        public IActionResult Update(int productId, int qty)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (qty <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = qty;
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var cart = GetCart();
            cart.RemoveAll(c => c.ProductId == productId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        #endregion
    }
}
