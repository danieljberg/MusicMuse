using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicMuse.Data.Migrations
{
    public partial class AddedEventBandInfluenceScoreModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Influence1",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence2",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Influence3",
                table: "Event",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventBandInfluenceScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    BandId = table.Column<int>(nullable: false),
                    InfluenceScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventBandInfluenceScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventBandInfluenceScore_Band_BandId",
                        column: x => x.BandId,
                        principalTable: "Band",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventBandInfluenceScore_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventBandInfluenceScore_BandId",
                table: "EventBandInfluenceScore",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_EventBandInfluenceScore_EventId",
                table: "EventBandInfluenceScore",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventBandInfluenceScore");

            migrationBuilder.DropColumn(
                name: "Influence1",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Influence2",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Influence3",
                table: "Event");
        }
    }
}
