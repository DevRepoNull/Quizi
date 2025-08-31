using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questioning_Data_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDateAndTimeOnFormTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndFormDate",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "StartFormDate",
                table: "Form");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndExamDate",
                table: "Form",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EndExamTime",
                table: "Form",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartExamDate",
                table: "Form",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "StartExamTime",
                table: "Form",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndExamDate",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "EndExamTime",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "StartExamDate",
                table: "Form");

            migrationBuilder.DropColumn(
                name: "StartExamTime",
                table: "Form");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndFormDate",
                table: "Form",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartFormDate",
                table: "Form",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
