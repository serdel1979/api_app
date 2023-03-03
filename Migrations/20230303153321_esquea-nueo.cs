using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_app.Migrations
{
    public partial class esqueanueo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_Job",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Id_Responsibility",
                table: "Projects",
                newName: "LeaderId");

            migrationBuilder.RenameColumn(
                name: "Id_Leader",
                table: "Projects",
                newName: "JobId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_JobId",
                table: "Projects",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LeaderId",
                table: "Projects",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Jobs_JobId",
                table: "Projects",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Users_LeaderId",
                table: "Projects",
                column: "LeaderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Jobs_JobId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Users_LeaderId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_JobId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_LeaderId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "LeaderId",
                table: "Projects",
                newName: "Id_Responsibility");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Projects",
                newName: "Id_Leader");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateStart",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateEnd",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id_Job",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
