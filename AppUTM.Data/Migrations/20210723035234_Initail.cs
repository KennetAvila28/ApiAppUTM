using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuponesGenericos",
                columns: table => new
                {
                    CuponGeneridoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuponesUsados = table.Column<int>(type: "int", nullable: false),
                    CuponesVisitados = table.Column<int>(type: "int", nullable: false),
                    PorcentajeDescuento = table.Column<int>(type: "int", nullable: false),
                    NumeroPorPersona = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponesGenericos", x => x.CuponGeneridoId);
                });

            migrationBuilder.CreateTable(
                name: "CuponesImagen",
                columns: table => new
                {
                    CuponImagenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaExpiracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuponesUsados = table.Column<int>(type: "int", nullable: false),
                    CuponesVisitados = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuponesImagen", x => x.CuponImagenId);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagenEmpresa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.EmpresaId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuponesGenericos");

            migrationBuilder.DropTable(
                name: "CuponesImagen");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
