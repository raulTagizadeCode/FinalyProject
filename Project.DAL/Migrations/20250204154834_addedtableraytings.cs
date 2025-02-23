using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedtableraytings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_AppUserId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Raytings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raytings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Raytings_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ce0b397-1b3c-4c50-9481-8d118dd37972", "AQAAAAIAAYagAAAAEAjUQ9yVuJ0Q5HVLyXNMbZcx8RV02FrQamkRShI1LP5/pBSEvU9wsurKvlRCcptj9Q==", "6687a142-4082-4e28-9bda-0cb0440965db" });

            migrationBuilder.CreateIndex(
                name: "IX_Raytings_AppUserId",
                table: "Raytings",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raytings");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "JobApplications");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "JobApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8835d403-20d3-48a2-8b41-0a27b68e929c", "AQAAAAIAAYagAAAAEC+INbPm1Gq0xkiafX0wEKjeMf1SCQfVBeg2in0kBL82rlbcwxdvuE3X2IHuqFyVeQ==", "5f25b9a0-ea55-4660-86cb-5888567f547b" });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_AppUserId",
                table: "JobApplications",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_AspNetUsers_AppUserId",
                table: "JobApplications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
