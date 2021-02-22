using Microsoft.EntityFrameworkCore.Migrations;

namespace Login.Migrations
{
    public partial class commentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Ticket_TicketId",
                schema: "dbo",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                schema: "dbo",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "Comment",
                schema: "dbo",
                newName: "Comments",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_TicketId",
                schema: "dbo",
                table: "Comments",
                newName: "IX_Comments_TicketId");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                schema: "dbo",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                schema: "dbo",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Ticket_TicketId",
                schema: "dbo",
                table: "Comments",
                column: "TicketId",
                principalSchema: "dbo",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Ticket_TicketId",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                schema: "dbo",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                schema: "dbo",
                newName: "Comment",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_TicketId",
                schema: "dbo",
                table: "Comment",
                newName: "IX_Comment_TicketId");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                schema: "dbo",
                table: "Comment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                schema: "dbo",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Ticket_TicketId",
                schema: "dbo",
                table: "Comment",
                column: "TicketId",
                principalSchema: "dbo",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
