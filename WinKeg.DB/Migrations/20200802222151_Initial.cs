using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WinKeg.DB.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BeverageImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    ImageBlob = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeverageImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kegerator",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kegerator", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KegeratorEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KegeratorEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kegs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    InitialVolume = table.Column<double>(nullable: false),
                    CurrentVolume = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kegs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    IsBeverageRestricted = table.Column<bool>(nullable: false),
                    IsAdministrator = table.Column<bool>(nullable: false),
                    EncryptedPasscode = table.Column<string>(nullable: true),
                    PCSalt = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beverages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ABV = table.Column<double>(nullable: false),
                    IBU = table.Column<double>(nullable: false),
                    IsRestricted = table.Column<bool>(nullable: false),
                    BeverageImageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beverages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beverages_BeverageImages_BeverageImageId",
                        column: x => x.BeverageImageId,
                        principalTable: "BeverageImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Hardware",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    KegId = table.Column<int>(nullable: true),
                    KegeratorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hardware_Kegs_KegId",
                        column: x => x.KegId,
                        principalTable: "Kegs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Hardware_Kegerator_KegeratorId",
                        column: x => x.KegeratorId,
                        principalTable: "Kegerator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KegHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GUID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModified = table.Column<DateTime>(nullable: false),
                    KegId = table.Column<int>(nullable: true),
                    BeverageId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KegHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KegHistories_Beverages_BeverageId",
                        column: x => x.BeverageId,
                        principalTable: "Beverages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KegHistories_Kegs_KegId",
                        column: x => x.KegId,
                        principalTable: "Kegs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Data = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: true),
                    KegHistoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_KegHistories_KegHistoryId",
                        column: x => x.KegHistoryId,
                        principalTable: "KegHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEvents_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Beverages_BeverageImageId",
                table: "Beverages",
                column: "BeverageImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_KegId",
                table: "Hardware",
                column: "KegId");

            migrationBuilder.CreateIndex(
                name: "IX_Hardware_KegeratorId",
                table: "Hardware",
                column: "KegeratorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_KegHistoryId",
                table: "HistoryEvents",
                column: "KegHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEvents_UserId",
                table: "HistoryEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_KegHistories_BeverageId",
                table: "KegHistories",
                column: "BeverageId");

            migrationBuilder.CreateIndex(
                name: "IX_KegHistories_KegId",
                table: "KegHistories",
                column: "KegId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hardware");

            migrationBuilder.DropTable(
                name: "HistoryEvents");

            migrationBuilder.DropTable(
                name: "KegeratorEvents");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Kegerator");

            migrationBuilder.DropTable(
                name: "KegHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Beverages");

            migrationBuilder.DropTable(
                name: "Kegs");

            migrationBuilder.DropTable(
                name: "BeverageImages");
        }
    }
}
