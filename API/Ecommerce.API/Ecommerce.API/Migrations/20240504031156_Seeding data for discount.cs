using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "discount",
                columns: new[] { "id", "active", "desc", "discount_percent", "name" },
                values: new object[,]
                {
                    { 1, false, "Chương trình giảm giá", 20m, "Giảm 20%" },
                    { 2, false, "Chương trình giảm giá các ngày lễ trong năm", 25m, "Giảm 25%" },
                    { 3, false, "Hàng tồn kho", 50m, "Giảm 50%" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "discount",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
