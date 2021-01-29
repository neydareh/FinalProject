using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class DeletedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "dbo",
                table: "Ticket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "dbo",
                table: "Ticket",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);
        }
    }
}
