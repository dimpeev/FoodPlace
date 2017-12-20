namespace FoodPlace.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedMenuProductRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Products_MenuId",
                table: "MenuProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Menus_ProductId",
                table: "MenuProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_Menus_MenuId",
                table: "MenuProducts",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_Products_ProductId",
                table: "MenuProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Menus_MenuId",
                table: "MenuProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuProducts_Products_ProductId",
                table: "MenuProducts");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_Products_MenuId",
                table: "MenuProducts",
                column: "MenuId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuProducts_Menus_ProductId",
                table: "MenuProducts",
                column: "ProductId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
