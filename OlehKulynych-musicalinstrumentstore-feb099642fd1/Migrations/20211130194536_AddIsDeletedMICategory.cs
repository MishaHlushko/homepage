using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_Instrument_Store.Migrations
{
    public partial class AddIsDeletedMICategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "mICategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "mICategories");
        }
    }
}
