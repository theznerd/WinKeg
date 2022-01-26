using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WinKeg.Data.Migrations
{
    public partial class nullableKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_Kegerators_KegeratorId",
                table: "Hardware");

            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_Kegs_KegId",
                table: "Hardware");

            migrationBuilder.AlterColumn<int>(
                name: "KegeratorId",
                table: "Hardware",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "KegId",
                table: "Hardware",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_Kegerators_KegeratorId",
                table: "Hardware",
                column: "KegeratorId",
                principalTable: "Kegerators",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_Kegs_KegId",
                table: "Hardware",
                column: "KegId",
                principalTable: "Kegs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_Kegerators_KegeratorId",
                table: "Hardware");

            migrationBuilder.DropForeignKey(
                name: "FK_Hardware_Kegs_KegId",
                table: "Hardware");

            migrationBuilder.AlterColumn<int>(
                name: "KegeratorId",
                table: "Hardware",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KegId",
                table: "Hardware",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_Kegerators_KegeratorId",
                table: "Hardware",
                column: "KegeratorId",
                principalTable: "Kegerators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hardware_Kegs_KegId",
                table: "Hardware",
                column: "KegId",
                principalTable: "Kegs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
