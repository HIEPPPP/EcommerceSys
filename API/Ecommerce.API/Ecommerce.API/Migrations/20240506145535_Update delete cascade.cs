using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    /// <inheritdoc />
    public partial class Updatedeletecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropForeignKey(
                name: "FK_order_details_payment_details",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_product_product_inventory",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_shopping_session_user",
                table: "shopping_session");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_item_product",
                table: "cart_item",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_payment_details",
                table: "order_details",
                column: "payment_id",
                principalTable: "payment_details",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_product_product_inventory",
                table: "product",
                column: "inventory_id",
                principalTable: "product_inventory",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shopping_session_user",
                table: "shopping_session",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cart_item_product",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "FK_order_details_payment_details",
                table: "order_details");

            migrationBuilder.DropForeignKey(
                name: "FK_product_product_inventory",
                table: "product");

            migrationBuilder.DropForeignKey(
                name: "FK_shopping_session_user",
                table: "shopping_session");

            migrationBuilder.AddForeignKey(
                name: "FK_cart_item_product",
                table: "cart_item",
                column: "product_id",
                principalTable: "product",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_details_payment_details",
                table: "order_details",
                column: "payment_id",
                principalTable: "payment_details",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_product_product_inventory",
                table: "product",
                column: "inventory_id",
                principalTable: "product_inventory",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_shopping_session_user",
                table: "shopping_session",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id");
        }
    }
}
