using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Repository;

namespace ShopSphere.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Category, int> repo;
        private readonly Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> _localizer;

        public CategoriesController(IEntityRepo<Category, int> _repo, Microsoft.Extensions.Localization.IStringLocalizer<CategoriesController> localizer)
        {
            repo = _repo;
            _localizer = localizer;
        }
        #endregion

        #region Public Actions
        public IActionResult Index()
        {
            return View(repo.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            repo.Add(category);
            repo.SaveChanges();
            TempData["Success"] = _localizer["تمت إضافة الفئة بنجاح"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var category = repo.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            repo.Update(category);
            repo.SaveChanges();
            TempData["Success"] = _localizer["تم تعديل الفئة بنجاح"].Value;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                repo.Delete(id);
                repo.SaveChanges();
                TempData["Success"] = _localizer["تم حذف الفئة بنجاح"].Value;
            }
            catch (Exception)
            {
                TempData["Error"] = _localizer["لا يمكن حذف هذه الفئة لأنها تحتوي على منتجات مرتبطة بها."].Value;
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}