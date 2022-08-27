using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class locationupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfBirth",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PlaceOfDeath",
                table: "Person");

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfBirthId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaceOfDeathId",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "stringName",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_PlaceOfBirthId",
                table: "Person",
                column: "PlaceOfBirthId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_PlaceOfDeathId",
                table: "Person",
                column: "PlaceOfDeathId");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Location_PlaceOfBirthId",
                table: "Person",
                column: "PlaceOfBirthId",
                principalTable: "Location",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Location_PlaceOfDeathId",
                table: "Person",
                column: "PlaceOfDeathId",
                principalTable: "Location",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Location_PlaceOfBirthId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Location_PlaceOfDeathId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_PlaceOfBirthId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_PlaceOfDeathId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PlaceOfBirthId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "PlaceOfDeathId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "stringName",
                table: "Location");

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfBirth",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfDeath",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
