using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Repository;
using ShopSphere.ViewModels;

namespace ShopSphere.Controllers
{
    public class CatalogController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Product, int> productRepo;
        private readonly IEntityRepo<Category, int> categoryRepo;
        private const int PageSize = 9;

        public CatalogController(
            IEntityRepo<Product, int> _productRepo,
            IEntityRepo<Category, int> _categoryRepo)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
        }
        #endregion

        #region Public Actions
        public IActionResult Index(int? categoryId, string q, string sort, int page = 1)
        {
            var products = productRepo.GetAll()
                .Where(p => p.IsActive)
                .AsQueryable();

            if (categoryId.HasValue)
                products = products.Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(q))
                products = products.Where(p => p.Name.Contains(q) || p.Description.Contains(q));

            products = sort switch
            {
                "price_asc"  => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "name"       => products.OrderBy(p => p.Name),
                _            => products.OrderByDescending(p => p.CreatedAt)
            };

            int total = products.Count();
            var paged = products.Skip((page - 1) * PageSize).Take(PageSize).ToList();

            var vm = new ProductListVM
            {
                Products           = paged,
                Categories         = categoryRepo.GetAll(),
                SelectedCategoryId = categoryId,
                SearchQuery        = q,
                Sort               = sort,
                CurrentPage        = page,
                TotalPages         = (int)Math.Ceiling(total / (double)PageSize),
                TotalCount         = total
            };

            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                ViewBag.Cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartItemVM>>(cartJson);
            }

            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var product = productRepo.GetById(id);
            if (product == null) return NotFound();

            var cartJson = HttpContext.Session.GetString("Cart");
            int cartQty = 0;
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CartItemVM>>(cartJson);
                cartQty = cart?.FirstOrDefault(c => c.ProductId == id)?.Quantity ?? 0;
            }
            ViewBag.CartQty = cartQty;

            return View(product);
        }
        #endregion
    }
}
