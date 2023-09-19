using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PozitronDev.BagTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _92b999bf839b6493f0ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IATA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    BagCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airline", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagTagId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    DeviceId = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Carousel = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    AirlineIATA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Flight = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    JulianDate = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResponseNeeded = table.Column<bool>(type: "bit", nullable: false),
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
                    Id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Carousel = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineIATA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    NumberIATA = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    OriginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActiveCarousel = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    AllocatedCarousel = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stop = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bag_BagTagId",
                table: "Bag",
                column: "BagTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Bag_Date",
                table: "Bag",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Bag_IsDeleted",
                table: "Bag",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_ActiveCarousel",
                table: "Flight",
                column: "ActiveCarousel");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_AirlineIATA",
                table: "Flight",
                column: "AirlineIATA");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_IsDeleted",
                table: "Flight",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_Start",
                table: "Flight",
                column: "Start");

            migrationBuilder.CreateIndex(
                name: "IX_Flight_Stop",
                table: "Flight",
                column: "Stop");

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessage_IsDeleted",
                table: "InboxMessage",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessage_IsDeleted",
                table: "OutboxMessage",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airline");

            migrationBuilder.DropTable(
                name: "Bag");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "InboxMessage");

            migrationBuilder.DropTable(
                name: "OutboxMessage");
        }
    }
}
