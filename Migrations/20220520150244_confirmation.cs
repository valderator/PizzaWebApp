using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaAPI.Migrations
{
    public partial class confirmation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationKey",
                table: "Users",
                type: "varchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isConfirmed",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationKey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "isConfirmed",
                table: "Users");
        }
    }
}
