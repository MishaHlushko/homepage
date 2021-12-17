using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_Instrument_Store.Migrations
{
    public partial class UpdateMusicalInstrument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicalInstruments_mICategories_MICategoryId",
                table: "MusicalInstruments");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "MusicalInstruments");

            migrationBuilder.AlterColumn<int>(
                name: "MICategoryId",
                table: "MusicalInstruments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicalInstruments_mICategories_MICategoryId",
                table: "MusicalInstruments",
                column: "MICategoryId",
                principalTable: "mICategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MusicalInstruments_mICategories_MICategoryId",
                table: "MusicalInstruments");

            migrationBuilder.AlterColumn<int>(
                name: "MICategoryId",
                table: "MusicalInstruments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "categoryId",
                table: "MusicalInstruments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MusicalInstruments_mICategories_MICategoryId",
                table: "MusicalInstruments",
                column: "MICategoryId",
                principalTable: "mICategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
