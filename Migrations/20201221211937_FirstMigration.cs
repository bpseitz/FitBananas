using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitBananas.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    AthleteId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.AthleteId);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ActivityType = table.Column<string>(nullable: true),
                    ChallengeType = table.Column<string>(nullable: true),
                    Goal = table.Column<int>(nullable: false),
                    ImageFileName = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.ChallengeId);
                });

            migrationBuilder.CreateTable(
                name: "AthleteStatsSets",
                columns: table => new
                {
                    AthleteStatsId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AthleteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteStatsSets", x => x.AthleteStatsId);
                    table.ForeignKey(
                        name: "FK_AthleteStatsSets_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "AthleteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AthleteChallenges",
                columns: table => new
                {
                    AthleteChallengeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AthleteId = table.Column<int>(nullable: false),
                    ChallengeId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteChallenges", x => x.AthleteChallengeId);
                    table.ForeignKey(
                        name: "FK_AthleteChallenges_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "AthleteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AthleteChallenges_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "ChallengeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikeTotals",
                columns: table => new
                {
                    BikeTotalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Distance = table.Column<int>(nullable: false),
                    Elevation_Gain = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AthleteStatsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeTotals", x => x.BikeTotalId);
                    table.ForeignKey(
                        name: "FK_BikeTotals_AthleteStatsSets_AthleteStatsId",
                        column: x => x.AthleteStatsId,
                        principalTable: "AthleteStatsSets",
                        principalColumn: "AthleteStatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RunTotals",
                columns: table => new
                {
                    RunTotalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Distance = table.Column<int>(nullable: false),
                    Elevation_Gain = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AthleteStatsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunTotals", x => x.RunTotalId);
                    table.ForeignKey(
                        name: "FK_RunTotals_AthleteStatsSets_AthleteStatsId",
                        column: x => x.AthleteStatsId,
                        principalTable: "AthleteStatsSets",
                        principalColumn: "AthleteStatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SwimTotals",
                columns: table => new
                {
                    SwimTotalId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Distance = table.Column<int>(nullable: false),
                    Elevation_Gain = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    AthleteStatsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwimTotals", x => x.SwimTotalId);
                    table.ForeignKey(
                        name: "FK_SwimTotals_AthleteStatsSets_AthleteStatsId",
                        column: x => x.AthleteStatsId,
                        principalTable: "AthleteStatsSets",
                        principalColumn: "AthleteStatsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteChallenges_AthleteId",
                table: "AthleteChallenges",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteChallenges_ChallengeId",
                table: "AthleteChallenges",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteStatsSets_AthleteId",
                table: "AthleteStatsSets",
                column: "AthleteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BikeTotals_AthleteStatsId",
                table: "BikeTotals",
                column: "AthleteStatsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RunTotals_AthleteStatsId",
                table: "RunTotals",
                column: "AthleteStatsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SwimTotals_AthleteStatsId",
                table: "SwimTotals",
                column: "AthleteStatsId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteChallenges");

            migrationBuilder.DropTable(
                name: "BikeTotals");

            migrationBuilder.DropTable(
                name: "RunTotals");

            migrationBuilder.DropTable(
                name: "SwimTotals");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "AthleteStatsSets");

            migrationBuilder.DropTable(
                name: "Athletes");
        }
    }
}
