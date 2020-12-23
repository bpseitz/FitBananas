using Microsoft.EntityFrameworkCore.Migrations;

namespace FitBananas.Migrations
{
    public partial class MetricUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Elevation_Gain",
                table: "SwimTotals");

            migrationBuilder.AddColumn<bool>(
                name: "MetricUnits",
                table: "Athletes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetricUnits",
                table: "Athletes");

            migrationBuilder.AddColumn<int>(
                name: "Elevation_Gain",
                table: "SwimTotals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
