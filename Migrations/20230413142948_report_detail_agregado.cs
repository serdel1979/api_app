using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class report_detail_agregado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_detail_AspNetUsers_UserId",
                table: "Report_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_detail_Reports_ReportId",
                table: "Report_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report_detail",
                table: "Report_detail");

            migrationBuilder.RenameTable(
                name: "Report_detail",
                newName: "Reports_detail");

            migrationBuilder.RenameIndex(
                name: "IX_Report_detail_UserId",
                table: "Reports_detail",
                newName: "IX_Reports_detail_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_detail_ReportId",
                table: "Reports_detail",
                newName: "IX_Reports_detail_ReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports_detail",
                table: "Reports_detail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_detail_AspNetUsers_UserId",
                table: "Reports_detail",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_detail_Reports_ReportId",
                table: "Reports_detail",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reports_detail_AspNetUsers_UserId",
                table: "Reports_detail");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_detail_Reports_ReportId",
                table: "Reports_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports_detail",
                table: "Reports_detail");

            migrationBuilder.RenameTable(
                name: "Reports_detail",
                newName: "Report_detail");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_detail_UserId",
                table: "Report_detail",
                newName: "IX_Report_detail_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_detail_ReportId",
                table: "Report_detail",
                newName: "IX_Report_detail_ReportId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report_detail",
                table: "Report_detail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_detail_AspNetUsers_UserId",
                table: "Report_detail",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_detail_Reports_ReportId",
                table: "Report_detail",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
