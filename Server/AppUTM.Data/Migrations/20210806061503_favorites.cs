using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class favorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Events_EventId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_EventId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Favorites");

            migrationBuilder.CreateTable(
                name: "EventFavorites",
                columns: table => new
                {
                    EventsId = table.Column<int>(type: "int", nullable: false),
                    FavoritesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventFavorites", x => new { x.EventsId, x.FavoritesId });
                    table.ForeignKey(
                        name: "FK_EventFavorites_Events_EventsId",
                        column: x => x.EventsId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventFavorites_Favorites_FavoritesId",
                        column: x => x.FavoritesId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventFavorites_FavoritesId",
                table: "EventFavorites",
                column: "FavoritesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventFavorites");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Favorites",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Favorites",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Favorites",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Favorites",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Favorites",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_EventId",
                table: "Favorites",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Events_EventId",
                table: "Favorites",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
