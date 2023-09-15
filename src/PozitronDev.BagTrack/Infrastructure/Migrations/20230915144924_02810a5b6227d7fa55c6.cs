using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _02810a5b6227d7fa55c6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagTrackId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Carousel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Flight = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Airline = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    JulianDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResponseNeeded = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Carousel = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bag_IsDeleted",
                table: "Bag",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bag");

            migrationBuilder.DropTable(
                name: "Device");
        }
    }
}
