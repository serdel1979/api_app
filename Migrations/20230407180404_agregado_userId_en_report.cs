using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class agregado_userId_en_report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Report",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Report_UserId",
                table: "Report",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_UserId",
                table: "Report",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_UserId",
                table: "Report");

            migrationBuilder.DropIndex(
                name: "IX_Report_UserId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Report");
        }
    }
}
