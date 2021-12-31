using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class keghistorytest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories");

            migrationBuilder.AlterColumn<int>(
                name: "BeverageId",
                table: "KegHistories",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories",
                column: "BeverageId",
                principalTable: "Beverages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories");

            migrationBuilder.AlterColumn<int>(
                name: "BeverageId",
                table: "KegHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KegHistories_Beverages_BeverageId",
                table: "KegHistories",
                column: "BeverageId",
                principalTable: "Beverages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
