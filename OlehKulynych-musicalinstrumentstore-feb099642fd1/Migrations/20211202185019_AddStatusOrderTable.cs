using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_Instrument_Store.Migrations
{
    public partial class AddStatusOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statusOrder",
                table: "orders");

            migrationBuilder.AddColumn<int>(
                name: "statusOrderId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusOrders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orders_statusOrderId",
                table: "orders",
                column: "statusOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_StatusOrders_statusOrderId",
                table: "orders",
                column: "statusOrderId",
                principalTable: "StatusOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_StatusOrders_statusOrderId",
                table: "orders");

            migrationBuilder.DropTable(
                name: "StatusOrders");

            migrationBuilder.DropIndex(
                name: "IX_orders_statusOrderId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "statusOrderId",
                table: "orders");

            migrationBuilder.AddColumn<string>(
                name: "statusOrder",
                table: "orders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
