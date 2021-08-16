using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class configurate_eventfavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFavorites_Events_EventsId",
                table: "EventFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_EventFavorites_Favorites_FavoritesId",
                table: "EventFavorites");

            migrationBuilder.RenameColumn(
                name: "FavoritesId",
                table: "EventFavorites",
                newName: "EventId");

            migrationBuilder.RenameColumn(
                name: "EventsId",
                table: "EventFavorites",
                newName: "FavoriteId");

            migrationBuilder.RenameIndex(
                name: "IX_EventFavorites_FavoritesId",
                table: "EventFavorites",
                newName: "IX_EventFavorites_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFavorites_Events_EventId",
                table: "EventFavorites",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventFavorites_Favorites_FavoriteId",
                table: "EventFavorites",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventFavorites_Events_EventId",
                table: "EventFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_EventFavorites_Favorites_FavoriteId",
                table: "EventFavorites");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "EventFavorites",
                newName: "FavoritesId");

            migrationBuilder.RenameColumn(
                name: "FavoriteId",
                table: "EventFavorites",
                newName: "EventsId");

            migrationBuilder.RenameIndex(
                name: "IX_EventFavorites_EventId",
                table: "EventFavorites",
                newName: "IX_EventFavorites_FavoritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventFavorites_Events_EventsId",
                table: "EventFavorites",
                column: "EventsId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventFavorites_Favorites_FavoritesId",
                table: "EventFavorites",
                column: "FavoritesId",
                principalTable: "Favorites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}