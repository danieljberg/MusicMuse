using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class UpdateForeignKeyOnAllModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Musician",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Business",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Band",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Musician_ApplicationUserId",
                table: "Musician",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_ApplicationUserId",
                table: "Business",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Band_ApplicationUserId",
                table: "Band",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Band_AspNetUsers_ApplicationUserId",
                table: "Band",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Business_AspNetUsers_ApplicationUserId",
                table: "Business",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Musician_AspNetUsers_ApplicationUserId",
                table: "Musician",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Band_AspNetUsers_ApplicationUserId",
                table: "Band");

            migrationBuilder.DropForeignKey(
                name: "FK_Business_AspNetUsers_ApplicationUserId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Musician_AspNetUsers_ApplicationUserId",
                table: "Musician");

            migrationBuilder.DropIndex(
                name: "IX_Musician_ApplicationUserId",
                table: "Musician");

            migrationBuilder.DropIndex(
                name: "IX_Business_ApplicationUserId",
                table: "Business");

            migrationBuilder.DropIndex(
                name: "IX_Band_ApplicationUserId",
                table: "Band");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Musician");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Band");
        }
    }
}
