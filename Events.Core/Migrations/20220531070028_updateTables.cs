using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class updateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person1ID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person2ID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person3ID",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonFatherID",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonID",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonMotherID",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Event_EventID",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Person_PersonID",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "Photos",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "EventID",
                table: "Photos",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PersonID",
                table: "Photos",
                newName: "IX_Photos_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_EventID",
                table: "Photos",
                newName: "IX_Photos_EventId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Person",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PersonMotherID",
                table: "ParentPerson",
                newName: "PersonMotherId");

            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "ParentPerson",
                newName: "PersonId");

            migrationBuilder.RenameColumn(
                name: "PersonFatherID",
                table: "ParentPerson",
                newName: "PersonFatherId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonMotherID",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonMotherId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonID",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonFatherID",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonFatherId");

            migrationBuilder.RenameColumn(
                name: "Person3ID",
                table: "Event",
                newName: "Person3Id");

            migrationBuilder.RenameColumn(
                name: "Person2ID",
                table: "Event",
                newName: "Person2Id");

            migrationBuilder.RenameColumn(
                name: "Person1ID",
                table: "Event",
                newName: "Person1Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Event",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person3ID",
                table: "Event",
                newName: "IX_Event_Person3Id");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person2ID",
                table: "Event",
                newName: "IX_Event_Person2Id");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person1ID",
                table: "Event",
                newName: "IX_Event_Person1Id");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlFile",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person1Id",
                table: "Event",
                column: "Person1Id",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person2Id",
                table: "Event",
                column: "Person2Id",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person3Id",
                table: "Event",
                column: "Person3Id",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonFatherId",
                table: "ParentPerson",
                column: "PersonFatherId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonId",
                table: "ParentPerson",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonMotherId",
                table: "ParentPerson",
                column: "PersonMotherId",
                principalTable: "Person",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Event_EventId",
                table: "Photos",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Person_PersonId",
                table: "Photos",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person1Id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person2Id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Person_Person3Id",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonFatherId",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonId",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentPerson_Person_PersonMotherId",
                table: "ParentPerson");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Event_EventId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Person_PersonId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "UrlFile",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Photos",
                newName: "PersonID");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Photos",
                newName: "EventID");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_PersonId",
                table: "Photos",
                newName: "IX_Photos_PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_EventId",
                table: "Photos",
                newName: "IX_Photos_EventID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Person",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PersonMotherId",
                table: "ParentPerson",
                newName: "PersonMotherID");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "ParentPerson",
                newName: "PersonID");

            migrationBuilder.RenameColumn(
                name: "PersonFatherId",
                table: "ParentPerson",
                newName: "PersonFatherID");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonMotherId",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonMotherID");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonId",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonID");

            migrationBuilder.RenameIndex(
                name: "IX_ParentPerson_PersonFatherId",
                table: "ParentPerson",
                newName: "IX_ParentPerson_PersonFatherID");

            migrationBuilder.RenameColumn(
                name: "Person3Id",
                table: "Event",
                newName: "Person3ID");

            migrationBuilder.RenameColumn(
                name: "Person2Id",
                table: "Event",
                newName: "Person2ID");

            migrationBuilder.RenameColumn(
                name: "Person1Id",
                table: "Event",
                newName: "Person1ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Event",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person3Id",
                table: "Event",
                newName: "IX_Event_Person3ID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person2Id",
                table: "Event",
                newName: "IX_Event_Person2ID");

            migrationBuilder.RenameIndex(
                name: "IX_Event_Person1Id",
                table: "Event",
                newName: "IX_Event_Person1ID");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person1ID",
                table: "Event",
                column: "Person1ID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person2ID",
                table: "Event",
                column: "Person2ID",
                principalTable: "Person",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Person_Person3ID",
                table: "Event",
                column: "Person3ID",
                principalTable: "Person",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonFatherID",
                table: "ParentPerson",
                column: "PersonFatherID",
                principalTable: "Person",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonID",
                table: "ParentPerson",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentPerson_Person_PersonMotherID",
                table: "ParentPerson",
                column: "PersonMotherID",
                principalTable: "Person",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Event_EventID",
                table: "Photos",
                column: "EventID",
                principalTable: "Event",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Person_PersonID",
                table: "Photos",
                column: "PersonID",
                principalTable: "Person",
                principalColumn: "ID");
        }
    }
}
