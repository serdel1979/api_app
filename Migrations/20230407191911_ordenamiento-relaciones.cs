using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class ordenamientorelaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_next_day_Report_ReportId",
                table: "Activities_next_day");

            migrationBuilder.DropForeignKey(
                name: "FK_Developed_activities_Report_ReportId",
                table: "Developed_activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Needs_next_day_Report_ReportId",
                table: "Needs_next_day");

            migrationBuilder.DropForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations");

            migrationBuilder.DropIndex(
                name: "IX_Observations_ReportId",
                table: "Observations");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Observations");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Needs_next_day",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Developed_activities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Activities_next_day",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_next_day_Report_ReportId",
                table: "Activities_next_day",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Developed_activities_Report_ReportId",
                table: "Developed_activities",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Needs_next_day_Report_ReportId",
                table: "Needs_next_day",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_next_day_Report_ReportId",
                table: "Activities_next_day");

            migrationBuilder.DropForeignKey(
                name: "FK_Developed_activities_Report_ReportId",
                table: "Developed_activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Needs_next_day_Report_ReportId",
                table: "Needs_next_day");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Observations",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Needs_next_day",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Developed_activities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Activities_next_day",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Observations_ReportId",
                table: "Observations",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_next_day_Report_ReportId",
                table: "Activities_next_day",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Developed_activities_Report_ReportId",
                table: "Developed_activities",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Needs_next_day_Report_ReportId",
                table: "Needs_next_day",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Observations_Report_ReportId",
                table: "Observations",
                column: "ReportId",
                principalTable: "Report",
                principalColumn: "Id");
        }
    }
}
