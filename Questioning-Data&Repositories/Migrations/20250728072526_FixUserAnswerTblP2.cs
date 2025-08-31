using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questioning_Data_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class FixUserAnswerTblP2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SelectedTrueOrFalse",
                table: "UserAnswer",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "SelectedTrueOrFalse",
                table: "UserAnswer",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
