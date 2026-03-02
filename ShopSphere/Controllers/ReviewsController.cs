using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopSphere.Models;
using ShopSphere.Repository;
using ShopSphere.ViewModels;

namespace ShopSphere.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Review, int> reviewRepo;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(
            IEntityRepo<Review, int> _reviewRepo,
            UserManager<ApplicationUser> _userManager)
        {
            reviewRepo = _reviewRepo;
            userManager = _userManager;
        }
        #endregion

        #region Public Actions
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ReviewVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(User);
                
                var existing = reviewRepo.FindBy(r => r.UserId == userId && r.ProductId == model.ProductId).FirstOrDefault();
                if (existing != null)
                {
                    return Json(new { success = false, message = "You have already reviewed this product." });
                }

                var review = new Review
                {
                    ProductId = model.ProductId,
                    UserId = userId,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    CreatedAt = DateTime.UtcNow
                };

                reviewRepo.Add(review);
                reviewRepo.SaveChanges();

                return Json(new { success = true, message = "Review submitted successfully!" });
            }

            return Json(new { success = false, message = "Invalid data." });
        }
        #endregion
    }
}
