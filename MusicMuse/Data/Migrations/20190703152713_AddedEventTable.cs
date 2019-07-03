using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class AddedEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Influence1",
                table: "Musician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence2",
                table: "Musician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence3",
                table: "Musician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence1",
                table: "Band",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence2",
                table: "Band",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence3",
                table: "Band",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Influence1",
                table: "Musician");

            migrationBuilder.DropColumn(
                name: "Influence2",
                table: "Musician");

            migrationBuilder.DropColumn(
                name: "Influence3",
                table: "Musician");

            migrationBuilder.DropColumn(
                name: "Influence1",
                table: "Band");

            migrationBuilder.DropColumn(
                name: "Influence2",
                table: "Band");

            migrationBuilder.DropColumn(
                name: "Influence3",
                table: "Band");
        }
    }
}
