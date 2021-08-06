using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class Create_Table_NavigationMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPassed",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "NavegationMenu",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ControllerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavegationMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavegationMenu_NavegationMenu_ParentMenuId",
                        column: x => x.ParentMenuId,
                        principalTable: "NavegationMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenuPermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    NavigationMenuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuPermission", x => new { x.RoleId, x.NavigationMenuId });
                    table.ForeignKey(
                        name: "FK_RoleMenuPermission_NavegationMenu_NavigationMenuId",
                        column: x => x.NavigationMenuId,
                        principalTable: "NavegationMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavegationMenu_ParentMenuId",
                table: "NavegationMenu",
                column: "ParentMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenuPermission_NavigationMenuId",
                table: "RoleMenuPermission",
                column: "NavigationMenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMenuPermission");

            migrationBuilder.DropTable(
                name: "NavegationMenu");

            migrationBuilder.DropColumn(
                name: "IsPassed",
                table: "Events");
        }
    }
}
