using Microsoft.EntityFrameworkCore.Migrations;

namespace AppUTM.Data.Migrations
{
    public partial class remove_EventFavorite_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsFavorites");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Favorites",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Events_EventId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_EventId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Favorites");

            migrationBuilder.CreateTable(
                name: "EventsFavorites",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false),
                    FavoriteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsFavorites", x => new { x.EventId, x.FavoriteId });
                    table.ForeignKey(
                        name: "FK_EventsFavorites_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventsFavorites_Favorites_FavoriteId",
                        column: x => x.FavoriteId,
                        principalTable: "Favorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsFavorites_FavoriteId",
                table: "EventsFavorites",
                column: "FavoriteId");
        }
    }
}
