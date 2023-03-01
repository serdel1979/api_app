using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class usersresponsabilitiesfotosrelaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagementConstruction",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "Id_Leader",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsabilityId",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ResponsabilityId",
                table: "Projects",
                column: "ResponsabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Responsabilities_ResponsabilityId",
                table: "Projects",
                column: "ResponsabilityId",
                principalTable: "Responsabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Responsabilities_ResponsabilityId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ResponsabilityId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Id_Leader",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ResponsabilityId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "ManagementConstruction",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
