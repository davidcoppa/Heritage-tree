using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class coordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "City");

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "State",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Coordinates",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "City");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "State",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "State",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Country",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Country",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "City",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "City",
                type: "float",
                nullable: true);
        }
    }
}
