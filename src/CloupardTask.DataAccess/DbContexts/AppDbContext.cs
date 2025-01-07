using CloupardTask.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CloupardTask.Api.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal properties
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)");

            // Seed data for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Gadgets and devices" },
                new Category { Id = 2, Name = "Clothing", Description = "Men's and Women's Wear" },
                new Category { Id = 3, Name = "Books", Description = "Fiction and Non-Fiction" },
                new Category { Id = 4, Name = "BUNDLES", Description = "A selection of packaged items grouped together for convenience and value. Perfect for customers looking for multiple products at once." },
                new Category { Id = 5, Name = "HERBS", Description = "A variety of fresh, dried, and aromatic herbs that enhance the flavor of dishes and offer natural remedies." },
                new Category { Id = 6, Name = "VEGETABLES", Description = "A wide assortment of fresh, seasonal, and nutrient-rich vegetables, including leafy greens, root vegetables, and more." },
                new Category { Id = 7, Name = "SUPPLIES", Description = "Essential products for everyday use, ranging from gardening tools to home and office supplies." },
                new Category { Id = 8, Name = "FLOWERS", Description = "A colorful collection of fresh-cut flowers for any occasion, designed to bring beauty and fragrance into your home or special event." },
                new Category { Id = 9, Name = "FRUITS", Description = "Fresh, juicy, and seasonal fruits picked to provide the best taste and nutrition, perfect for snacking or adding to meals." }
            );

            // Seed data for Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 16 Pro Max - Premium Smartphone",
                    Price = 1500.99M,
                    CategoryId = 1,
                    ImageUrl = "Images\\phone.webp",
                    Description = "The iPhone 16 Pro Max offers cutting-edge performance, stunning visuals, and an exceptional camera experience. With its 6.7-inch OLED display, this device is perfect for gaming, content creation, and all your daily tasks."
                },
                new Product
                {
                    Id = 2,
                    Name = "MacBook Pro M2 - High-Performance Laptop",
                    Price = 1199.99M,
                    CategoryId = 1,
                    ImageUrl = "Images\\macbook.webp",
                    Description = "The MacBook Pro M2 features Apple's powerful M2 chip, offering top-notch performance for professional and creative work. Its 13-inch Retina display and long battery life make it the perfect companion for productivity on the go."
                },
                new Product
                {
                    Id = 3,
                    Name = "Classic White T-Shirt - Casual Comfort",
                    Price = 9.99M,
                    CategoryId = 2,
                    ImageUrl = "Images\\tsh.webp",
                    Description = "A timeless essential for any wardrobe, this Classic White T-Shirt is made from soft, breathable cotton and provides all-day comfort. Whether you're lounging at home or going out with friends, it's a must-have for casual looks."
                },
                new Product
                {
                    Id = 4,
                    Name = "The Great Gatsby by F. Scott Fitzgerald - Classic Novel",
                    Price = 9.99M,
                    CategoryId = 3,
                    ImageUrl = "Images\\novell.webp",
                    Description = "The Great Gatsby is a captivating story of love, wealth, and tragedy set in the Roaring Twenties. F. Scott Fitzgerald's classic novel explores the American Dream and its complexities through the eyes of Nick Carraway and the enigmatic Jay Gatsby."
                },
                new Product
                {
                    Id = 5,
                    Name = "1984 by George Orwell - Dystopian Masterpiece",
                    Price = 9.99M,
                    CategoryId = 3,
                    ImageUrl = "Images\\1984.jpg",
                    Description = "1984 by George Orwell is a gripping, thought-provoking dystopian novel that critiques totalitarianism and explores themes of surveillance, freedom, and individuality. Set in a society under constant surveillance, the story follows Winston Smith as he rebels against the oppressive regime."
                }
            );


            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId);
        }
    }
}
