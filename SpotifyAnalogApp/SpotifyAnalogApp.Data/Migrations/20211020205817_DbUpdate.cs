using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAnalogApp.Data.Migrations
{
    public partial class DbUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_GenreId",
                table: "Songs",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Genres_GenreId",
                table: "Songs",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Genres_GenreId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_GenreId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Songs");
        }
    }
}
