using Microsoft.EntityFrameworkCore;
using ShopSphere.Models;

namespace ShopSphere.Data
{
    public static class DbInitializer
    {
        public static void Seed(ShopSphereDB context)
        {
            context.Database.EnsureCreated();

            if (!context.Categories.Any())
            {
                var categories = new Category[]
                {
                    new Category { Name = "Electronics" },
                    new Category { Name = "Fashion" },
                    new Category { Name = "Home & Kitchen" },
                    new Category { Name = "Books" },
                    new Category { Name = "Beauty & Personal Care" },
                    new Category { Name = "Sports & Fitness" },
                    new Category { Name = "Toys & Games" },
                    new Category { Name = "Automotive" },
                    new Category { Name = "Grocery" },
                    new Category { Name = "Office Supplies" }
                };
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                // Get categories from DB to ensure we have correct IDs
                var dbCategories = context.Categories.ToList();
                
                var getCatId = (string name) => dbCategories.FirstOrDefault(c => c.Name == name)?.Id ?? dbCategories[0].Id;

                var products = new Product[]
                {
                    // Electronics
                    new Product { Name = "iPhone 15 Pro", SKU = "EL-AP-IP15", Price = 55000, StockQuantity = 15, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Apple iPhone 15 Pro 256GB Titanium Blue", ImageUrl = "/images/product/iPhone 15 Pro.jpeg" },
                    new Product { Name = "Samsung QLED 4K TV", SKU = "EL-SS-TV55", Price = 32000, StockQuantity = 8, IsActive = true, CategoryId = getCatId("Electronics"), Description = "55 Inch Smart TV", ImageUrl = "/images/product/Samsung QLED 4K TV.jpeg" },
                    new Product { Name = "Sony DualSense Controller", SKU = "EL-SN-PS5C", Price = 3500, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Electronics"), Description = "PS5 Wireless Controller", ImageUrl = "/images/product/Sony DualSense Controller.jpeg" },

                    // Fashion
                    new Product { Name = "Men Leather Jacket", SKU = "FS-MN-LJ", Price = 4800, StockQuantity = 10, IsActive = true, CategoryId = getCatId("Fashion"), Description = "Genuine Brown Leather Jacket", ImageUrl = "/images/product/Men Leather Jacket.jpeg" },
                    new Product { Name = "Nike Running Shoes Air Zoom Pegasus", SKU = "FS-NK-RUN", Price = 6200, StockQuantity = 20, IsActive = true, CategoryId = getCatId("Fashion"), Description = "High performance running shoes", ImageUrl = "/images/product/Nike Running Shoes Air Zoom Pegasus.jpeg" },
                    
                    // Home & Kitchen
                    new Product { Name = "Air Fryer 5L", SKU = "HK-PH-AF", Price = 7500, StockQuantity = 12, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "Digital Air Fryer for healthy cooking", ImageUrl = "/images/product/Air Fryer 5L.jpeg" },
                    new Product { Name = "Espresso Coffee Machine 15-Bar", SKU = "HK-DL-EM", Price = 12000, StockQuantity = 5, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "Professional Coffee Machine", ImageUrl = "/images/product/Espresso Coffee Machine 15-Bar.jpeg" },
                    
                    // Sports & Fitness
                    new Product { Name = "Adjustable Dumbbells 20KG", SKU = "SP-DB-20", Price = 3500, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Home Gym Adjustable Weights", ImageUrl = "/images/product/Adjustable Dumbbells 20KG.jpeg" },
                    new Product { Name = "Electric Treadmill Foldable", SKU = "SP-TM-EL", Price = 18000, StockQuantity = 6, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Foldable Home Treadmill", ImageUrl = "/images/product/Electric Treadmill Foldable.jpeg" },
                    
                    // Automotive
                    new Product { Name = "Car Vacuum Cleaner Portable", SKU = "AU-VC-01", Price = 2200, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Automotive"), Description = "Portable High Power Vacuum", ImageUrl = "/images/product/Car Vacuum Cleaner Portable.jpeg" },
                    new Product { Name = "Car Phone Holder Dashboard Mount", SKU = "AU-PH-02", Price = 450, StockQuantity = 50, IsActive = true, CategoryId = getCatId("Automotive"), Description = "360° Dashboard Mount", ImageUrl = "/images/product/Car Phone Holder Dashboard Mount.jpeg" },
                    
                    // Toys & Games
                    new Product { Name = "LEGO Classic Box", SKU = "TY-LG-CL", Price = 1800, StockQuantity = 40, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "Creative Building Blocks", ImageUrl = "/images/product/LEGO Classic Box.jpeg" },
                    new Product { Name = "Remote Control Car High Speed", SKU = "TY-RC-CAR", Price = 2500, StockQuantity = 22, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "High Speed RC Car", ImageUrl = "/images/product/Remote Control Car High Speed.jpeg" },
                    
                    // Books
                    new Product { Name = "The Lean Startup by Eric Ries", SKU = "BK-BS-LS", Price = 850, StockQuantity = 50, IsActive = true, CategoryId = getCatId("Books"), Description = "How Today's Entrepreneurs Use Continuous Innovation", ImageUrl = "/images/product/The Lean Startup by Eric Ries.jpeg" },
                    new Product { Name = "Atomic Habits by James Clear", SKU = "BK-SF-AH", Price = 950, StockQuantity = 45, IsActive = true, CategoryId = getCatId("Books"), Description = "An Easy & Proven Way to Build Good Habits", ImageUrl = "/images/product/Atomic Habits by James Clear.jpeg" },
                    
                    // Beauty
                    new Product { Name = "Skin Care Routine Kit", SKU = "BT-OR-SK", Price = 2800, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Beauty & Personal Care"), Description = "Essential daily skin care set", ImageUrl = "/images/product/Skin Care Routine Kit.jpeg" }
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
