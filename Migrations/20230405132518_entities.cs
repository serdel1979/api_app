using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observation_Report_detail_Report_detailId",
                table: "Observation");

            migrationBuilder.DropForeignKey(
                name: "FK_Observation_Report_ReportId",
                table: "Observation");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Observation_ObservationId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_detail_Report_ReporteId",
                table: "Report_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Observation",
                table: "Observation");

            migrationBuilder.RenameTable(
                name: "Observation",
                newName: "Observations");

            migrationBuilder.RenameColumn(
                name: "ReporteId",
                table: "Report_detail",
                newName: "ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_detail_ReporteId",
                table: "Report_detail",
                newName: "IX_Report_detail_ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Observation_ReportId",
                table: "Observations",
                newName: "IX_Observations_ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Observation_Report_detailId",
                table: "Observations",
                newName: "IX_Observations_Report_detailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Observations",
                table: "Observations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Report_detail_Report_detailId",
                table: "Observations",
                column: "Report_detailId",
                principalTable: "Report_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Observations_ObservationId",
                table: "Photos",
                column: "ObservationId",
                principalTable: "Observations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_detail_Report_ReportId",
                table: "Report_detail",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Report_detail_Report_detailId",
                table: "Observations");

            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Observations_ObservationId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_detail_Report_ReportId",
                table: "Report_detail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Observations",
                table: "Observations");

            migrationBuilder.RenameTable(
                name: "Observations",
                newName: "Observation");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "Report_detail",
                newName: "ReporteId");

            migrationBuilder.RenameIndex(
                name: "IX_Report_detail_ReportId",
                table: "Report_detail",
                newName: "IX_Report_detail_ReporteId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_ReportId",
                table: "Observation",
                newName: "IX_Observation_ReportId");

            migrationBuilder.RenameIndex(
                name: "IX_Observations_Report_detailId",
                table: "Observation",
                newName: "IX_Observation_Report_detailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Observation",
                table: "Observation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_Report_detail_Report_detailId",
                table: "Observation",
                column: "Report_detailId",
                principalTable: "Report_detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Observation_Report_ReportId",
                table: "Observation",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Observation_ObservationId",
                table: "Photos",
                column: "ObservationId",
                principalTable: "Observation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_detail_Report_ReporteId",
                table: "Report_detail",
                column: "ReporteId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
