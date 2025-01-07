using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CloupardTask.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Gadgets and devices", "Electronics" },
                    { 2, "Men's and Women's Wear", "Clothing" },
                    { 3, "Fiction and Non-Fiction", "Books" },
                    { 4, "A selection of packaged items grouped together for convenience and value. Perfect for customers looking for multiple products at once.", "BUNDLES" },
                    { 5, "A variety of fresh, dried, and aromatic herbs that enhance the flavor of dishes and offer natural remedies.", "HERBS" },
                    { 6, "A wide assortment of fresh, seasonal, and nutrient-rich vegetables, including leafy greens, root vegetables, and more.", "VEGETABLES" },
                    { 7, "Essential products for everyday use, ranging from gardening tools to home and office supplies.", "SUPPLIES" },
                    { 8, "A colorful collection of fresh-cut flowers for any occasion, designed to bring beauty and fragrance into your home or special event.", "FLOWERS" },
                    { 9, "Fresh, juicy, and seasonal fruits picked to provide the best taste and nutrition, perfect for snacking or adding to meals.", "FRUITS" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "The iPhone 16 Pro Max offers cutting-edge performance, stunning visuals, and an exceptional camera experience. With its 6.7-inch OLED display, this device is perfect for gaming, content creation, and all your daily tasks.", "Images\\phone.webp", "iPhone 16 Pro Max - Premium Smartphone", 1500.99m },
                    { 2, 1, "The MacBook Pro M2 features Apple's powerful M2 chip, offering top-notch performance for professional and creative work. Its 13-inch Retina display and long battery life make it the perfect companion for productivity on the go.", "Images\\macbook.webp", "MacBook Pro M2 - High-Performance Laptop", 1199.99m },
                    { 3, 2, "A timeless essential for any wardrobe, this Classic White T-Shirt is made from soft, breathable cotton and provides all-day comfort. Whether you're lounging at home or going out with friends, it's a must-have for casual looks.", "Images\\tsh.webp", "Classic White T-Shirt - Casual Comfort", 9.99m },
                    { 4, 3, "The Great Gatsby is a captivating story of love, wealth, and tragedy set in the Roaring Twenties. F. Scott Fitzgerald's classic novel explores the American Dream and its complexities through the eyes of Nick Carraway and the enigmatic Jay Gatsby.", "Images\\novell.webp", "The Great Gatsby by F. Scott Fitzgerald - Classic Novel", 9.99m },
                    { 5, 3, "1984 by George Orwell is a gripping, thought-provoking dystopian novel that critiques totalitarianism and explores themes of surveillance, freedom, and individuality. Set in a society under constant surveillance, the story follows Winston Smith as he rebels against the oppressive regime.", "Images\\1984.jpg", "1984 by George Orwell - Dystopian Masterpiece", 9.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
