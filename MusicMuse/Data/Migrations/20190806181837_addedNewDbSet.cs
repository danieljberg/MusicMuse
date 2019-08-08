using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class addedNewDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicianBandInfluenceScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MusicianId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: false),
                    InfluenceScore = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicianBandInfluenceScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MusicianBandInfluenceScore_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicianBandInfluenceScore_Musician_MusicianId",
                        column: x => x.MusicianId,
                        principalTable: "Musician",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicianBandInfluenceScore_BandId",
                table: "MusicianBandInfluenceScore",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicianBandInfluenceScore_MusicianId",
                table: "MusicianBandInfluenceScore",
                column: "MusicianId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicianBandInfluenceScore");
        }
    }
}
