using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class mediaDateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MediaDateUploaded",
                table: "Media",
                newName: "DateUploaded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateUploaded",
                table: "Media",
                newName: "MediaDateUploaded");
        }
    }
}
