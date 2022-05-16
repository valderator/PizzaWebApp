using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaAPI.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Pizzas_PizzaId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_PizzaId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShoppingCarts",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "PizzaId",
                table: "ShoppingCarts",
                newName: "PizzaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "ShoppingCarts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "PizzaID",
                table: "ShoppingCarts",
                newName: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_PizzaId",
                table: "ShoppingCarts",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Pizzas_PizzaId",
                table: "ShoppingCarts",
                column: "PizzaId",
                principalTable: "Pizzas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Users_UserId",
                table: "ShoppingCarts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
