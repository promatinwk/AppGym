using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdTrainingSessionDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrainingSessionDetailsId",
                table: "TrainingExercises",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TrainingSessionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    SessionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TrainingName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessionDetails_TraningSessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "TraningSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WeightRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrainingSessionId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    SeriesCount = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightRecords_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WeightRecords_TraningSessions_TrainingSessionId",
                        column: x => x.TrainingSessionId,
                        principalTable: "TraningSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_TrainingSessionDetailsId",
                table: "TrainingExercises",
                column: "TrainingSessionDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessionDetails_SessionId",
                table: "TrainingSessionDetails",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightRecords_ExerciseId",
                table: "WeightRecords",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightRecords_TrainingSessionId",
                table: "WeightRecords",
                column: "TrainingSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercises_TrainingSessionDetails_TrainingSessionDeta~",
                table: "TrainingExercises",
                column: "TrainingSessionDetailsId",
                principalTable: "TrainingSessionDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercises_TrainingSessionDetails_TrainingSessionDeta~",
                table: "TrainingExercises");

            migrationBuilder.DropTable(
                name: "TrainingSessionDetails");

            migrationBuilder.DropTable(
                name: "WeightRecords");

            migrationBuilder.DropIndex(
                name: "IX_TrainingExercises_TrainingSessionDetailsId",
                table: "TrainingExercises");

            migrationBuilder.DropColumn(
                name: "TrainingSessionDetailsId",
                table: "TrainingExercises");
        }
    }
}
