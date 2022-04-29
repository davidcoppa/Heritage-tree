using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lgn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceOfDeath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EventTypeId = table.Column<int>(type: "int", nullable: false),
                    Person1ID = table.Column<int>(type: "int", nullable: false),
                    Person2ID = table.Column<int>(type: "int", nullable: true),
                    Person3ID = table.Column<int>(type: "int", nullable: true),
                    LoccationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Event_EventType_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Location_LoccationId",
                        column: x => x.LoccationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Event_Person_Person1ID",
                        column: x => x.Person1ID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Event_Person_Person2ID",
                        column: x => x.Person2ID,
                        principalTable: "Person",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Event_Person_Person3ID",
                        column: x => x.Person3ID,
                        principalTable: "Person",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ParentPerson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    PersonFatherID = table.Column<int>(type: "int", nullable: true),
                    PersonMotherID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentPerson_Person_PersonFatherID",
                        column: x => x.PersonFatherID,
                        principalTable: "Person",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ParentPerson_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParentPerson_Person_PersonMotherID",
                        column: x => x.PersonMotherID,
                        principalTable: "Person",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    PersonID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Event_EventID",
                        column: x => x.EventID,
                        principalTable: "Event",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Photos_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventTypeId",
                table: "Event",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_LoccationId",
                table: "Event",
                column: "LoccationId");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Person1ID",
                table: "Event",
                column: "Person1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Person2ID",
                table: "Event",
                column: "Person2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Event_Person3ID",
                table: "Event",
                column: "Person3ID");

            migrationBuilder.CreateIndex(
                name: "IX_ParentPerson_PersonFatherID",
                table: "ParentPerson",
                column: "PersonFatherID");

            migrationBuilder.CreateIndex(
                name: "IX_ParentPerson_PersonID",
                table: "ParentPerson",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_ParentPerson_PersonMotherID",
                table: "ParentPerson",
                column: "PersonMotherID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_EventID",
                table: "Photos",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PersonID",
                table: "Photos",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentPerson");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "EventType");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
