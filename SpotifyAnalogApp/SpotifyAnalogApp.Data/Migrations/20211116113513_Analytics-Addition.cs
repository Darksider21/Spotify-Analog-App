using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyAnalogApp.Data.Migrations
{
    public partial class AnalyticsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalyticsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Analytics",
                columns: table => new
                {
                    AnalyticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSongsCount = table.Column<int>(type: "int", nullable: false),
                    MetalSongsCount = table.Column<int>(type: "int", nullable: false),
                    JPopSongsCount = table.Column<int>(type: "int", nullable: false),
                    RockSongsCount = table.Column<int>(type: "int", nullable: false),
                    ClassicalSongsCount = table.Column<int>(type: "int", nullable: false),
                    ElectronicSongsCount = table.Column<int>(type: "int", nullable: false),
                    PopSongsCount = table.Column<int>(type: "int", nullable: false),
                    JazzSongsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.AnalyticsId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AnalyticsId",
                table: "Users",
                column: "AnalyticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Analytics_AnalyticsId",
                table: "Users",
                column: "AnalyticsId",
                principalTable: "Analytics",
                principalColumn: "AnalyticsId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Analytics_AnalyticsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Analytics");

            migrationBuilder.DropIndex(
                name: "IX_Users_AnalyticsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AnalyticsId",
                table: "Users");
        }
    }
}
