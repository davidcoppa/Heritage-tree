using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class MediaFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Event_EventId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_MediaType_MediaTypeId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Person_PersonId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Media");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PersonId",
                table: "Media",
                newName: "IX_Media_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_MediaTypeId",
                table: "Media",
                newName: "IX_Media_MediaTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_EventId",
                table: "Media",
                newName: "IX_Media_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Media",
                table: "Media",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Event_EventId",
                table: "Media",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Person_PersonId",
                table: "Media",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Event_EventId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaType_MediaTypeId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Media_Person_PersonId",
                table: "Media");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Media",
                table: "Media");

            migrationBuilder.RenameTable(
                name: "Media",
                newName: "Photos");

            migrationBuilder.RenameIndex(
                name: "IX_Media_PersonId",
                table: "Photos",
                newName: "IX_Photos_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_MediaTypeId",
                table: "Photos",
                newName: "IX_Photos_MediaTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Media_EventId",
                table: "Photos",
                newName: "IX_Photos_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Event_EventId",
                table: "Photos",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_MediaType_MediaTypeId",
                table: "Photos",
                column: "MediaTypeId",
                principalTable: "MediaType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Person_PersonId",
                table: "Photos",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
