using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class mediatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Photos",
                newName: "MediaDateUploaded");

            migrationBuilder.AddColumn<DateTime>(
                name: "MediaDate",
                table: "Photos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MediaTypeId",
                table: "Photos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MediaTypeId",
                table: "Photos",
                column: "MediaTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_MediaType_MediaTypeId",
                table: "Photos",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_MediaType_MediaTypeId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "MediaType");

            migrationBuilder.DropIndex(
                name: "IX_Photos_MediaTypeId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MediaDate",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MediaTypeId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "MediaDateUploaded",
                table: "Photos",
                newName: "Date");
        }
    }
}
