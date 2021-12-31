using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class historyeventtest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEvents_KegHistories_KegHistoryId",
                table: "HistoryEvents");

            migrationBuilder.RenameColumn(
                name: "KegHistoryId",
                table: "HistoryEvents",
                newName: "KegHistoryID");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryEvents_KegHistoryId",
                table: "HistoryEvents",
                newName: "IX_HistoryEvents_KegHistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEvents_KegHistories_KegHistoryID",
                table: "HistoryEvents",
                column: "KegHistoryID",
                principalTable: "KegHistories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HistoryEvents_KegHistories_KegHistoryID",
                table: "HistoryEvents");

            migrationBuilder.RenameColumn(
                name: "KegHistoryID",
                table: "HistoryEvents",
                newName: "KegHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_HistoryEvents_KegHistoryID",
                table: "HistoryEvents",
                newName: "IX_HistoryEvents_KegHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HistoryEvents_KegHistories_KegHistoryId",
                table: "HistoryEvents",
                column: "KegHistoryId",
                principalTable: "KegHistories",
                principalColumn: "Id");
        }
    }
}
