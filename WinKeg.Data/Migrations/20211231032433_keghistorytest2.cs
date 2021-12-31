using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class keghistorytest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories");

            migrationBuilder.RenameColumn(
                name: "BeverageId",
                table: "KegHistories",
                newName: "BeverageID");

            migrationBuilder.RenameIndex(
                name: "IX_KegHistories_BeverageId",
                table: "KegHistories",
                newName: "IX_KegHistories_BeverageID");

            migrationBuilder.AddForeignKey(
                name: "FK_KegHistories_Beverages_BeverageID",
                table: "KegHistories",
                column: "BeverageID",
                principalTable: "Beverages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KegHistories_Beverages_BeverageID",
                table: "KegHistories");

            migrationBuilder.RenameColumn(
                name: "BeverageID",
                table: "KegHistories",
                newName: "BeverageId");

            migrationBuilder.RenameIndex(
                name: "IX_KegHistories_BeverageID",
                table: "KegHistories",
                newName: "IX_KegHistories_BeverageId");

            migrationBuilder.AddForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories",
                column: "BeverageId",
                principalTable: "Beverages",
                principalColumn: "Id");
        }
    }
}
