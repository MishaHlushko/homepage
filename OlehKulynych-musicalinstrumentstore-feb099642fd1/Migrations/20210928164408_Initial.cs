using Microsoft.EntityFrameworkCore.Migrations;

namespace Musical_Instrument_Store.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mICategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mICategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MusicalInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameMI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descMI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    available = table.Column<bool>(type: "bit", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false),
                    MICategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicalInstruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicalInstruments_mICategories_MICategoryId",
                        column: x => x.MICategoryId,
                        principalTable: "mICategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicalInstruments_MICategoryId",
                table: "MusicalInstruments",
                column: "MICategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicalInstruments");

            migrationBuilder.DropTable(
                name: "mICategories");
        }
    }
}
