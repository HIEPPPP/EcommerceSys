using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataforcategoryproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "product_category",
                columns: new[] { "id", "desc", "name" },
                values: new object[,]
                {
                    { 1, "Phụ kiện unisex", "Accessories" },
                    { 2, "", "Fitness" },
                    { 3, "Quần áo unisex", "Clothing" },
                    { 4, "Thiết bị điện tử", "Electronics" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product_category",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "product_category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "product_category",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "product_category",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
