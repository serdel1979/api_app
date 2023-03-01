using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class entit_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id_Responsibility",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Leader",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id_Responsibility",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Leader",
                table: "AspNetUsers");
        }
    }
}
