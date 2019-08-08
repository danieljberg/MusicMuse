using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class updateEventModelWithDateAndInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventInfo",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Posted",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventInfo",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Posted",
                table: "Event");
        }
    }
}
