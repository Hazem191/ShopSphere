using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using ShopSphere.Repository;

namespace ShopSphere.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        #region Fields & Constructor
        private readonly IEntityRepo<Order, int> orderRepo;
        private readonly IEntityRepo<OrderItem, int> orderItemRepo;
        private readonly ShopSphereDB _context;

        public AdminOrdersController(
            IEntityRepo<Order, int> _orderRepo,
            IEntityRepo<OrderItem, int> _orderItemRepo,
            ShopSphereDB context)
        {
            orderRepo     = _orderRepo;
            orderItemRepo = _orderItemRepo;
            _context      = context;
        }
        #endregion

        #region Public Actions
        public IActionResult Index()
        {
            var orders = _context.Orders
                .Include(o => o.User)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Address)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            var items = _context.OrderItems
                .Include(oi => oi.Product)
                .Where(oi => oi.OrderId == id)
                .ToList();

            ViewBag.Items = items;
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, string status)
        {
            var order = orderRepo.GetById(id);
            if (order == null) return NotFound();
            order.Status = status;
            orderRepo.Update(order);
            orderRepo.SaveChanges();
            TempData["Success"] = "Order status updated successfully.";
            return RedirectToAction("Details", new { id });
        }
        #endregion
    }
}
