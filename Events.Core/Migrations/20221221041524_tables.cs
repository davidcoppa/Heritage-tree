using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Country",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Capital = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Country", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EventType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EventType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MediaType",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MediaType", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Tags",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Tags", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Person",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        FirstSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SecondSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        PlaceOfBirthId = table.Column<int>(type: "int", nullable: true),
            //        PlaceOfDeathId = table.Column<int>(type: "int", nullable: true),
            //        DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Sex = table.Column<int>(type: "int", nullable: false),
            //        Order = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Person", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Person_Country_PlaceOfBirthId",
            //            column: x => x.PlaceOfBirthId,
            //            principalTable: "Country",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Person_Country_PlaceOfDeathId",
            //            column: x => x.PlaceOfDeathId,
            //            principalTable: "Country",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "State",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Capital = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CountryId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_State", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_State_Country_CountryId",
            //            column: x => x.CountryId,
            //            principalTable: "Country",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Event",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        EventTypeId = table.Column<int>(type: "int", nullable: false),
            //        Person1Id = table.Column<int>(type: "int", nullable: false),
            //        Person2Id = table.Column<int>(type: "int", nullable: true),
            //        Person3Id = table.Column<int>(type: "int", nullable: true),
            //        LoccationId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Event", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Event_Country_LoccationId",
            //            column: x => x.LoccationId,
            //            principalTable: "Country",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Event_EventType_EventTypeId",
            //            column: x => x.EventTypeId,
            //            principalTable: "EventType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Event_Person_Person1Id",
            //            column: x => x.Person1Id,
            //            principalTable: "Person",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Event_Person_Person2Id",
            //            column: x => x.Person2Id,
            //            principalTable: "Person",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Event_Person_Person3Id",
            //            column: x => x.Person3Id,
            //            principalTable: "Person",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "City",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Capital = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        StatesId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_City", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_City_State_StatesId",
            //            column: x => x.StatesId,
            //            principalTable: "State",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Media",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        MediaDateUploaded = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        EventId = table.Column<int>(type: "int", nullable: true),
            //        PersonId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Media", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Media_Event_EventId",
            //            column: x => x.EventId,
            //            principalTable: "Event",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_Media_Person_PersonId",
            //            column: x => x.PersonId,
            //            principalTable: "Person",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FileData",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DateUploaded = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DocumentTypeId = table.Column<int>(type: "int", nullable: false),
            //        Size = table.Column<int>(type: "int", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        WebUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MediaId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FileData", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_FileData_Media_MediaId",
            //            column: x => x.MediaId,
            //            principalTable: "Media",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_FileData_MediaType_DocumentTypeId",
            //            column: x => x.DocumentTypeId,
            //            principalTable: "MediaType",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "MediaTags",
            //    columns: table => new
            //    {
            //        MediaId = table.Column<int>(type: "int", nullable: false),
            //        TagItemsId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MediaTags", x => new { x.MediaId, x.TagItemsId });
            //        table.ForeignKey(
            //            name: "FK_MediaTags_Media_MediaId",
            //            column: x => x.MediaId,
            //            principalTable: "Media",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MediaTags_Tags_TagItemsId",
            //            column: x => x.TagItemsId,
            //            principalTable: "Tags",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_City_StatesId",
            //    table: "City",
            //    column: "StatesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Event_EventTypeId",
            //    table: "Event",
            //    column: "EventTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Event_LoccationId",
            //    table: "Event",
            //    column: "LoccationId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Event_Person1Id",
            //    table: "Event",
            //    column: "Person1Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Event_Person2Id",
            //    table: "Event",
            //    column: "Person2Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Event_Person3Id",
            //    table: "Event",
            //    column: "Person3Id");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileData_DocumentTypeId",
            //    table: "FileData",
            //    column: "DocumentTypeId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_FileData_MediaId",
            //    table: "FileData",
            //    column: "MediaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_EventId",
            //    table: "Media",
            //    column: "EventId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Media_PersonId",
            //    table: "Media",
            //    column: "PersonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MediaTags_TagItemsId",
            //    table: "MediaTags",
            //    column: "TagItemsId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Person_PlaceOfBirthId",
            //    table: "Person",
            //    column: "PlaceOfBirthId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Person_PlaceOfDeathId",
            //    table: "Person",
            //    column: "PlaceOfDeathId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_State_CountryId",
            //    table: "State",
            //    column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "City");

            //migrationBuilder.DropTable(
            //    name: "FileData");

            //migrationBuilder.DropTable(
            //    name: "MediaTags");

            //migrationBuilder.DropTable(
            //    name: "State");

            //migrationBuilder.DropTable(
            //    name: "MediaType");

            //migrationBuilder.DropTable(
            //    name: "Media");

            //migrationBuilder.DropTable(
            //    name: "Tags");

            //migrationBuilder.DropTable(
            //    name: "Event");

            //migrationBuilder.DropTable(
            //    name: "EventType");

            //migrationBuilder.DropTable(
            //    name: "Person");

            //migrationBuilder.DropTable(
            //    name: "Country");
        }
    }
}
