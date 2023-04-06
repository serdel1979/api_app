using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class relacion_activity_assigned_corregido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigned_Activities_AspNetUsers_userId",
                table: "Assigned_Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigned_Activities",
                table: "Assigned_Activities");

            migrationBuilder.DropIndex(
                name: "IX_Assigned_Activities_userId",
                table: "Assigned_Activities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Assigned_Activities");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Assigned_Activities",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Assigned_Activities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigned_Activities",
                table: "Assigned_Activities",
                columns: new[] { "UserId", "Developed_ActivityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Assigned_Activities_AspNetUsers_UserId",
                table: "Assigned_Activities",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assigned_Activities_AspNetUsers_UserId",
                table: "Assigned_Activities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assigned_Activities",
                table: "Assigned_Activities");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assigned_Activities",
                newName: "userId");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Assigned_Activities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Assigned_Activities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assigned_Activities",
                table: "Assigned_Activities",
                columns: new[] { "UserId", "Developed_ActivityId" });

            migrationBuilder.CreateIndex(
                name: "IX_Assigned_Activities_userId",
                table: "Assigned_Activities",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assigned_Activities_AspNetUsers_userId",
                table: "Assigned_Activities",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
