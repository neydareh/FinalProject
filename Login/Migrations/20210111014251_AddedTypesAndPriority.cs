using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class AddedTypesAndPriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketPriority",
                schema: "dbo",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "dbo",
                table: "Ticket",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AddProjectViewModel",
                schema: "dbo",
                columns: table => new
                {
                    ProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddProjectViewModel", x => x.ProjectID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddProjectViewModel",
                schema: "dbo");

            migrationBuilder.DropColumn(
                name: "TicketPriority",
                schema: "dbo",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "Ticket");
        }
    }
}
