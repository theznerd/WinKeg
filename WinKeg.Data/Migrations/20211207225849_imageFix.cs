using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class imageFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beverages_BeverageImages_ImageId",
                table: "Beverages");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Beverages",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Beverages_BeverageImages_ImageId",
                table: "Beverages",
                column: "ImageId",
                principalTable: "BeverageImages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beverages_BeverageImages_ImageId",
                table: "Beverages");

            migrationBuilder.AlterColumn<int>(
                name: "ImageId",
                table: "Beverages",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beverages_BeverageImages_ImageId",
                table: "Beverages",
                column: "ImageId",
                principalTable: "BeverageImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
