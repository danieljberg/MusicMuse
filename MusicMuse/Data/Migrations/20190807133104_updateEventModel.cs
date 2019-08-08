using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class updateEventModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventLocation",
                table: "Event");

            migrationBuilder.AlterColumn<int>(
                name: "BusinessId",
                table: "Event",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_BusinessId",
                table: "Event",
                column: "BusinessId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Business_BusinessId",
                table: "Event",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Business_BusinessId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_BusinessId",
                table: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "BusinessId",
                table: "Event",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "EventLocation",
                table: "Event",
                nullable: true);
        }
    }
}
