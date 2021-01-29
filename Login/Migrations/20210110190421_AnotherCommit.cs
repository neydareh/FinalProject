using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class AnotherCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_UserId",
                schema: "dbo",
                table: "Ticket");

            migrationBuilder.AddColumn<string>(
                name: "AssignedToId",
                schema: "dbo",
                table: "Ticket",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_AssignedToId",
                schema: "dbo",
                table: "Ticket",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_AssignedToId",
                schema: "dbo",
                table: "Ticket",
                column: "AssignedToId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_AssignedToId",
                schema: "dbo",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_AssignedToId",
                schema: "dbo",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "AssignedToId",
                schema: "dbo",
                table: "Ticket");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_UserId",
                schema: "dbo",
                table: "Ticket",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
