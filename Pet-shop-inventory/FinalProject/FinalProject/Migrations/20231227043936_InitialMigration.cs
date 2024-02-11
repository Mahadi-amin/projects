using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CagesOrAquariums",
                columns: table => new
                {
                    CageOrAquariumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CagesOrAquariums", x => x.CageOrAquariumId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseInformations",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfPet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseInformations", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "SalesRecords",
                columns: table => new
                {
                    SalesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRecords", x => x.SalesId);
                });

            migrationBuilder.CreateTable(
                name: "FeedingSchedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CageOrAquariumId = table.Column<int>(type: "int", nullable: false),
                    FeedTime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingSchedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_FeedingSchedules_CagesOrAquariums_CageOrAquariumId",
                        column: x => x.CageOrAquariumId,
                        principalTable: "CagesOrAquariums",
                        principalColumn: "CageOrAquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CageOrAquariumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_CagesOrAquariums_CageOrAquariumId",
                        column: x => x.CageOrAquariumId,
                        principalTable: "CagesOrAquariums",
                        principalColumn: "CageOrAquariumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "admin", "123456" });

            migrationBuilder.CreateIndex(
                name: "IX_FeedingSchedules_CageOrAquariumId",
                table: "FeedingSchedules",
                column: "CageOrAquariumId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CageOrAquariumId",
                table: "Pets",
                column: "CageOrAquariumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "FeedingSchedules");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "PurchaseInformations");

            migrationBuilder.DropTable(
                name: "SalesRecords");

            migrationBuilder.DropTable(
                name: "CagesOrAquariums");
        }
    }
}
