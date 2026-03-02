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
                    new Product { Name = "MacBook Air M2 13-inch", SKU = "EL-AP-MBA13", Price = 42000, StockQuantity = 7, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Apple MacBook Air M2 8GB RAM 256GB SSD", ImageUrl = "/images/product/MacBook Air M2 13-inch.jpeg" },
                    new Product { Name = "Samsung Galaxy S24 Ultra", SKU = "EL-SS-S24U", Price = 47000, StockQuantity = 10, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Samsung Galaxy S24 Ultra 256GB", ImageUrl = "/images/product/Samsung Galaxy S24 Ultra.jpeg" },
                    new Product { Name = "LG 65 Inch OLED TV", SKU = "EL-LG-OLED65", Price = 58000, StockQuantity = 4, IsActive = true, CategoryId = getCatId("Electronics"), Description = "LG 65 Inch 4K OLED Smart TV", ImageUrl = "/images/product/LG 65 Inch OLED TV.jpeg" },
                    new Product { Name = "JBL Bluetooth Speaker Charge 5", SKU = "EL-JB-CH5", Price = 5200, StockQuantity = 18, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Portable Waterproof Bluetooth Speaker", ImageUrl = "/images/product/JBL Bluetooth Speaker Charge 5.jpeg" },
                    new Product { Name = "Dell 27 Inch Monitor 144Hz", SKU = "EL-DL-MN27", Price = 12500, StockQuantity = 9, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Gaming Monitor 144Hz Full HD", ImageUrl = "/images/product/Dell 27 Inch Monitor 144Hz.jpeg" },
                    new Product { Name = "iPhone 15 Pro", SKU = "EL-AP-IP15", Price = 55000, StockQuantity = 15, IsActive = true, CategoryId = getCatId("Electronics"), Description = "Apple iPhone 15 Pro 256GB Titanium Blue", ImageUrl = "/images/product/iPhone 15 Pro.jpeg" }, new Product { Name = "Samsung QLED 4K TV", SKU = "EL-SS-TV55", Price = 32000, StockQuantity = 8, IsActive = true, CategoryId = getCatId("Electronics"), Description = "55 Inch Smart TV", ImageUrl = "/images/product/Samsung QLED 4K TV.jpeg" }, new Product { Name = "Sony DualSense Controller", SKU = "EL-SN-PS5C", Price = 3500, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Electronics"), Description = "PS5 Wireless Controller", ImageUrl = "/images/product/Sony DualSense Controller.jpeg" },
                    // Fashion
                    new Product { Name = "Adidas Hoodie Essentials", SKU = "FS-AD-HOOD", Price = 3200, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Fashion"), Description = "Comfortable cotton hoodie", ImageUrl = "/images/product/Adidas Hoodie Essentials.jpeg" },
                    new Product { Name = "Women's Handbag Leather Black", SKU = "FS-WM-HB", Price = 4100, StockQuantity = 14, IsActive = true, CategoryId = getCatId("Fashion"), Description = "Elegant black leather handbag", ImageUrl = "/images/product/Women Handbag Leather Black.jpeg" },
                    new Product { Name = "Casio Stainless Steel Watch", SKU = "FS-CS-WT", Price = 2800, StockQuantity = 20, IsActive = true, CategoryId = getCatId("Fashion"), Description = "Classic stainless steel wrist watch", ImageUrl = "/images/product/Casio Stainless Steel Watch.jpeg" },
                    new Product { Name = "Men Leather Jacket", SKU = "FS-MN-LJ", Price = 4800, StockQuantity = 10, IsActive = true, CategoryId = getCatId("Fashion"), Description = "Genuine Brown Leather Jacket", ImageUrl = "/images/product/Men Leather Jacket.jpeg" }, new Product { Name = "Nike Running Shoes Air Zoom Pegasus", SKU = "FS-NK-RUN", Price = 6200, StockQuantity = 20, IsActive = true, CategoryId = getCatId("Fashion"), Description = "High performance running shoes", ImageUrl = "/images/product/Nike Running Shoes Air Zoom Pegasus.jpeg" },
                    // Home & Kitchen
                    new Product { Name = "Air Fryer 5L", SKU = "HK-PH-AF", Price = 7500, StockQuantity = 12, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "Digital Air Fryer for healthy cooking", ImageUrl = "/images/product/Air Fryer 5L.jpeg" }, new Product { Name = "Espresso Coffee Machine 15-Bar", SKU = "HK-DL-EM", Price = 12000, StockQuantity = 5, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "Professional Coffee Machine", ImageUrl = "/images/product/Espresso Coffee Machine 15-Bar.jpeg" },
                    new Product { Name = "Microwave Oven 30L Digital", SKU = "HK-MW-30L", Price = 8900, StockQuantity = 8, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "30L Digital Microwave Oven", ImageUrl = "/images/product/Microwave Oven 30L Digital.jpeg" },
                    new Product { Name = "Blender 1000W High Speed", SKU = "HK-BL-1000", Price = 3200, StockQuantity = 15, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "High power kitchen blender", ImageUrl = "/images/product/Blender 1000W High Speed.jpeg" },
                    new Product { Name = "Vacuum Cleaner Bagless", SKU = "HK-VC-BG", Price = 6700, StockQuantity = 11, IsActive = true, CategoryId = getCatId("Home & Kitchen"), Description = "Bagless vacuum cleaner with strong suction", ImageUrl = "/images/product/Vacuum Cleaner Bagless.jpeg" },                   
                    // Sports & Fitness
                    new Product { Name = "Adjustable Dumbbells 20KG", SKU = "SP-DB-20", Price = 3500, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Home Gym Adjustable Weights", ImageUrl = "/images/product/Adjustable Dumbbells 20KG.jpeg" }, new Product { Name = "Electric Treadmill Foldable", SKU = "SP-TM-EL", Price = 18000, StockQuantity = 6, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Foldable Home Treadmill", ImageUrl = "/images/product/Electric Treadmill Foldable.jpeg" },
                    new Product { Name = "Yoga Mat Anti-Slip", SKU = "SP-YG-MAT", Price = 900, StockQuantity = 40, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Eco-friendly anti-slip yoga mat", ImageUrl = "/images/product/Yoga Mat Anti-Slip.jpeg" },
                    new Product { Name = "Protein Powder Whey 2KG", SKU = "SP-WH-2KG", Price = 2800, StockQuantity = 18, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Premium whey protein 2KG", ImageUrl = "/images/product/Protein Powder Whey 2KG.jpeg" },
                    new Product { Name = "Exercise Bike Magnetic", SKU = "SP-EB-MG", Price = 14500, StockQuantity = 5, IsActive = true, CategoryId = getCatId("Sports & Fitness"), Description = "Magnetic resistance exercise bike", ImageUrl = "/images/product/Exercise Bike Magnetic.jpeg" }, 
                    // Automotive
                    new Product { Name = "Car Vacuum Cleaner Portable", SKU = "AU-VC-01", Price = 2200, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Automotive"), Description = "Portable High Power Vacuum", ImageUrl = "/images/product/Car Vacuum Cleaner Portable.jpeg" }, new Product { Name = "Car Phone Holder Dashboard Mount", SKU = "AU-PH-02", Price = 450, StockQuantity = 50, IsActive = true, CategoryId = getCatId("Automotive"), Description = "360° Dashboard Mount", ImageUrl = "/images/product/Car Phone Holder Dashboard Mount.jpeg" },
                    new Product { Name = "Car Air Compressor Portable", SKU = "AU-AC-01", Price = 1900, StockQuantity = 22, IsActive = true, CategoryId = getCatId("Automotive"), Description = "Portable tire inflator air compressor", ImageUrl = "/images/product/Car Air Compressor Portable.jpeg" },
                    new Product { Name = "Dash Cam Full HD 1080p", SKU = "AU-DC-1080", Price = 3200, StockQuantity = 16, IsActive = true, CategoryId = getCatId("Automotive"), Description = "Full HD dashboard camera", ImageUrl = "/images/product/Dash Cam Full HD 1080p.jpeg" },
                    // Toys & Games
                    new Product { Name = "LEGO Classic Box", SKU = "TY-LG-CL", Price = 1800, StockQuantity = 40, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "Creative Building Blocks", ImageUrl = "/images/product/LEGO Classic Box.jpeg" }, new Product { Name = "Remote Control Car High Speed", SKU = "TY-RC-CAR", Price = 2500, StockQuantity = 22, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "High Speed RC Car", ImageUrl = "/images/product/Remote Control Car High Speed.jpeg" },
                    new Product { Name = "Monopoly Classic Board Game", SKU = "TY-MN-CL", Price = 1200, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "Classic Monopoly family board game", ImageUrl = "/images/product/Monopoly Classic Board Game.jpeg" },
                    new Product { Name = "Puzzle 1000 Pieces Landscape", SKU = "TY-PZ-1000", Price = 750, StockQuantity = 35, IsActive = true, CategoryId = getCatId("Toys & Games"), Description = "1000 pieces landscape puzzle", ImageUrl = "/images/product/Puzzle 1000 Pieces Landscape.jpeg" },
                    // Books
                    new Product { Name = "The Lean Startup by Eric Ries", SKU = "BK-BS-LS", Price = 850, StockQuantity = 50, IsActive = true, CategoryId = getCatId("Books"), Description = "How Today's Entrepreneurs Use Continuous Innovation", ImageUrl = "/images/product/The Lean Startup by Eric Ries.jpeg" }, new Product { Name = "Atomic Habits by James Clear", SKU = "BK-SF-AH", Price = 950, StockQuantity = 45, IsActive = true, CategoryId = getCatId("Books"), Description = "An Easy & Proven Way to Build Good Habits", ImageUrl = "/images/product/Atomic Habits by James Clear.jpeg" },
                    new Product { Name = "Deep Work by Cal Newport", SKU = "BK-PD-DW", Price = 880, StockQuantity = 40, IsActive = true, CategoryId = getCatId("Books"), Description = "Rules for Focused Success in a Distracted World", ImageUrl = "/images/product/Deep Work by Cal Newport.jpeg" },
                    new Product { Name = "Clean Code by Robert C. Martin", SKU = "BK-PR-CC", Price = 1100, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Books"), Description = "A Handbook of Agile Software Craftsmanship", ImageUrl = "/images/product/Clean Code by Robert C. Martin.jpeg" },   
                    // Beauty
                    new Product { Name = "Hair Dryer Ionic 2200W", SKU = "BT-HD-2200", Price = 1600, StockQuantity = 28, IsActive = true, CategoryId = getCatId("Beauty & Personal Care"), Description = "Professional ionic hair dryer", ImageUrl = "/images/product/Hair Dryer Ionic 2200W.jpeg" },
                    new Product { Name = "Men Beard Grooming Kit", SKU = "BT-BG-MN", Price = 2100, StockQuantity = 19, IsActive = true, CategoryId = getCatId("Beauty & Personal Care"), Description = "Complete beard care kit", ImageUrl = "/images/product/Men Beard Grooming Kit.jpeg" },
                    new Product { Name = "Skin Care Routine Kit", SKU = "BT-OR-SK", Price = 2800, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Beauty & Personal Care"), Description = "Essential daily skin care set", ImageUrl = "/images/product/Skin Care Routine Kit.jpeg" },
                    
                    // Grocery
                    new Product { Name = "Basmati Rice 5KG", SKU = "GR-RC-5KG", Price = 350, StockQuantity = 50, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Premium long grain basmati rice", ImageUrl = "/images/product/Basmati Rice 5KG.jpeg" },
                    new Product { Name = "Sunflower Cooking Oil 2L", SKU = "GR-OIL-2L", Price = 180, StockQuantity = 75, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Pure sunflower cooking oil", ImageUrl = "/images/product/Sunflower Cooking Oil 2L.jpeg" },
                    new Product { Name = "Granulated Sugar 1KG", SKU = "GR-SG-1KG", Price = 45, StockQuantity = 120, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Refined white sugar", ImageUrl = "/images/product/Granulated Sugar 1KG.jpeg" },
                    new Product { Name = "Full Cream Milk 1L", SKU = "GR-ML-1L", Price = 30, StockQuantity = 90, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Fresh full cream milk", ImageUrl = "/images/product/Full Cream Milk 1L.jpeg" },
                    new Product { Name = "Spaghetti Pasta 500g", SKU = "GR-PT-500", Price = 25, StockQuantity = 80, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Durum wheat spaghetti pasta", ImageUrl = "/images/product/Spaghetti Pasta 500g.jpeg" },
                    new Product { Name = "Canned Tuna in Oil 170g", SKU = "GR-TN-170", Price = 55, StockQuantity = 60, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Tuna chunks in sunflower oil", ImageUrl = "/images/product/Canned Tuna 170g.jpeg" },
                    new Product { Name = "Tomato Paste 300g", SKU = "GR-TM-300", Price = 15, StockQuantity = 110, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Concentrated tomato paste", ImageUrl = "/images/product/Tomato Paste 300g.jpeg" },
                    new Product { Name = "Tea Bags 100pcs", SKU = "GR-TEA-100", Price = 120, StockQuantity = 55, IsActive = true, CategoryId = getCatId("Grocery"), Description = "Black tea bags pack", ImageUrl = "/images/product/Tea Bags 100pcs.jpeg" },

                    // Office Supplies
                    new Product { Name = "A4 Copy Paper 500 Sheets", SKU = "OS-PP-A4", Price = 220, StockQuantity = 40, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "High quality A4 printing paper", ImageUrl = "/images/product/A4 Copy Paper 500 Sheets.jpeg" },
                    new Product { Name = "Ballpoint Pen Pack (10pcs)", SKU = "OS-PN-10", Price = 75, StockQuantity = 85, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "Smooth writing blue ink pens", ImageUrl = "/images/product/Ballpoint Pen Pack.jpeg" },
                    new Product { Name = "Office Stapler Heavy Duty", SKU = "OS-ST-HD", Price = 140, StockQuantity = 25, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "Durable heavy duty stapler", ImageUrl = "/images/product/Office Stapler Heavy Duty.jpeg" },
                    new Product { Name = "Spiral Notebook A5", SKU = "OS-NB-A5", Price = 60, StockQuantity = 70, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "200 pages ruled spiral notebook", ImageUrl = "/images/product/Spiral Notebook A5.jpeg" },
                    new Product { Name = "Plastic Ruler 30cm", SKU = "OS-RL-30", Price = 20, StockQuantity = 100, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "30cm transparent plastic ruler", ImageUrl = "/images/product/Plastic Ruler 30cm.jpeg" },
                    new Product { Name = "Highlighters Set (5 Colors)", SKU = "OS-HL-5C", Price = 95, StockQuantity = 60, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "Assorted color highlighters", ImageUrl = "/images/product/Highlighters Set.jpeg" },
                    new Product { Name = "Sticky Notes 3x3 100 Sheets", SKU = "OS-SN-3x3", Price = 45, StockQuantity = 90, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "Yellow sticky notes pad", ImageUrl = "/images/product/Sticky Notes 3x3.jpeg" },
                    new Product { Name = "Desk Organizer Tray", SKU = "OS-DT-ORG", Price = 220, StockQuantity = 30, IsActive = true, CategoryId = getCatId("Office Supplies"), Description = "Multi-section desk organizer", ImageUrl = "/images/product/Desk Organizer Tray.jpeg" },
                };

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
