using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class addedMusicianMusicianInfluenceScore1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicianMusicianInfluenceScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MusicianId = table.Column<int>(nullable: false),
                    MusicianToCheckId = table.Column<int>(nullable: false),
                    InfluenceScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianMusicianInfluenceScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicianMusicianInfluenceScore_Musician_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianMusicianInfluenceScore_MusicianId",
                table: "MusicianMusicianInfluenceScore",
                column: "MusicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianMusicianInfluenceScore");
        }
    }
}
