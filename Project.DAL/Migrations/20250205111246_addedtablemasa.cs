using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedtablemasa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableCategoryNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableCategoryNumbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableCategoryPlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableCategoryPlaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    TableCategoryNumberId = table.Column<int>(type: "int", nullable: false),
                    TableCategoryPlaceId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Masas_TableCategoryNumbers_TableCategoryNumberId",
                        column: x => x.TableCategoryNumberId,
                        principalTable: "TableCategoryNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Masas_TableCategoryPlaces_TableCategoryPlaceId",
                        column: x => x.TableCategoryPlaceId,
                        principalTable: "TableCategoryPlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9175607d-294d-44d8-9c32-42b17428fb65", "AQAAAAIAAYagAAAAEI1OwCmmLB4MYkg30orMo1pXwhzbiHXMIBROblPw8chM5iH02f8xIqVhaWHOMUMe0A==", "89b6abaf-93ad-4219-82b4-2f709870b948" });

            migrationBuilder.CreateIndex(
                name: "IX_Masas_TableCategoryNumberId",
                table: "Masas",
                column: "TableCategoryNumberId");

            migrationBuilder.CreateIndex(
                name: "IX_Masas_TableCategoryPlaceId",
                table: "Masas",
                column: "TableCategoryPlaceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Masas");

            migrationBuilder.DropTable(
                name: "TableCategoryNumbers");

            migrationBuilder.DropTable(
                name: "TableCategoryPlaces");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9ce0b397-1b3c-4c50-9481-8d118dd37972", "AQAAAAIAAYagAAAAEAjUQ9yVuJ0Q5HVLyXNMbZcx8RV02FrQamkRShI1LP5/pBSEvU9wsurKvlRCcptj9Q==", "6687a142-4082-4e28-9bda-0cb0440965db" });
        }
    }
}
