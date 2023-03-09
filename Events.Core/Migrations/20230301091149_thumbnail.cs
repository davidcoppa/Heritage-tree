using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Events.Core.Migrations
{
    public partial class thumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string> (
                 name: "UrlPreview",
                 table: "FileData",
                 type: "nvarchar(max)",
                 nullable: true);


      


            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
