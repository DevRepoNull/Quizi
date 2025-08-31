using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questioning_Data_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAnswerTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_FkUserId_FkAnswerId_FkQuestionId_UAnswerId",
                table: "UserAnswer");

            migrationBuilder.AlterColumn<string>(
                name: "FkAnswerId",
                table: "UserAnswer",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "UserAnswer",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<string>(
                name: "DescriptiveAnswerText",
                table: "UserAnswer",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FkFormId",
                table: "UserAnswer",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedOption",
                table: "UserAnswer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SelectedTrueOrFalse",
                table: "UserAnswer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "UserAnswer",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkFormId",
                table: "UserAnswer",
                column: "FkFormId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkUserId_FkQuestionId",
                table: "UserAnswer",
                columns: new[] { "FkUserId", "FkQuestionId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Form_FkFormId",
                table: "UserAnswer",
                column: "FkFormId",
                principalTable: "Form",
                principalColumn: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Form_FkFormId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_FkFormId",
                table: "UserAnswer");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_FkUserId_FkQuestionId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "DescriptiveAnswerText",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "FkFormId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "SelectedOption",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "SelectedTrueOrFalse",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "UserAnswer");

            migrationBuilder.AlterColumn<string>(
                name: "FkAnswerId",
                table: "UserAnswer",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkUserId_FkAnswerId_FkQuestionId_UAnswerId",
                table: "UserAnswer",
                columns: new[] { "FkUserId", "FkAnswerId", "FkQuestionId", "UAnswerId" },
                unique: true);
        }
    }
}
