using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class report_detail_cambio_relacion_observations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Report_detail_Report_detailId",
                table: "Observations");

            migrationBuilder.RenameColumn(
                name: "Report_detailId",
                table: "Observations",
                newName: "ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_Report_detailId",
                table: "Observations",
                newName: "IX_Observations_ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Observations",
                newName: "Report_detailId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_ReportId",
                table: "Observations",
                newName: "IX_Observations_Report_detailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Report_detail_Report_detailId",
                table: "Observations",
                column: "Report_detailId",
                principalTable: "Report_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
