using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Questioning_Data_Repositories.Migrations
{
    /// <inheritdoc />
    public partial class initializeQuestionExamDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RoleActive = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UserProfile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FkRoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Role_FkRoleId",
                        column: x => x.FkRoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    FormId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    FormTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    AccessCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartFormDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndFormDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    FkUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FKCategoryId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_Form_Categories_FKCategoryId",
                        column: x => x.FKCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK_Form_User_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    QuestionText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    FkUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_User_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnswerTypes = table.Column<byte>(type: "tinyint", nullable: false),
                    TestOptionOne = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TestOptionTwo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TestOptionThree = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TestOptionFour = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TestCorrectOption = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TruOrFalseAnswer = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AnswerText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FkQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answer_Question_FkQuestionId",
                        column: x => x.FkQuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "RFormQuestion",
                columns: table => new
                {
                    FormQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    FkFormId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FkQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFormQuestion", x => x.FormQuestionId);
                    table.ForeignKey(
                        name: "FK_RFormQuestion_Form_FkFormId",
                        column: x => x.FkFormId,
                        principalTable: "Form",
                        principalColumn: "FormId");
                    table.ForeignKey(
                        name: "FK_RFormQuestion_Question_FkQuestionId",
                        column: x => x.FkQuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.CreateTable(
                name: "UserAnswer",
                columns: table => new
                {
                    UAnswerId = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValueSql: "NEWID()"),
                    FkUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FkQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FkAnswerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AnswerDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswer", x => x.UAnswerId);
                    table.ForeignKey(
                        name: "FK_UserAnswer_Answer_FkAnswerId",
                        column: x => x.FkAnswerId,
                        principalTable: "Answer",
                        principalColumn: "AnswerId");
                    table.ForeignKey(
                        name: "FK_UserAnswer_Question_FkQuestionId",
                        column: x => x.FkQuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId");
                    table.ForeignKey(
                        name: "FK_UserAnswer_User_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnswerId",
                table: "Answer",
                column: "AnswerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AnswerText_TestCorrectOption_TruOrFalseAnswer",
                table: "Answer",
                columns: new[] { "AnswerText", "TestCorrectOption", "TruOrFalseAnswer" });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_FkQuestionId",
                table: "Answer",
                column: "FkQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Form_FKCategoryId",
                table: "Form",
                column: "FKCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_FkUserId",
                table: "Form",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Form_FormId",
                table: "Form",
                column: "FormId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Form_FormTitle_Description_IsActive",
                table: "Form",
                columns: new[] { "FormTitle", "Description", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Question_FkUserId",
                table: "Question",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionId",
                table: "Question",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionText",
                table: "Question",
                column: "QuestionText");

            migrationBuilder.CreateIndex(
                name: "IX_RFormQuestion_FkFormId_FkQuestionId_FormQuestionId",
                table: "RFormQuestion",
                columns: new[] { "FkFormId", "FkQuestionId", "FormQuestionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RFormQuestion_FkQuestionId",
                table: "RFormQuestion",
                column: "FkQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleActive",
                table: "Role",
                column: "RoleActive");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleId_RoleName",
                table: "Role",
                columns: new[] { "RoleId", "RoleName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_FirstName_LastName_PhoneNumber",
                table: "User",
                columns: new[] { "FirstName", "LastName", "PhoneNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_User_FkRoleId",
                table: "User",
                column: "FkRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserId_Email_UserName",
                table: "User",
                columns: new[] { "UserId", "Email", "UserName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkAnswerId",
                table: "UserAnswer",
                column: "FkAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkQuestionId",
                table: "UserAnswer",
                column: "FkQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_FkUserId_FkAnswerId_FkQuestionId_UAnswerId",
                table: "UserAnswer",
                columns: new[] { "FkUserId", "FkAnswerId", "FkQuestionId", "UAnswerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RFormQuestion");

            migrationBuilder.DropTable(
                name: "UserAnswer");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
