using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Repository;

namespace ShopSphere.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Product, int> productRepo;
        private readonly IEntityRepo<Category, int> categoryRepo;
        private readonly Microsoft.Extensions.Localization.IStringLocalizer<ProductsController> _localizer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(
            IEntityRepo<Product, int> _productRepo,
            IEntityRepo<Category, int> _categoryRepo,
            Microsoft.Extensions.Localization.IStringLocalizer<ProductsController> localizer,
            IWebHostEnvironment webHostEnvironment)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
            _localizer = localizer;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Public Actions
        public IActionResult Index()
        {
            var products = productRepo.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            var categories = categoryRepo.GetAll();
            if (!categories.Any())
            {
                TempData["Error"] = _localizer["يرجى إضافة فئات أولاً قبل إضافة المنتجات."].Value;
                return RedirectToAction("Index", "Categories");
            }
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile? productImage)
        {
            if (ModelState.IsValid)
            {
                if (productImage != null && productImage.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        await productImage.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = @"/images/product/" + fileName;
                }

                productRepo.Add(product);
                productRepo.SaveChanges();
                TempData["Success"] = _localizer["تم إضافة المنتج بنجاح"].Value;
                return RedirectToAction("Index");
            }

            ViewBag.Categories = categoryRepo.GetAll();
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = productRepo.GetById(id);
            if (product == null) return NotFound();

            ViewBag.Categories = categoryRepo.GetAll();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product, IFormFile? productImage)
        {
            if (ModelState.IsValid)
            {
                if (productImage != null && productImage.Length > 0)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(productImage.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }

                    // Delete old image if it exists
                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        await productImage.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = @"/images/product/" + fileName;
                }

                productRepo.Update(product);
                productRepo.SaveChanges();
                TempData["Success"] = _localizer["تم تعديل المنتج بنجاح"].Value;
                return RedirectToAction("Index");
            }

            ViewBag.Categories = categoryRepo.GetAll();
            return View(product);
        }

        public IActionResult Delete(int id)
        {
            try 
            {
                var product = productRepo.GetById(id);
                if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                productRepo.Delete(id);
                productRepo.SaveChanges();
                TempData["Success"] = _localizer["تم حذف المنتج بنجاح"].Value;
            }
            catch (Exception)
            {
                TempData["Error"] = _localizer["لا يمكن حذف المنتج لأنه مرتبط بطلبات سابقة."].Value;
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}