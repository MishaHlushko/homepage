using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_Instrument_Store.Migrations
{
    public partial class AddUserInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "orderDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "orderDetails");
        }
    }
}
