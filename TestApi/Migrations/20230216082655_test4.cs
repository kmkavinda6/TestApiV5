using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApi.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "totalAmount",
                table: "Order",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "storeID",
                table: "Order",
                newName: "StoreID");

            migrationBuilder.RenameColumn(
                name: "qty",
                table: "Order",
                newName: "Qty");

            migrationBuilder.RenameColumn(
                name: "itemID",
                table: "Order",
                newName: "ItemID");

            migrationBuilder.RenameColumn(
                name: "isDeleverd",
                table: "Order",
                newName: "IsDeleverd");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Order",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "orderID",
                table: "Order",
                newName: "OrderID");

            migrationBuilder.AddColumn<int>(
                name: "SalesRepID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesRepID",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Order",
                newName: "totalAmount");

            migrationBuilder.RenameColumn(
                name: "StoreID",
                table: "Order",
                newName: "storeID");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "Order",
                newName: "qty");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Order",
                newName: "itemID");

            migrationBuilder.RenameColumn(
                name: "IsDeleverd",
                table: "Order",
                newName: "isDeleverd");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Order",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Order",
                newName: "orderID");
        }
    }
}
