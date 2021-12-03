using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAnalogApp.Data.Migrations
{
    public partial class Dislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DislikedSongs",
                columns: table => new
                {
                    DislikedSongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongId = table.Column<int>(type: "int", nullable: true),
                    AppUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DislikedSongs", x => x.DislikedSongId);
                    table.ForeignKey(
                        name: "FK_DislikedSongs_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DislikedSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "SongId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DislikedSongs_AppUserId",
                table: "DislikedSongs",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedSongs_SongId",
                table: "DislikedSongs",
                column: "SongId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DislikedSongs");
        }
    }
}
