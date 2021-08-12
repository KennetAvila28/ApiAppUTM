using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class CuponesStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "CuponesGenericos",
                nullable: true,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "CuponesImagen",
                nullable: true,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "CuponesGenericos");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "CuponesImagen");
        }
    }
}
