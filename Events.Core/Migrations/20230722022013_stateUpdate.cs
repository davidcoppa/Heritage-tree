using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class stateUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "State",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "Country",
                newName: "Lng");

            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "City",
                newName: "Lng");

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "State",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "Country",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "City",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b4a505e-964f-472c-8282-6eebf3da2c8d",
                column: "ConcurrencyStamp",
                value: "f304a0cd-be4b-42d9-a0e5-4c13daf6a531");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6368d931-bb95-4a6d-9ada-ac38d97381e1",
                column: "ConcurrencyStamp",
                value: "ccafe48a-17b3-4a1f-a4ad-6146dfc67747");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "State");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "City");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "State",
                newName: "Coordinates");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "Country",
                newName: "Coordinates");

            migrationBuilder.RenameColumn(
                name: "Lng",
                table: "City",
                newName: "Coordinates");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b4a505e-964f-472c-8282-6eebf3da2c8d",
                column: "ConcurrencyStamp",
                value: "3b6f3cd0-e355-4086-8abc-b95e9fa2604a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6368d931-bb95-4a6d-9ada-ac38d97381e1",
                column: "ConcurrencyStamp",
                value: "ae38d753-120f-4665-a861-1350ddbdc1b2");
        }
    }
}
