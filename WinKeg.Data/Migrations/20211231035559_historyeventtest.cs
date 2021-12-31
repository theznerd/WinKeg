using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class historyeventtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEvents_Users_UserId",
                table: "HistoryEvents");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HistoryEvents",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryEvents_UserId",
                table: "HistoryEvents",
                newName: "IX_HistoryEvents_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "HistoryEvents",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEvents_Users_UserID",
                table: "HistoryEvents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEvents_Users_UserID",
                table: "HistoryEvents");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "HistoryEvents",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryEvents_UserID",
                table: "HistoryEvents",
                newName: "IX_HistoryEvents_UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HistoryEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEvents_Users_UserId",
                table: "HistoryEvents",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
