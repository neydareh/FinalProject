using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class AddedUpdatedDateFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "dbo",
                table: "Ticket",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "dbo",
                table: "Ticket");
        }
    }
}
