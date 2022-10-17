using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_project.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    category_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.category_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question_type",
                columns: table => new
                {
                    question_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    question_type_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_type", x => x.question_type_id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "survey",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    survey_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    tittle = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    isPopular = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_survey", x => x.survey_id);
                    table.ForeignKey(
                        name: "FK_survey_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    survey_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    question_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    question_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => x.question_id);
                    table.ForeignKey(
                        name: "FK_question_question_type_question_type_id",
                        column: x => x.question_type_id,
                        principalTable: "question_type",
                        principalColumn: "question_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_question_survey_survey_id",
                        column: x => x.survey_id,
                        principalTable: "survey",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "image_url_detail",
                columns: table => new
                {
                    image_url_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    base_url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    survey_id = table.Column<int>(type: "int", nullable: false),
                    question_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image_url_detail", x => x.image_url_id);
                    table.ForeignKey(
                        name: "FK_image_url_detail_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_image_url_detail_question_type_question_type_id",
                        column: x => x.question_type_id,
                        principalTable: "question_type",
                        principalColumn: "question_type_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_image_url_detail_survey_survey_id",
                        column: x => x.survey_id,
                        principalTable: "survey",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    question_type_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    option_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.option_id);
                    table.ForeignKey(
                        name: "FK_options_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_options_question_type_question_type_id",
                        column: x => x.question_type_id,
                        principalTable: "question_type",
                        principalColumn: "question_type_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_answer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    option_id = table.Column<int>(type: "int", nullable: false),
                    create_date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_answer", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_answer_options_option_id",
                        column: x => x.option_id,
                        principalTable: "options",
                        principalColumn: "option_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_answer_question_question_id",
                        column: x => x.question_id,
                        principalTable: "question",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_image_url_detail_question_id",
                table: "image_url_detail",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_url_detail_question_type_id",
                table: "image_url_detail",
                column: "question_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_url_detail_survey_id",
                table: "image_url_detail",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_options_question_id",
                table: "options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_options_question_type_id",
                table: "options",
                column: "question_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_question_type_id",
                table: "question",
                column: "question_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_survey_id",
                table: "question",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_survey_category_id",
                table: "survey",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_option_id",
                table: "user_answer",
                column: "option_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_answer_question_id",
                table: "user_answer",
                column: "question_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "image_url_detail");

            migrationBuilder.DropTable(
                name: "user_answer");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "question");

            migrationBuilder.DropTable(
                name: "question_type");

            migrationBuilder.DropTable(
                name: "survey");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
