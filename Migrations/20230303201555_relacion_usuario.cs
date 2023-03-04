using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class relacion_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Responsability",
                table: "Users",
                newName: "ResponsabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ResponsabilityId",
                table: "Users",
                column: "ResponsabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Responsabilities_ResponsabilityId",
                table: "Users",
                column: "ResponsabilityId",
                principalTable: "Responsabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Responsabilities_ResponsabilityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ResponsabilityId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "ResponsabilityId",
                table: "Users",
                newName: "Responsability");
        }
    }
}
