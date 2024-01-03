using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeRecordHolderIdonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_RecordHolderId",
                table: "Exercises");

            migrationBuilder.AlterColumn<string>(
                name: "RecordHolderId",
                table: "Exercises",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_RecordHolderId",
                table: "Exercises",
                column: "RecordHolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_AspNetUsers_RecordHolderId",
                table: "Exercises");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "RecordHolderId",
                keyValue: null,
                column: "RecordHolderId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "RecordHolderId",
                table: "Exercises",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_AspNetUsers_RecordHolderId",
                table: "Exercises",
                column: "RecordHolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
