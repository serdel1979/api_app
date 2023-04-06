using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class relacion_activity_assigned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assigned_Activities",
                columns: table => new
                {
                    Developed_ActivityId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    userId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigned_Activities", x => new { x.UserId, x.Developed_ActivityId });
                    table.ForeignKey(
                        name: "FK_Assigned_Activities_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Assigned_Activities_Developed_activities_Developed_Activity~",
                        column: x => x.Developed_ActivityId,
                        principalTable: "Developed_activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assigned_Activities_Developed_ActivityId",
                table: "Assigned_Activities",
                column: "Developed_ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigned_Activities_userId",
                table: "Assigned_Activities",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assigned_Activities");
        }
    }
}
