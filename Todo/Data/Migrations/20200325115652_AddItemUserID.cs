using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTodo.Data.Migrations
{
    public partial class AddItemUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Items");
        }
    }
}
