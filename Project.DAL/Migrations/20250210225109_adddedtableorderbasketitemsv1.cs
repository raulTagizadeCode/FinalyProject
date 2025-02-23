using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adddedtableorderbasketitemsv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "BasketItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "BasketItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba90d736-f57e-4e73-b04a-4d2f4e299dd2", "AQAAAAIAAYagAAAAEATYKcfrNh+Nzc31DjepB6OHa/JrXLZb8lwiWhOfbC8t2AwrPJrWDORGe31Xe28f7w==", "59cea959-447e-4d79-a0ba-fb347acdc190" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "BasketItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "be30629f-0508-461a-8fa1-0e905705e1f5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d0d1e32-00c2-4bdc-884d-98462bdf2a01", "AQAAAAIAAYagAAAAEPVzRZGS6XVnl2MYRqt1OgeYcCRFZTj06DfN9UMkooaa+hN3uWgn5PSwQFotuIKXCA==", "7589f0cb-26b6-4625-8cfe-b26768a7af73" });

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Orders_OrderId",
                table: "BasketItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
